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
            mCaseCorrections["addprivate"] = "AddPrivate";
            mCaseCorrections["linodeplans"] = "LinodePlans";
            mCaseCorrections["webconsoletoken"] = "WebConsoleToken";
            mCaseCorrections["estimateinvoice"] = "EstimateInvoice";
            mCaseCorrections["updatecard"] = "UpdateCard";
            mCaseCorrections["stackscript"] = "StackScript";
            mCaseCorrections["stackscripts"] = "StackScripts";
            mCaseCorrections["createfromstackscript"] = "CreateFromStackScript";
            mCaseCorrections["getapikey"] = "GetApiKey";
            mCaseCorrections["createfromdistribution"] = "CreateFromDistribution";
            mCaseCorrections["paybalance"] = "PayBalance";

            mReturnType["linode.list"] = typeof(Node[]);
            mReturnType["linode.job.list"] = typeof(Job[]);
            mReturnType["avail.linodeplans"] = typeof(Plan[]);
            mReturnType["avail.datacenters"] = typeof(DataCenter[]);
            mReturnType["linode.reboot"] = typeof(JobIdResponse);
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
