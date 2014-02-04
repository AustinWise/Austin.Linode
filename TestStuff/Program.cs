using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Austin.Linode;

namespace TestStuff
{
    class Program
    {
        static void Main(string[] args)
        {
            var li = new LinodeClient("~~~");

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
