using MusicArtDownloader.Data.Fanart;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MusicArtDownloader.Data
{
    /// <summary>
    /// A class that retrieves data through the Fanart.tv API.
    /// </summary>
    public class FanartContext : IDisposable
    {
        private const string musicStorageFile = "music.xml";
        private readonly HttpClient client;
        private readonly string apiKey;
        private readonly Music music;

        /// <summary>
        /// Initializes a new instance of the Fanart class.
        /// </summary>
        /// <param name="client">HttpClient to use for API requests.</param>
        /// <param name="disposeClient">true if the client should be disposed of by Dispose(), false if you intent to reuse the client.</param>
        public FanartContext(string storage = null, HttpClient client = null, bool disposeClient = false)
        {
            this.apiKey = ConfigurationManager.AppSettings["api"];
            if (String.IsNullOrWhiteSpace(apiKey))
                throw new ConfigurationErrorsException("api");

            storage = storage ?? Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData, 
                                                                   Environment.SpecialFolderOption.Create);

            storage = System.IO.Path.Combine(storage, "MusicArtDownloader");
            System.IO.Directory.CreateDirectory(storage);
            storage = System.IO.Path.Combine(storage, musicStorageFile);
            
            if (client == null)
            {
                client = new HttpClient();
                disposeClient = true;
            }

            if (disposeClient)
                this.client = client;

            this.music = new Music(apiKey: this.apiKey,
                                   client: client,
                                   storage: storage);
        }

        /// <summary>
        /// Gets an object that can make calls to the Fanart.tv Music API.
        /// </summary>
        public Music Music { get { return this.music; } }

        public void Dispose()
        {
            if (this.client != null)
                this.client.Dispose();

            this.music.Dispose();
        }
    }
}
