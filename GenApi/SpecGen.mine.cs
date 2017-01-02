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

using Austin.Linode;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace GenApi
{
    partial class SpecGen
    {
        Dictionary<string, string> mCaseCorrections = new Dictionary<string, string>();
        Dictionary<string, Type> mReturnType = new Dictionary<string, Type>();
        ApiSpec SPEC;
        public SpecGen(ApiSpec spec)
        {
            base.ToStringHelper.FormatProvider = CultureInfo.InvariantCulture;

            this.SPEC = spec;

            mCaseCorrections["nodebalancer"] = "NodeBalancer";
            mCaseCorrections["nodebalancers"] = "NodeBalancers";
            mCaseCorrections["addprivate"] = "AddPrivate";
            mCaseCorrections["addpublic"] = "AddPublic";
            mCaseCorrections["setrdns"] = "SetRDns";
            mCaseCorrections["linodeplans"] = "LinodePlans";
            mCaseCorrections["webconsoletoken"] = "WebConsoleToken";
            mCaseCorrections["estimateinvoice"] = "EstimateInvoice";
            mCaseCorrections["updatecard"] = "UpdateCard";
            mCaseCorrections["stackscript"] = "StackScript";
            mCaseCorrections["stackscripts"] = "StackScripts";
            mCaseCorrections["createfromstackscript"] = "CreateFromStackScript";
            mCaseCorrections["createfromimage"] = "CreateFromImage";
            mCaseCorrections["getapikey"] = "GetApiKey";
            mCaseCorrections["createfromdistribution"] = "CreateFromDistribution";
            mCaseCorrections["paybalance"] = "PayBalance";

            mReturnType["linode.job.list"] = typeof(Job[]);
            mReturnType["avail.linodeplans"] = typeof(Plan[]);
            mReturnType["avail.datacenters"] = typeof(DataCenter[]);
            mReturnType["avail.distributions"] = typeof(DistributionResponse[]);
            mReturnType["avail.kernels"] = typeof(KernelResponse[]);

            mReturnType["linode.boot"] = typeof(JobIdResponse);
            mReturnType["linode.clone"] = typeof(LinodeIdResponse);
            mReturnType["linode.create"] = typeof(LinodeIdResponse);
            mReturnType["linode.delete"] = typeof(LinodeIdResponse);
            mReturnType["linode.list"] = typeof(Node[]);
            mReturnType["linode.reboot"] = typeof(JobIdResponse);
            mReturnType["linode.shutdown"] = typeof(JobIdResponse);
            mReturnType["linode.update"] = typeof(LinodeIdResponse);

            mReturnType["linode.config.create"] = typeof(ConfigIdResponse);
            mReturnType["linode.config.delete"] = typeof(ConfigIdResponse);
            mReturnType["linode.config.list"] = typeof(ConfigResponse[]);
            mReturnType["linode.config.update"] = typeof(ConfigIdResponse);

            mReturnType["linode.disk.create"] = typeof(DiskIdResponse);
            mReturnType["linode.disk.createfromdistribution"] = typeof(DiskIdResponse);
            mReturnType["linode.disk.createfromimage"] = typeof(DiskIdResponse);
            mReturnType["linode.disk.createfromstackscript"] = typeof(DiskIdResponse);
            mReturnType["linode.disk.delete"] = typeof(DiskIdResponse);
            mReturnType["linode.disk.duplicate"] = typeof(DiskIdResponse);
            mReturnType["linode.disk.imagize"] = typeof(ImageIdResponse);
            mReturnType["linode.disk.list"] = typeof(DiskResponse[]);
            mReturnType["linode.disk.resize"] = typeof(DiskIdResponse);
            //mReturnType["linode.disk.update"] = typeof(); //This does not have a jobid?

            mReturnType["linode.ip.addprivate"] = typeof(IpAddressResponse);
            mReturnType["linode.ip.addpublic"] = typeof(IpAddressResponse);
            mReturnType["linode.ip.list"] = typeof(IpAddressListEntry[]);
            //mReturnType["linode.ip.setrdns"] = typeof();
            //mReturnType["linode.ip.swap"] = typeof();
        }

        static IEnumerable<KeyValuePair<string, ApiParameter>> SortParams(Dictionary<string, ApiParameter> map)
        {
            return map.OrderBy(kvp => kvp, new ParamComparer());
        }

        class ParamComparer : IComparer<KeyValuePair<string, ApiParameter>>
        {
            public int Compare(KeyValuePair<string, ApiParameter> x, KeyValuePair<string, ApiParameter> y)
            {
                if (x.Value.Required != y.Value.Required)
                    return x.Value.Required ? -1 : 1;
                return x.Key.CompareTo(y.Key);
            }
        }

        string MethodName(string name)
        {
            return string.Join("_", name.Split('.').Select(n => mCaseCorrections.ContainsKey(n) ? mCaseCorrections[n] : char.ToUpper(n[0]) + n.Substring(1)));
        }

        static string ParamDec(string name, ApiParameter param)
        {
            var sb = new StringBuilder();
            var clrType = param.ClrType;
            sb.Append(PrettyPrintType(clrType));
            sb.Append(' ');
            sb.Append(name);

            if (!param.Required)
            {
                sb.Append(" = null");
            }

            return sb.ToString();
        }

        static string PrettyPrintType(Type t)
        {
            if (t == typeof(void))
                return "void";
            if (t == typeof(object))
                return "object";
            if (t == typeof(string))
                return "string";
            if (t == typeof(int))
                return "int";
            if (t == typeof(bool))
                return "bool";
            if (t.IsGenericType && t.GetGenericTypeDefinition() == typeof(Nullable<>))
                return PrettyPrintType(t.GetGenericArguments()[0]) + "?";
            return t.FullName;
        }

        static string ToStringPostfix(ApiParameter param)
        {
            var sb = new StringBuilder();
            if (param.IsValueType && !param.Required)
                sb.Append(".Value");
            switch (param.Type)
            {
                case ApiParameterType.@string:
                    break;
                case ApiParameterType.boolean:
                    sb.Append(" ? \"true\" : \"false\"");
                    break;
                case ApiParameterType.numeric:
                    sb.Append(".ToString(CultureInfo.InvariantCulture)");
                    break;
                default:
                    throw new NotImplementedException("Unknown type: " + param.Type);
            }
            return sb.ToString();
        }
    }
}
