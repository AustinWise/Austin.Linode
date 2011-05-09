using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using Newtonsoft.Json;

namespace Austin.Linode
{
    public class Linode
    {
        private string apiKey;
        private WebClient wc = new WebClient();
        private static readonly Dictionary<string, string> EmptyDict = new Dictionary<string, string>(0);

        public Linode(string apiKey)
        {
            this.apiKey = apiKey;
            var dic = new Dictionary<int, int>();
        }

        public string GetJson(string apiAction, Dictionary<string, string> args)
        {
            var param = string.Join("&", args.Select(kvp => kvp.Key + "=" + kvp.Value));
            string url = string.Format("https://api.linode.com/?api_key={0}&api_action={1}&{2}", apiKey, apiAction, param);
            return wc.DownloadString(url);
        }

        public T GetResponse<T>(string apiAction, Dictionary<string, string> args)
        {
            string json = GetJson(apiAction, args);
            try
            {
                var ret = JsonConvert.DeserializeObject<Response<T>>(json);
                if (ret.Errors != null && ret.Errors.Length != 0)
                    throw new LinodeException(ret.Errors);
                return ret.Data;
            }
            catch
            {
                var errors = JsonConvert.DeserializeObject<Response<object>>(json).Errors;
                if (errors.Length == 0)
                    throw new Exception(string.Format("The errors array is empty, the '{0}' class is probably not right for the '{1}' API action", typeof(T).Name, apiAction));
                throw new LinodeException(errors);
            }
        }

        public Node[] Linode_List()
        {
            return GetResponse<Node[]>("linode.list", EmptyDict);
        }

        public Job[] Linode_Job_List(int node)
        {
            return GetResponse<Job[]>("linode.job.list", new Dictionary<string, string> { { "LinodeID", node.ToString() } });
        }

        public Job[] Linode_Job_List(int node, int jobId)
        {
            return GetResponse<Job[]>("linode.job.list", new Dictionary<string, string> { { "LinodeID", node.ToString() }, { "JobID", jobId.ToString() } });
        }

        /// <returns>The job id</returns>
        public int Linode_Reboot(int node)
        {
            return GetResponse<JobIdResponse>("linode.reboot", new Dictionary<string, string> { { "LinodeID", node.ToString() } }).JobID;
        }
    }
}
