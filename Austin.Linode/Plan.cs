using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Austin.Linode
{
    public class Plan
    {
        [JsonProperty("PLANID")]
        public int Id { get; set; }

        [JsonProperty("LABEL")]
        public string Label { get; set; }

        [JsonProperty("DISK")]
        public int Disk { get; set; }

        [JsonProperty("RAM")]
        public int RAM { get; set; }

        [JsonProperty("XFER")]
        public int Transfer { get; set; }

        [JsonProperty("PRICE")]
        public decimal Price { get; set; }

        [JsonProperty("AVAIL")]
        public Dictionary<int, int> AvailableAtDatacenters { get; set; }
    }
}
