using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicArtDownloader.Common.Exceptions
{
    [Serializable]
    public class FanartException : Exception
    {
        public FanartException() { }
        public FanartException(string message) : base(message) { }
        public FanartException(string message, Exception inner) : base(message, inner) { }
        protected FanartException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}
