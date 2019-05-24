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
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace GenApi
{
    static class Program
    {
        static void printMarkdown(int indent, IEnumerable<KeyValuePair<string, ApiMethod>> methods)
        {
            var groups = methods.GroupBy(kvp =>
            {
                var splits = kvp.Key.Split('.');
                return string.Join(".", splits.Take(indent + 1));
            });

            var thingsToPrintAtTheEnd = new List<KeyValuePair<string, ApiMethod>>();

            foreach (var group in groups)
            {
                var methCount = group.Count();
                if (methCount <= 0)
                    throw new Exception("That's unexpected.");
                else if (methCount == 1)
                {
                    thingsToPrintAtTheEnd.Add(group.Single());
                }
                else
                {
                    string indentSpace = new string(' ', indent * 2);
                    Console.WriteLine(indentSpace + "- " + group.Key);
                    printMarkdown(indent + 1, group);
                }
            }

            foreach (var meth in thingsToPrintAtTheEnd)
            {
                string firstGroupName = meth.Key.Split('.')[0];
                if (firstGroupName == "api" || firstGroupName == "avail" || firstGroupName == "test")
                {
                    firstGroupName = "utility";
                }
                string indentSpace = new string(' ', indent * 2);
                Console.WriteLine($"{indentSpace}- [ ] [{meth.Key}](https://www.linode.com/api/{firstGroupName}/{meth.Key})");
            }
        }

        static void Main(string[] args)
        {
            ApiSpec spec;
            if (args.Any(a => a == "online"))
            {
                var li = new LinodeClient("~~~");
                spec = li.Api_Spec();
            }
            else
            {
                var res = JsonConvert.DeserializeObject<Response<ApiSpec>>(File.ReadAllText(@"spec.json"));
                if (res.Errors.Length != 0)
                    throw new LinodeException(res.Errors);
                spec = res.Data;
            }

            if (args.Any(a => a == "markdown"))
            {
                printMarkdown(0, spec.Methods.OrderBy(kvp => kvp.Key));
                return;
            }

            var gen = new SpecGen(spec);
            Console.WriteLine(gen.TransformText());
            Console.ReadLine();
        }
    }
}
