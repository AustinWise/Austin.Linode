using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Austin.Linode;
using System.Security.Cryptography;

namespace Austin.Libode.IntegrationTest
{
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

        static void Main(string[] args)
        {
            var li = new LinodeClient(LoadApiKey(args));

            var id = li.Linode_List()[0].Id;
            int jobId = li.Linode_Reboot(id).JobID;
            while (true)
            {
                var j = li.Linode_Job_List(id, jobId)[0];

                if (j.HostSuccess.HasValue)
                {
                    Console.WriteLine("Finished: {0}", j.HostSuccess.Value);
                    break;
                }

                Console.WriteLine("still waiting");
                System.Threading.Thread.Sleep(5 * 1000);
            }

            Console.WriteLine();
            Console.WriteLine("Press enter to exit.");
            Console.ReadLine();
        }
    }
}
