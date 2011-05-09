using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Austin.Linode
{
    public class Node
    {

        [JsonProperty("BACKUPSENABLED")]
        public bool BackupsEnabled { get; set; }

        [JsonProperty("WATCHDOG")]
        public bool WatchDogEnabled { get; set; }

        [JsonProperty("LPM_DISPLAYGROUP")]
        public string DisplayGroup { get; set; }

        //ALERT_BWQUOTA_ENABLED

        [JsonProperty("STATUS")]
        public int Status { get; set; }

        [JsonProperty("TOTALRAM")]
        public int TotalRam { get; set; }

        //ALERT_DISKIO_THRESHOLD
        //BACKUPWINDOW
        //ALERT_BWOUT_ENABLED
        //ALERT_BWOUT_THRESHOLD

        [JsonProperty("LABEL")]
        public string Label { get; set; }

        //ALERT_CPU_ENABLED
        //ALERT_BWQUOTA_THRESHOLD
        //ALERT_BWIN_THRESHOLD
        //BACKUPWEEKLYDAY

        [JsonProperty("DATACENTERID")]
        public int DatacenterId { get; set; }

        //ALERT_CPU_THRESHOLD

        [JsonProperty("TOTALHD")]
        public int TotalHdd { get; set; }

        //ALERT_DISKIO_ENABLED
        //ALERT_BWIN_ENABLED

        [JsonProperty("LINODEID")]
        public int Id { get; set; }
    }
}
