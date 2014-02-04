using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Austin.Linode
{
    public class ApiSpec
    {
        [JsonProperty("VERSION")]
        public string Version { get; set; }

        [JsonProperty("METHODS")]
        public Dictionary<string, ApiMethod> Methods { get; set; }
    }
}
