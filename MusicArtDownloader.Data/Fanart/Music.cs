using MusicArtDownloader.Common;
using MusicArtDownloader.Common.Exceptions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MusicArtDownloader.Data.Fanart
{
    public class Music : IDisposable
    {
        private const string getArtistMask = "http://fanart.tv/webservice/artist/{0}/{1}/xml/";
        private const string getAlbumMask = "http://fanart.tv/webservice/album/{0}/{1}/xml/";
        private readonly string apiKey;
        private readonly HttpClient client;
        private readonly MusicSerializer serializer;
        private readonly string storage;
        private readonly TimeSpan expiry;
        private AsyncCache<string, Artist> cache;

        internal Music(string apiKey, HttpClient client, string storage)
        {
            this.apiKey = apiKey;
            this.storage = storage;
            this.client = client;
            this.expiry = TimeSpan.FromDays(Settings.Default.FanartCacheDays);
            this.serializer = new MusicSerializer();
            this.cache = new AsyncCache<string, Artist>(id =>
                {
                    return GetArtistByMusicBrainzIdInternalAsync(id);
                });
            this.Load();
        }

        public Artist GetArtistByMusicBrainzId(string id)
        {
            return GetArtistByMusicBrainzIdAsync(id).Result;
        }

        public async Task<Artist> GetArtistByMusicBrainzIdAsync(string id)
        {
            var artist = await this.cache.GetValue(id);

            // TODO: reenaable artist expiry
            //if (artist.Retrieved < DateTime.Now - this.expiry)
            //{
            //    this.cache.Remove(id);
            //    artist = await this.cache.GetValue(id);
            //}

            return artist;
        }

        public async Task<Artist> GetArtistByMusicBrainzIdInternalAsync(string id)
        {
            var url = String.Format(getArtistMask, this.apiKey, id);
            return await GetArtistFromUrlAsync(url);
        }

        private async Task<Artist> GetArtistFromUrlAsync(string url)
        {
            using (var stream = await GetStreamAsync(url))
            {
                try
                {
                    var artist = serializer.GetArtist(stream);
                    artist.Retrieved = DateTime.Now;
                    return artist;
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
            return await response.EnsureSuccessStatusCode()
                                 .Content
                                 .ReadAsStreamAsync();
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

        public void Dispose()
        {
            this.Save();
        }

        public void Save()
        {
            lock (this)
            {
                var values = Task.WhenAll(this.cache.Values).Result;

                var fanart = this.GetXmlFromArtistsAsync(values);
                if (File.Exists(this.storage))
                {
                    File.Delete(Path.ChangeExtension(this.storage, "bak"));
                    File.Move(this.storage, Path.ChangeExtension(this.storage, "bak"));
                }

                File.WriteAllText(this.storage, fanart.Result);
            }
        }

        public void Load()
        {
            lock (this)
            {
                this.cache.Clear();
                if (File.Exists(this.storage))
                {
                    var xml = File.ReadAllText(this.storage);
                    var artists = this.GetArtistsFromXml(xml);
                    foreach (var artist in artists)
                    {
                        this.cache.SetValue(artist.Id, artist);
                    }
                }
            }
        }


    }
}
