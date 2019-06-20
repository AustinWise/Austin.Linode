/*
 *
 * Copyright (c) 2019, Austin Wise.
 * All rights reserved.
 *
 * Redistribution and use in source and binary forms, with or without
 * modification, are permitted provided that the following conditions are
 * met:
 *
 *     * Redistributions of source code must retain the above copyright
 *       notice, this list of conditions and the following disclaimer.
 *
 *     * Redistributions in binary form must reproduce the above copyright
 *       notice, this list of conditions and the following disclaimer in the
 *       documentation and/or other materials provided with the distribution.
 *
 *     * Neither the name of the Austin.Linode Project, Austin Wise, nor the names
 *       of its contributors may be used to endorse or promote products
 *       derived from this software without specific prior written
 *       permission.
 *
 * THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS
 * IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO,
 * THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
 * PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER OR
 * CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL,
 * EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO,
 * PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR
 * PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF
 * LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING
 * NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS
 * SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
 */

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

namespace Austin.Linode
{
    public partial class LinodeClient
    {
        private readonly string mApiKey;
        private readonly HttpClient mHttp = new HttpClient();

        public LinodeClient(string apiKey)
        {
            if (string.IsNullOrEmpty(apiKey))
                throw new ArgumentNullException(nameof(apiKey));
            this.mApiKey = apiKey;
        }

        string GetJson(string apiAction, Dictionary<string, string> args, bool needsAuth = true)
        {
            if (args == null)
                args = new Dictionary<string, string>();
            args.Add("api_action", apiAction);
            if (needsAuth)
                args.Add("api_key", mApiKey);

            //TODO: url encode
            var param = string.Join("&", args.Select(kvp => kvp.Key + "=" + kvp.Value));
            string url = "https://api.linode.com/?" + param;
            return mHttp.GetStringAsync(url).GetAwaiter().GetResult();
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
