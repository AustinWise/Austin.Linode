using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Austin.Linode;
using System.Security.Cryptography;
using System.Threading;

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

        /// <summary>
        /// Expected disk label.
        /// </summary>
        const string DISK_LABEL = "austin_test_label";

        const int DISK_SIZE = 4096;

        /// <summary>
        /// Ideally, we will have password-based authentication disabled and this invitation will remain unfulfilled.
        /// </summary>
        const string DISK_ROOT_PASSWORD = "PleaseHackMe1337";

        const string DISK_SSH_KEY = "ssh-rsa AAAAB3NzaC1yc2EAAAABIwAAAQEAnJbFVIfhuOtHbIV5/CRzjpMHEnxhwGk7tOFObxe3SeMCd2i1D278RoepOjMuT24Gz/h3J2ZUut7j6W3jRt9m1AqYevB4WnZWNdR33OzWu0y9e+a554QpOiLA074bXHjjv1SM0VSNV3NBZT9ASdHDcRdMWVjuDvQN7d7LIBv/bhIxGUDx9+pbHiahyqtDjq5BaNnA36kK8xA/nw6fqVvp/F3gIUWdjIYgu53qC/4Mgata0rKCScgd1bSstHQYSZrfsI+MK5fKzwl3iMe6tjlkD6bRVoO3ea4X7Votle5l6AI4zzjHoejF/ib88j9XCLLa04PGLRlmPmkD5CZe6BwiKQ== AustinWise@gmail.com";

        static LinodeClient li;
        static int linodeId;

        static string LoadApiKey(string[] args)
        {
            Properties.Settings settings = Properties.Settings.Default;
            var settingEncoding = Encoding.UTF8;
            var settingScope = DataProtectionScope.CurrentUser;

            string apiKey;
            if (args.Length == 1 && args[0].ToLowerInvariant() == "--set-api-key")
            {
                Console.Write("Enter API key: ");
                apiKey = Console.ReadLine();
                byte[] encryptedPasword = ProtectedData.Protect(settingEncoding.GetBytes(apiKey), null, settingScope);
                string base64ApiKey = Convert.ToBase64String(encryptedPasword);
                settings.ApiKey = base64ApiKey;
                settings.Save();
            }
            else
            {
                byte[] base64Password = Convert.FromBase64String(settings.ApiKey);
                byte[] decryptedPassword = ProtectedData.Unprotect(base64Password, null, settingScope);
                apiKey = settingEncoding.GetString(decryptedPassword);
            }

            return apiKey;
        }

        static bool readBoolFromCommandline(string prompt)
        {
            Console.WriteLine(prompt + "? (type 'true' or 'false'): ");
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
        static void waitForJob(string desc, JobIdResponse jobRes)
        {
            Console.WriteLine(desc);
            int secs = 1;
            while (true)
            {
                var status = li.Linode_Job_List(linodeId, jobRes.JobID).Single();
                if (status.HostSuccess.HasValue)
                {
                    if (!status.HostSuccess.Value)
                        throw new Exception($"Job {jobRes.JobID} failed!");
                    return;
                }

                Console.WriteLine($"Waiting {secs} seconds for {desc}");
                Thread.Sleep(secs * 1000);
                secs = Math.Min(10, secs * 2);
            }
        }

        private static void createLinode(bool createIfNotFound)
        {
            var foundNode = li.Linode_List()
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
            var dc = li.Avail_Datacenters()
                .Where(d => d.Location.IndexOf("Fremont") >= 0)
                .Single();
            //cheapest plan
            var plan = li.Avail_LinodePlans()
                .OrderBy(p => p.Price)
                .First();

            linodeId = li.Linode_Create(dc.Id, plan.Id).LinodeID;

            li.Linode_Update(linodeId, Label: LINODE_LABEL);
        }

        static void Main(string[] args)
        {
            li = new LinodeClient(LoadApiKey(args));

            bool createLinode = readBoolFromCommandline("Do you want to create a VM");
            bool deleteLinode = readBoolFromCommandline("Do you want to delete the VM at the end");

            Program.createLinode(createLinode);

            //-------- got a linode, now create a disk --------

            var foundDisk = li.Linode_Disk_List(linodeId).SingleOrDefault();
            int diskid;
            if (foundDisk == null)
            {
                var distro = li.Avail_Distributions()
                    .Where(d => d.Label == "Ubuntu 16.04 LTS")
                    .Single();

                var creation = li.Linode_Disk_CreateFromDistribution(distro.Id, DISK_LABEL, linodeId,
                    DISK_ROOT_PASSWORD, DISK_SIZE, DISK_SSH_KEY);

                waitForJob("create disk", creation);
                diskid = creation.DiskID;
            }
            else
            {
                assertEqual(DISK_LABEL, foundDisk.Label);
                diskid = foundDisk.Id;
            }

            //-------- done testing, delete everything --------

            if (deleteLinode)
            {
                var nodeStatus = li.Linode_List(linodeId).Single().Status;
                if (nodeStatus == NodeStatus.Running)
                {
                    waitForJob("shutdown", li.Linode_Shutdown(linodeId));
                }
                waitForJob("delete disk", li.Linode_Disk_Delete(diskid, linodeId));
                li.Linode_Delete(linodeId);
                //waitForJob("delete linode", li.Linode_Delete(linodeId));
            }
        }
    }
}
