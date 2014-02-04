using Austin.Linode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GenApi
{
    class Program
    {
        static void Main(string[] args)
        {
            var li = new LinodeClient("~~~");
            var res = li.Api_Spec();

            var gen = new SpecGen(res);
            Console.WriteLine(gen.TransformText());
            Console.ReadLine();
        }
    }
}
