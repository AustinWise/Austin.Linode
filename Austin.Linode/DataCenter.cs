using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Austin.Linode
{
    public class DataCenter
    {
        [JsonProperty("DATACENTERID")]
        public int Id { get; set; }

        [JsonProperty("LOCATION")]
        public string Location { get; set; }
    }
}
