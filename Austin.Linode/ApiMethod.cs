using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Austin.Linode
{
    public class ApiMethod
    {
        [JsonProperty("DESCRIPTION")]
        public string Description { get; set; }

        [JsonProperty("PARAMETERS")]
        public Dictionary<string, ApiParameter> Parameters { get; set; }

        [JsonProperty("THROWS")]
        public string Throws { get; set; }

        public bool HasParameters
        {
            get { return Parameters.Count != 0; }
        }
    }
}
