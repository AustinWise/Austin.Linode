/**
 *
 * Copyright (c) 2014, Austin Wise.
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
using System.Text;

namespace Austin.Linode
{
    public class ApiParameter
    {
        [JsonProperty("DESCRIPTION")]
        public string Description { get; set; }

        [JsonProperty("TYPE")]
        public ApiParameterType Type { get; set; }

        [JsonProperty("REQUIRED")]
        object RequiredInternals { get; set; }

        public bool Required
        {
            get
            {
                if (RequiredInternals is bool)
                    return (bool)RequiredInternals;
                return ((string)RequiredInternals).Equals("yes", StringComparison.OrdinalIgnoreCase);
            }
        }

        public Type ClrType
        {
            get
            {
                switch (Type)
                {
                    case ApiParameterType.numeric:
                        return Required ? typeof(int) : typeof(int?);
                    case ApiParameterType.@string:
                        return typeof(string);
                    case ApiParameterType.boolean:
                        return Required ? typeof(bool) : typeof(bool?);
                    default:
                        throw new NotImplementedException("Unknown type: " + Type.ToString());
                }
            }
        }

        public bool IsValueType
        {
            get
            {
                switch (Type)
                {
                    case ApiParameterType.numeric:
                        return true;
                    case ApiParameterType.@string:
                        return false;
                    case ApiParameterType.boolean:
                        return true;
                    default:
                        throw new NotImplementedException("Unknown type: " + Type.ToString());
                }
            }
        }
    }

    public enum ApiParameterType
    {
        numeric,
        @string,
        boolean
    }
}
