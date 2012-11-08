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
        public ImageRetrievalContext(System.Net.Http.HttpClient client, bool disposeClient)
        {
            if (disposeClient)
                this.client = client;
        }

        /// <summary>
        /// Initializes a new instance of the ImageRetrievalContext class.
        /// </summary>
        /// <param name="client">HttpClient to use for API requests.</param>
        public ImageRetrievalContext(System.Net.Http.HttpClient client)
            : this(client, false) { }

        /// <summary>
        /// Initializes a new instance of the ImageRetrievalContext class.
        /// </summary>
        public ImageRetrievalContext()
            : this(new System.Net.Http.HttpClient(), true) { }

        public void Dispose()
        {
            if (this.client != null)
                this.client.Dispose();            
        }
    }
}
