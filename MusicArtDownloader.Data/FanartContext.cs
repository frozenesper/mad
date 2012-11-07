using MusicArtDownloader.Data.Fanart;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicArtDownloader.Data
{
    /// <summary>
    /// A class that retrieves data through the Fanart.tv API.
    /// </summary>
    public class FanartContext
    {
        private string apiKey;
        private Music music;

        /// <summary>
        /// Initializes a new instance of the Fanart class.
        /// </summary>
        /// <param name="client">HttpClient to use for API requests.</param>
        public FanartContext(System.Net.Http.HttpClient client)
        {
            this.apiKey = ConfigurationManager.AppSettings["api"];
            if (String.IsNullOrWhiteSpace(apiKey))
                throw new ConfigurationErrorsException("api");

            this.music = new Music(this.apiKey, client);
        }

        /// <summary>
        /// Initializes a new instance of the Fanart class.
        /// </summary>
        public FanartContext()
            : this(new System.Net.Http.HttpClient()) { }

        /// <summary>
        /// Gets an object that can make calls to the Fanart.tv Music API.
        /// </summary>
        public Music Music { get { return this.music; } }
    }
}
