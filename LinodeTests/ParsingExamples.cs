/*
 *
 * Copyright (c) 2016, Austin Wise.
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

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Austin.Linode;

namespace LinodeTests
{
    // Tests that parse the examples from the API documentation.
    [TestClass]
    public class ParsingExamples
    {
        [TestMethod]
        public void Linode_Ip_List()
        {
            string json = @"
{
   ""ERRORARRAY"":[],
   ""ACTION"":""linode.ip.list"",
   ""DATA"":[
      {
         ""LINODEID"":8098,
         ""ISPUBLIC"":1,
         ""IPADDRESS"":""75.127.96.54"",
         ""RDNS_NAME"":""li22-54.members.linode.com"",
         ""IPADDRESSID"":5384
      },
      {
         ""LINODEID"":8099,
         ""ISPUBLIC"":1,
         ""IPADDRESS"":""75.127.96.245"",
         ""RDNS_NAME"":""li22-245.members.linode.com"",
         ""IPADDRESSID"":5575
      }
   ]
}";
            var res = JsonConvert.DeserializeObject<Response<IpAddressListEntry[]>>(json);
            Assert.AreEqual(0, res.Errors.Length);
            Assert.AreEqual(2, res.Data.Length);

            Assert.AreEqual(8098, res.Data[0].LinodeId);
            Assert.IsTrue(res.Data[0].IsPublic);
            Assert.AreEqual("75.127.96.54", res.Data[0].IpAddress);
            Assert.AreEqual("li22-54.members.linode.com", res.Data[0].ReverseDnsName);
            Assert.AreEqual(5384, res.Data[0].IpAddressId);
        }
    }
}
