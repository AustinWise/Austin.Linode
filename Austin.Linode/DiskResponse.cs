/*
 *
 * Copyright (c) 2016, Austin Wise.
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

namespace Austin.Linode
{
    public class DiskResponse
    {
        [JsonProperty("DISKID")]
        public int Id { get; set; }

        [JsonProperty("LABEL")]
        public string Label { get; set; }

        [JsonProperty("TYPE")]
        public string Type { get; set; }

        [JsonProperty("LINODEID")]
        public int LinodeId { get; set; }

        [JsonProperty("ISREADONLY")]
        public bool IsReadonly { get; set; }

        //When I saw this property and it's lack of documentation, I realized that
        //I'm wasting my implementing this studpid API. It's has obviously not been
        //designed (or at least not documented) with any seriously-automated use in mind.
        [JsonProperty("STATUS")]
        public int Status { get; set; }

        [JsonProperty("UPDATE_DT")]
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime UpdatedTime { get; set; }

        [JsonProperty("CREATE_DT")]
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime CreatedTime { get; set; }

        /// <summary>
        /// Size in megabytes, probably.
        /// </summary>
        [JsonProperty("SIZE")]
        public int Size { get; set; }
    }
}
