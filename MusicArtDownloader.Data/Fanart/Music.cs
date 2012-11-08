using MusicArtDownloader.Common;
using MusicArtDownloader.Common.Exceptions;
using System;
using System.Collections.Generic;
using System.IO;
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
            return GetArtistByMusicBrainzIdAsync(id).Result;
        }

        public async Task<Artist> GetArtistByMusicBrainzIdAsync(string id)
        {
            var url = String.Format(getArtistMask, this.apiKey, id);
            return await GetArtistFromUrl(url);
        }

        public Artist GetAlbumByMusicBrainzId(string id)
        {
            return GetAlbumByMusicBrainzIdAsync(id).Result;
        }

        public async Task<Artist> GetAlbumByMusicBrainzIdAsync(string id)
        {
            var url = String.Format(getAlbumMask, this.apiKey, id);
            return await GetArtistFromUrl(url);
        }

        private async Task<Artist> GetArtistFromUrl(string url)
        {
            using (var stream = await GetStreamAsync(url))
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

        private async Task<Stream> GetStreamAsync(string url)
        {
            var response = await this.client.GetAsync(url);
            return await response.Content.ReadAsStreamAsync();
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
            return GetArtistsFromXmlAsync(xml).Result;
        }

        public async Task<IEnumerable<Artist>> GetArtistsFromXmlAsync(string xml)
        {
            using (var sr = new System.IO.StringReader(xml))
            {
                return await Task.Run(() => serializer.GetArtists(sr));
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
            return GetXmlFromArtistsAsync(artists).Result;
        }

        public async Task<string> GetXmlFromArtistsAsync(IEnumerable<Artist> artists)
        {
            using (var sw = new System.IO.StringWriter())
            {
                await Task.Run(() => serializer.GetFanart(sw, artists));
                return sw.ToString();
            }
        }
    }
}
