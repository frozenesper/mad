using MusicArtDownloader.Common;
using MusicArtDownloader.Common.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MusicArtDownloader.Data.Fanart
{
    public class Music
    {
        private const string getArtistMask = "http://fanart.tv/webservice/artist/{0}/{1}/xml/";
        private const string getAlbumMask = "http://fanart.tv/webservice/album/{0}/{1}/xml/";
        private readonly string apiKey;
        private readonly HttpClient client;
        private readonly MusicSerializer serializer;

        internal Music(string apiKey, HttpClient client)
        {
            this.apiKey = apiKey;
            this.client = client;
            this.serializer = new MusicSerializer();
        }

        public Artist GetArtistByMusicBrainzId(string id)
        {
            var url = String.Format(getArtistMask, this.apiKey, id);
            return GetArtistFromUrl(url);
        }

        public Artist GetAlbumByMusicBrainzId(string id)
        {
            var url = String.Format(getAlbumMask, this.apiKey, id);
            return GetArtistFromUrl(url);
        }

        private Artist GetArtistFromUrl(string url)
        {
            using (var stream = GetStream(url))
            {
                try
                {
                    return serializer.GetArtist(stream);
                }
                catch (FanartException e)
                {
                    throw new ArgumentOutOfRangeException("id", e);
                }
            }
        }

        private System.IO.Stream GetStream(string url)
        {
            var task = this.client.GetAsync(url);
            var response = task.Result;
            return response.Content.ReadAsStreamAsync().Result;
        }

        public Artist GetArtistFromXml(string xml)
        {
            using (var sr = new System.IO.StringReader(xml))
            {
                return serializer.GetArtist(sr);
            }
        }

        public IEnumerable<Artist> GetArtistsFromXml(string xml)
        {
            using (var sr = new System.IO.StringReader(xml))
            {
                return serializer.GetArtists(sr);
            }
        }

        public string GetXmlFromArtist(Artist artist)
        {
            using (var sw = new System.IO.StringWriter())
            {
                serializer.GetFanart(sw, artist);
                return sw.ToString();
            }
        }

        public string GetXmlFromArtists(IEnumerable<Artist> artists)
        {
            using (var sw = new System.IO.StringWriter())
            {
                serializer.GetFanart(sw, artists);
                return sw.ToString();
            }
        }
    }
}
