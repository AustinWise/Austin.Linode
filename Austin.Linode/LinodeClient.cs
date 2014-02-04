using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using Newtonsoft.Json;

namespace Austin.Linode
{
    public partial class LinodeClient
    {
        private string mApiKey;
        private WebClient mWc = new WebClient();

        public LinodeClient(string apiKey)
        {
            this.mApiKey = apiKey;
        }

        string GetJson(string apiAction, Dictionary<string, string> args, bool needsAuth = true)
        {
            if (args == null)
                args = new Dictionary<string, string>();
            args.Add("api_action", apiAction);
            if (needsAuth)
                args.Add("api_key", mApiKey);

            var param = string.Join("&", args.Select(kvp => kvp.Key + "=" + kvp.Value));
            string url = "https://api.linode.com/?" + param;
            return mWc.DownloadString(url);
        }

        T GetResponse<T>(string apiAction, Dictionary<string, string> args, bool needsAuth = true)
        {
            string json = GetJson(apiAction, args, needsAuth);
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

        public ApiSpec Api_Spec()
        {
            return GetResponse<ApiSpec>("api.spec", null, false);
        }
    }
}
