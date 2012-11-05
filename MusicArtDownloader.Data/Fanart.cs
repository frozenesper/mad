using MusicArtDownloader.Data.FanartApis;
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
    public class Fanart
    {
        private static readonly Lazy<Fanart> instance = new Lazy<Fanart>(() => new Fanart());
        private string apiKey;
        private Music music;

        /// <summary>
        /// Initializes a new instance of the Fanart class.
        /// </summary>
        private Fanart()
        {
            this.apiKey = ConfigurationManager.AppSettings["api"];
            if (String.IsNullOrWhiteSpace(apiKey))
                throw new ConfigurationErrorsException("api");

            this.music = new Music(this.apiKey);
        }

        /// <summary>
        /// Gets the main instance of the Fanart object.
        /// </summary>
        public static Fanart Instance { get { return instance.Value; } }

        /// <summary>
        /// Gets an object that can make calls to the Fanart.tv Music API.
        /// </summary>
        public Music Music { get { return this.music; } }
    }
}
