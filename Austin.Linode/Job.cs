using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Austin.Linode
{
    public class Job
    {
        [JsonProperty("ENTERED_DT")]
        public DateTime Entered { get; set; }

        [JsonProperty("ACTION")]
        public string Action { get; set; }

        [JsonProperty("LABEL")]
        public string Label { get; set; }

        [JsonProperty("HOST_START_DT")]
        public DateTime? HostStart { get; set; }

        [JsonProperty("LINODEID")]
        public int NodeId { get; set; }

        [JsonProperty("HOST_FINISH_DT")]
        public DateTime? HostFinish { get; set; }

        [JsonProperty("DURATION")]
        public int? Duration { get; set; }

        [JsonProperty("HOST_MESSAGE")]
        public string HostMessage { get; set; }

        [JsonProperty("JOBID")]
        public int Id { get; set; }

        [JsonProperty("HOST_SUCCESS")]
        public bool? HostSuccess { get; set; }
    }
}
