using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Austin.Linode;
using System.Security.Cryptography;

namespace Austin.Libode.IntegrationTest
{
    /// <summary>
    /// Creates a linode in Fremont and tries out various API calls on.
    /// If the linode is not created during this test, this program assumes there
    /// is a single linode in the account that tests can be performed on.
    /// </summary>
    static class Program
    {
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

        /// <summary>
        /// A name that someone would be unlikly to use for something they cared about.
        /// </summary>
        const string TEST_LINODE_LABEL = "Austin-s_Deletable_Test_Machine";

        static void Main(string[] args)
        {
            var li = new LinodeClient(LoadApiKey(args));

            bool createLinode = readBoolFromCommandline("Do you want to create a VM");
            bool deleteLinode = readBoolFromCommandline("Do you want to delete the VM at the end");

            int linodeId;

            if (createLinode)
            {
                //create the VM in Fremont
                var dc = li.Avail_Datacenters()
                    .Where(d => d.Location.IndexOf("Fremont") >= 0)
                    .Single();
                //cheapest plan
                var plan = li.Avail_LinodePlans()
                    .OrderBy(p => p.Price)
                    .First();

                linodeId = li.Linode_Create(dc.Id, plan.Id).LinodeID;

                li.Linode_Update(linodeId, Label: TEST_LINODE_LABEL);
            }
            else
            {
                var node = li.Linode_List()
                    .Where(n => n.Label == TEST_LINODE_LABEL)
                    .Single();
                linodeId = node.Id;
            }

            if (deleteLinode)
            {
                li.Linode_Delete(linodeId);
            }
        }
    }
}
