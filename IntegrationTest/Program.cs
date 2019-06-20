/*
 *
 * Copyright (c) 2019, Austin Wise.
 * All rights reserved.
 *
 * Redistribution and use in source and binary forms, with or without
 * modification, are permitted provided that the following conditions are
 * met:
 *
 *     * Redistributions of source code must retain the above copyright
 *       notice, this list of conditions and the following disclaimer.
 *
 *     * Redistributions in binary form must reproduce the above copyright
 *       notice, this list of conditions and the following disclaimer in the
 *       documentation and/or other materials provided with the distribution.
 *
 *     * Neither the name of the Austin.Linode Project, Austin Wise, nor the names
 *       of its contributors may be used to endorse or promote products
 *       derived from this software without specific prior written
 *       permission.
 *
 * THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS
 * IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO,
 * THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
 * PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER OR
 * CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL,
 * EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO,
 * PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR
 * PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF
 * LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING
 * NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS
 * SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
 */

using Austin.Linode;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Austin.Libode.IntegrationTest
{
    /// <summary>
    /// Creates a linode in Fremont and tries out various API calls on,
    /// or assumes assumes machines with the name of <see cref="LINODE_LABEL"/>
    /// are fair game for testing.
    /// </summary>
    static class Program
    {
        /// <summary>
        /// A name that someone would be unlikly to use for something they cared about.
        /// </summary>
        const string LINODE_LABEL = "Austin-s_Deletable_Test_Machine";

        const string CONFIG_LABEL = "austin_test_config";

        /// <summary>
        /// Expected disk label.
        /// </summary>
        const string DISK_LABEL = "austin_test_label";

        const int DISK_SIZE = 4096;
        const int SWAP_SIZE = 256;

        /// <summary>
        /// Ideally, we will have password-based authentication disabled and this invitation will remain unfulfilled.
        /// </summary>
        const string DISK_ROOT_PASSWORD = "PleaseHackMe1337";

        const string DISK_SSH_KEY = "ssh-rsa AAAAB3NzaC1yc2EAAAABIwAAAQEAnJbFVIfhuOtHbIV5/CRzjpMHEnxhwGk7tOFObxe3SeMCd2i1D278RoepOjMuT24Gz/h3J2ZUut7j6W3jRt9m1AqYevB4WnZWNdR33OzWu0y9e+a554QpOiLA074bXHjjv1SM0VSNV3NBZT9ASdHDcRdMWVjuDvQN7d7LIBv/bhIxGUDx9+pbHiahyqtDjq5BaNnA36kK8xA/nw6fqVvp/F3gIUWdjIYgu53qC/4Mgata0rKCScgd1bSstHQYSZrfsI+MK5fKzwl3iMe6tjlkD6bRVoO3ea4X7Votle5l6AI4zzjHoejF/ib88j9XCLLa04PGLRlmPmkD5CZe6BwiKQ== AustinWise@gmail.com";

        static readonly LinodeClient li = new LinodeClient();
        static int linodeId;

        static async Task<string> LoadApiKey(string[] args)
        {
            Properties.Settings settings = Properties.Settings.Default;
            var settingEncoding = Encoding.UTF8;
            const DataProtectionScope settingScope = DataProtectionScope.CurrentUser;

            bool saveKey = true;
            string apiKey;
            if (args.Length == 1 && args[0].ToLowerInvariant() == "--set-api-key")
            {
                Console.Write("Enter API key: ");
                apiKey = Console.ReadLine();
            }
            else if (args.Length == 2 && args[0].ToLowerInvariant() == "--username")
            {
                string username = args[1];
                Console.Write("Enter Password: ");
                string password = Console.ReadLine();
                apiKey = (await li.User_GetApiKeyAsync(password, username)).Key;
            }
            else
            {
                saveKey = false;
                byte[] base64Password = Convert.FromBase64String(settings.ApiKey);
                byte[] decryptedPassword = ProtectedData.Unprotect(base64Password, null, settingScope);
                apiKey = settingEncoding.GetString(decryptedPassword);
            }

            if (saveKey)
            {
                byte[] encryptedPasword = ProtectedData.Protect(settingEncoding.GetBytes(apiKey), null, settingScope);
                string base64ApiKey = Convert.ToBase64String(encryptedPasword);
                settings.ApiKey = base64ApiKey;
                settings.Save();
            }

            return apiKey;
        }

        static bool readBoolFromCommandline(string prompt)
        {
            Console.Write(prompt + "? (type 'true' or 'false'): ");
            string text = Console.ReadLine();
            bool ret;
            if (!bool.TryParse(text, out ret))
            {
                Console.WriteLine("Failed to parse.");
                Environment.Exit(1);
            }
            return ret;
        }

        static void assertEqual<T>(T expected, T actual, bool doThrow = true)
        {
            if (!expected.Equals(actual))
                throw new Exception($"expected '{expected}' but got '{actual}'");
        }

        //TODO: consider adding something like this to the library
        //Though how to implement pooling will require some thought and research.
        static async Task waitForJob(string desc, JobIdResponse jobRes)
        {
            Console.WriteLine(desc);
            int secs = 1;
            while (true)
            {
                var status = (await li.Linode_Job_ListAsync(linodeId, jobRes.JobID)).Single();
                if (status.HostSuccess.HasValue)
                {
                    if (!status.HostSuccess.Value)
                        throw new Exception($"Job {jobRes.JobID} failed!");
                    return;
                }

                Console.WriteLine($"Waiting {secs} seconds for {desc}");
                await Task.Delay(secs * 1000);
                secs = Math.Min(10, secs * 2);
            }
        }

        private static async Task createLinode(bool createIfNotFound)
        {
            var foundNode = (await li.Linode_ListAsync())
                .Where(n => n.Label == LINODE_LABEL)
                .SingleOrDefault();

            if (foundNode != null)
            {
                linodeId = foundNode.Id;
                return;
            }
            else if (!createIfNotFound)
            {
                throw new Exception("Failed to find test linode, and not ordered to create a new one");
            }

            //create the VM in Fremont
            var dc = (await li.Avail_DatacentersAsync())
                .Where(d => d.Location.IndexOf("Fremont") >= 0)
                .Single();
            //cheapest plan
            var plan = (await li.Avail_LinodePlansAsync())
                .OrderBy(p => p.Price)
                .First();

            linodeId = (await li.Linode_CreateAsync(dc.Id, plan.Id)).LinodeID;

            await li.Linode_UpdateAsync(linodeId, Label: LINODE_LABEL);
        }

        static async Task Main(string[] args)
        {
            li.ApiKey = await LoadApiKey(args);

            bool createLinode = readBoolFromCommandline("Do you want to create a VM");
            bool deleteLinode = readBoolFromCommandline("Do you want to delete the VM at the end");

            await Program.createLinode(createLinode);

            //-------- got a linode, now create a disk --------

            var allFoundDisks = await li.Linode_Disk_ListAsync(linodeId);
            int mainDiskId;
            int swapDiskId;
            if (allFoundDisks.Length == 0)
            {
                var distro = (await li.Avail_DistributionsAsync())
                    .Where(d => d.Label == "Ubuntu 16.04 LTS")
                    .Single();

                var fsCreation = await li.Linode_Disk_CreateFromDistributionAsync(distro.Id, DISK_LABEL, linodeId,
                    DISK_ROOT_PASSWORD, DISK_SIZE, DISK_SSH_KEY);
                var swapCreation = await li.Linode_Disk_CreateAsync("swap", linodeId, SWAP_SIZE, "swap");

                await waitForJob("create disk", fsCreation);
                await waitForJob("create swap", swapCreation);
                mainDiskId = fsCreation.DiskID;
                swapDiskId = swapCreation.DiskID;
            }
            else if (allFoundDisks.Length == 2)
            {
                var swapDisk = allFoundDisks.Where(d => d.Type == "swap").Single();
                var mainDisk = allFoundDisks.Where(d => d.Type != "swap").Single();
                //assertEqual(DISK_LABEL, mainDisk.Label);
                mainDiskId = mainDisk.Id;
                swapDiskId = swapDisk.Id;
            }
            else
            {
                throw new Exception("Unexpected number of disks: found " + allFoundDisks.Length);
            }

            //-------- found disks, create config if needed --------
            int configId;
            var allConfigs = await li.Linode_Config_ListAsync(linodeId);
            if (allConfigs.Length == 0)
            {
                var kernel = (await li.Avail_KernelsAsync())
                    .Where(k => k.Label.StartsWith("Latest 64 bit"))
                    .Single();

                var cfg = await li.Linode_Config_CreateAsync($"{mainDiskId},{swapDiskId}", kernel.Id, CONFIG_LABEL, linodeId);
                configId = cfg.ConfigID;
            }
            else if (allConfigs.Length == 1)
            {
                configId = allConfigs[0].Id;
            }
            else
            {
                throw new Exception("Unexpected number of configs: " + allConfigs.Length);
            }

            //-------- found config, boot if not already running --------

            if ((await li.Linode_ListAsync(linodeId)).Single().Status != NodeStatus.Running)
            {
                await waitForJob("boot", await li.Linode_BootAsync(linodeId, configId));
            }

            //TODO: make sure as part of provisioning that only SSH login is supported

            //-------- done testing, delete everything --------

            if (deleteLinode)
            {
                var nodeStatus = (await li.Linode_ListAsync(linodeId)).Single().Status;
                if (nodeStatus == NodeStatus.Running)
                {
                    await waitForJob("shutdown", await li.Linode_ShutdownAsync(linodeId));
                }

                var diskDeleteJobs = (await li.Linode_Disk_ListAsync(linodeId)).Select(d => li.Linode_Disk_DeleteAsync(d.Id, d.LinodeId)).ToArray();
                foreach (var deleteJobTask in diskDeleteJobs)
                {
                    var deleteJob = await deleteJobTask;
                    await waitForJob("delete disk " + deleteJob.DiskID, deleteJob);
                }

                await li.Linode_DeleteAsync(linodeId);
            }
        }
    }
}
