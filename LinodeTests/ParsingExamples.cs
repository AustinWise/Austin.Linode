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
        static T GetRes<T>(string json)
        {
            var res = JsonConvert.DeserializeObject<Response<T>>(json);
            Assert.AreEqual(0, res.Errors.Length);
            return res.Data;
        }

        [TestMethod]
        public void Linode_Ip_List()
        {
            const string json = @"
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
            var res = GetRes<IpAddressListEntry[]>(json);
            Assert.AreEqual(2, res.Length);

            Assert.AreEqual(8098, res[0].LinodeId);
            Assert.IsTrue(res[0].IsPublic);
            Assert.AreEqual("75.127.96.54", res[0].IpAddress);
            Assert.AreEqual("li22-54.members.linode.com", res[0].ReverseDnsName);
            Assert.AreEqual(5384, res[0].IpAddressId);
        }

        [TestMethod]
        public void Linode_Disk_List()
        {
            //THE SECOND ITEM HAD AN INVALID DATE STRING ON THE WEBSITE -_-
            const string json = @"
{
   ""ERRORARRAY"":[],
   ""ACTION"":""linode.disk.list"",
   ""DATA"":[
      {
         ""UPDATE_DT"":""2009-06-30 13:19:00.0"",
         ""DISKID"":55319,
         ""LABEL"":""test label"",
         ""TYPE"":""ext3"",
         ""LINODEID"":8098,
         ""ISREADONLY"":0,
         ""STATUS"":1,
         ""CREATE_DT"":""2008-04-04 10:08:06.0"",
         ""SIZE"":4096
      },
      {
         ""UPDATE_DT"":""2009-07-18 12:53:43.0"",
         ""DISKID"":55320,
         ""LABEL"":""256M Swap Image"",
         ""TYPE"":""swap"",
         ""LINODEID"":8098,
         ""ISREADONLY"":0,
         ""STATUS"":1,
         ""CREATE_DT"":""2008-04-04 10:08:06.0"",
         ""SIZE"":256
      }
   ]
}
";

            var res = GetRes<DiskResponse[]>(json);

            var d0 = res[0];
            Assert.AreEqual(new DateTime(2009, 6, 30, 13, 19, 0, DateTimeKind.Utc), d0.UpdatedTime);
            Assert.AreEqual(55319, d0.Id);
            Assert.AreEqual("test label", d0.Label);
            Assert.AreEqual("ext3", d0.Type);
            Assert.AreEqual(8098, d0.LinodeId);
            Assert.AreEqual(false, d0.IsReadonly);
            Assert.AreEqual(DiskStatus.Ready, d0.Status);
            Assert.AreEqual(new DateTime(2008, 4, 4, 10, 8, 6, DateTimeKind.Utc), d0.CreatedTime);
            Assert.AreEqual(4096, d0.Size);

            //TODO: test other element of response
        }

        [TestMethod]
        public void Avail_Distributions()
        {
            const string json = @"
{
   ""ERRORARRAY"":[],
   ""ACTION"":""avail.distributions"",
   ""DATA"":[
      {
         ""IS64BIT"":0,
         ""LABEL"":""Debian 4.0"",
         ""MINIMAGESIZE"":200,
         ""DISTRIBUTIONID"":28,
         ""CREATE_DT"":""2007-04-18 00:00:00.0"",
         ""REQUIRESPVOPSKERNEL"":0
      },
      {
         ""IS64BIT"":0,
         ""LABEL"":""Ubuntu 9.10"",
         ""MINIMAGESIZE"":400,
         ""DISTRIBUTIONID"":64,
         ""CREATE_DT"":""2009-10-31 15:11:29.0"",
         ""REQUIRESPVOPSKERNEL"":1
      },
      {
         ""IS64BIT"":1,
         ""LABEL"":""Ubuntu 8.10 64bit"",
         ""MINIMAGESIZE"":230,
         ""DISTRIBUTIONID"":49,
         ""CREATE_DT"":""2008-12-02 00:00:00.0"",
         ""REQUIRESPVOPSKERNEL"":0
      }
   ]
}
";

            var res = GetRes<DistributionResponse[]>(json);

            var d0 = res[0];
            Assert.AreEqual(false, d0.Is64Bit);
            Assert.AreEqual("Debian 4.0", d0.Label);
            Assert.AreEqual(200, d0.MinimumImageSize);
            Assert.AreEqual(28, d0.Id);
            Assert.AreEqual(new DateTime(2007, 4, 18, 0, 0, 0, DateTimeKind.Utc), d0.CreateTime);
            Assert.AreEqual(false, d0.RequiresPvopsKernel);

            //TODO: test other element of response
        }

        [TestMethod]
        public void Linode_List()
        {
            const string json = @"
{
   ""ERRORARRAY"":[],
   ""ACTION"":""linode.list"",
   ""DATA"":[
      {
         ""TOTALXFER"":2000,
         ""BACKUPSENABLED"":1,
         ""WATCHDOG"":1,
         ""LPM_DISPLAYGROUP"":"""",
         ""ALERT_BWQUOTA_ENABLED"":1,
         ""STATUS"":2,
         ""TOTALRAM"":1024,
         ""ALERT_DISKIO_THRESHOLD"":1000,
         ""BACKUPWINDOW"":1,
         ""ALERT_BWOUT_ENABLED"":1,
         ""ALERT_BWOUT_THRESHOLD"":5,
         ""LABEL"":""api-node3"",
         ""ALERT_CPU_ENABLED"":1,
         ""ALERT_BWQUOTA_THRESHOLD"":80,
         ""ALERT_BWIN_THRESHOLD"":5,
         ""BACKUPWEEKLYDAY"":0,
         ""DATACENTERID"":5,
         ""ALERT_CPU_THRESHOLD"":90,
         ""TOTALHD"":40960,
         ""ALERT_DISKIO_ENABLED"":1,
         ""ALERT_BWIN_ENABLED"":1,
         ""LINODEID"":8098,
         ""CREATE_DT"":""2015-09-22 11:33:06.0"",
         ""PLANID"":1,
         ""DISTRIBUTIONVENDOR"": ""Debian"",
         ""ISXEN"":0,
         ""ISKVM"":1
      }
   ]
}
";

            var res = GetRes<Node[]>(json);
            var d0 = res[0];

            Assert.AreEqual(NodeStatus.PoweredOff, d0.Status);
            //TODO: test the other fields
        }

        [TestMethod]
        public void Linode_Config_List()
        {
            const string json = @"
{
   ""ERRORARRAY"":[],
   ""ACTION"":""linode.config.list"",
   ""DATA"":[
      {
         ""helper_disableUpdateDB"":1,
         ""RootDeviceRO"":true,
         ""RootDeviceCustom"":"""",
         ""Label"":""My configuration profile"",
         ""DiskList"":""55319,55590,,55591,55592,,,,"",
         ""LinodeID"":8098,
         ""Comments"":"""",
         ""ConfigID"":31058,
         ""helper_xen"":1,
         ""RunLevel"":""default"",
         ""helper_depmod"":1,
         ""KernelID"":85,
         ""RootDeviceNum"":1,
         ""helper_libtls"":false,
         ""RAMLimit"":0
      },
      {
         ""helper_disableUpdateDB"":1,
         ""RootDeviceRO"":true,
         ""RootDeviceCustom"":"""",
         ""Label"":""test profile"",
         ""DiskList"":"",,,,,,,,"",
         ""LinodeID"":8098,
         ""Comments"":"""",
         ""ConfigID"":31231,
         ""helper_xen"":1,
         ""RunLevel"":""default"",
         ""helper_depmod"":1,
         ""KernelID"":85,
         ""RootDeviceNum"":1,
         ""helper_libtls"":false,
         ""RAMLimit"":0
      }
   ]
}
";

            var res = GetRes<ConfigResponse[]>(json);
            var d0 = res[0];

            Assert.AreEqual(8098, d0.LinodeId);
            Assert.AreEqual(31058, d0.Id);
            Assert.AreEqual("My configuration profile", d0.Label);
            //TODO: test the other fields and elements
        }

        [TestMethod]
        public void Avail_Kernels()
        {
            //THIS WAS INVALID JSON IN THE WEBSITE (missing comma) -_-
            const string json = @"
{
   ""ERRORARRAY"":[],
   ""ACTION"":""avail.kernels"",
   ""DATA"":[
      {
         ""LABEL"":""Latest Legacy (2.6.18.8-linode22)"",
         ""ISXEN"":1,
         ""ISKVM"":0,
         ""ISPVOPS"":0,
         ""KERNELID"":60
      },
      {
         ""LABEL"":""2.6.32.16-linode28"",
         ""ISXEN"":1,
         ""ISKVM"":0,
         ""ISPVOPS"":1,
         ""KERNELID"":123
      },
      {
         ""LABEL"":""pv-grub-x86_32"",
         ""ISXEN"":1,
         ""ISKVM"":0,
         ""ISPVOPS"":0,
         ""KERNELID"":92
      },
      {
         ""LABEL"":""4.0.2-x86_64-linode56"",
         ""ISKVM"":1,
         ""ISXen"":1,
         ""ISPVOPS"":1,
         ""KERNELID"":215
      },
   ]
}
";

            var res = GetRes<KernelResponse[]>(json);
            var d0 = res[0];

            Assert.AreEqual(60, d0.Id);
            Assert.AreEqual("Latest Legacy (2.6.18.8-linode22)", d0.Label);
            //TODO: test the other fields and elements
        }
    }
}
