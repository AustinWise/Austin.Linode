using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Austin.Linode
{
    [Serializable]
    public class LinodeException : Exception
    {
        public LinodeException(Error[] errors)
            : base(errors.Length == 1 ? errors[0].Message : "Several errors occured.")
        {
            this.Errors = errors;
        }
        protected LinodeException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }

        public Error[] Errors { get; private set; }
    }
}
