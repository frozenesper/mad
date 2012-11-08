using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MusicArtDownloader.Data
{
    public class ImageRetrievalContext
    {
        private HttpClient client;
        
        /// <summary>
        /// Initializes a new instance of the ImageRetrievalContext class.
        /// </summary>
        /// <param name="client">HttpClient to use for requests.</param>
        /// <param name="disposeClient">true if the client should be disposed of by Dispose(), false if you intent to reuse the client.</param>
        public ImageRetrievalContext(HttpClient client = null, bool disposeClient = false)
        {
            if (client == null)
            {
                client = new HttpClient();
                disposeClient = true;
            }

            if (disposeClient)
                this.client = client;
        }

        public void Dispose()
        {
            if (this.client != null)
                this.client.Dispose();            
        }
    }
}
