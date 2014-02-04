using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Austin.Linode
{
    public class ApiParameter
    {
        [JsonProperty("DESCRIPTION")]
        public string Description { get; set; }

        [JsonProperty("TYPE")]
        public ApiParameterType Type { get; set; }

        [JsonProperty("REQUIRED")]
        object RequiredInternals { get; set; }

        public bool Required
        {
            get
            {
                if (RequiredInternals is bool)
                    return (bool)RequiredInternals;
                return ((string)RequiredInternals).Equals("yes", StringComparison.OrdinalIgnoreCase);
            }
        }

        public Type ClrType
        {
            get
            {
                switch (Type)
                {
                    case ApiParameterType.numeric:
                        return Required ? typeof(int) : typeof(int?);
                    case ApiParameterType.@string:
                        return typeof(string);
                    case ApiParameterType.boolean:
                        return Required ? typeof(bool) : typeof(bool?);
                    default:
                        throw new NotImplementedException("Unknown type: " + Type.ToString());
                }
            }
        }

        public bool IsValueType
        {
            get
            {
                switch (Type)
                {
                    case ApiParameterType.numeric:
                        return true;
                    case ApiParameterType.@string:
                        return false;
                    case ApiParameterType.boolean:
                        return true;
                    default:
                        throw new NotImplementedException("Unknown type: " + Type.ToString());
                }
            }
        }
    }

    public enum ApiParameterType
    {
        numeric,
        @string,
        boolean
    }
}
