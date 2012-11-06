using MusicArtDownloader.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicArtDownloader.Data.Fanart
{
    public class Music
    {
        private const string getArtistMask = "http://fanart.tv/webservice/artist/{0}/{1}/xml/";
        private const string getAlbumMask = "http://fanart.tv/webservice/album/{0}/{1}/xml/";
        private readonly string apiKey;
        private readonly MusicSerializer serializer;

        internal Music(string apiKey)
        {
            this.apiKey = apiKey;
            this.serializer = new MusicSerializer();
        }

        public Artist GetArtistByMusicBrainzId(string id)
        {
            var url = String.Format(getArtistMask, this.apiKey, id);
            return GetArtistFromUrl(url);
        }

        private Artist GetArtistFromUrl(string url)
        {
            using (var stream = GetStream(url))
            {
                return serializer.GetArtistFromStream(stream);
            }
        }

        private System.IO.Stream GetStream(string url)
        {
            var client = new System.Net.Http.HttpClient();
            var task = client.GetAsync(url);
            var response = task.Result;
            return response.Content.ReadAsStreamAsync().Result;
        }

        public Artist GetArtistFromXml(string xml)
        {
            using (var sr = new System.IO.StringReader(xml))
            {
                return serializer.GetArtistFromTextReader(sr);
            }
        }
    }
}
