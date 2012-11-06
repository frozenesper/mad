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
        private readonly System.Xml.Serialization.XmlSerializer serializer;

        internal Music(string apiKey)
        {
            this.apiKey = apiKey;
            this.serializer = new System.Xml.Serialization.XmlSerializer(typeof(Generated.Fanart));
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
                return GetArtistFromStream(stream);
            }
        }

        private System.IO.Stream GetStream(string url)
        {
            var client = new System.Net.Http.HttpClient();
            var task = client.GetAsync(url);
            var response = task.GetAwaiter().GetResult();
            return response.Content.ReadAsStreamAsync().GetAwaiter().GetResult();
        }

        public Artist GetArtistFromXml(string xml)
        {
            using (var sr = new System.IO.StringReader(xml))
            {
                return GetArtistFromTextReader(sr);
            }
        }

        public Artist GetArtistFromTextReader(System.IO.TextReader reader)
        {
            var o = this.serializer.Deserialize(reader);
            var fanart = (Generated.Fanart)o;
            return GetArtistFromDeserializedObject(fanart);
        }

        public Artist GetArtistFromStream(System.IO.Stream stream)
        {
            var o = this.serializer.Deserialize(stream);
            var fanart = (Generated.Fanart)o;
            return GetArtistFromDeserializedObject(fanart);
        }

        #region Read Artist from Deserialized XML Object

        private Artist GetArtistFromDeserializedObject(Generated.Fanart fanart)
        {
            var music = fanart.music;

            var artist = new Artist();
            artist.Id = music.id;
            artist.Name = music.name;
            artist.Backgrounds = LoadArt(music.artistbackgrounds);
            artist.Albums = LoadAlbums(music.albums);
            artist.Thumbs = LoadArt(music.artistthumbs);
            artist.ClearLogos = LoadArt(music.musiclogos);
            artist.HdClearLogo = LoadArt(music.hdmusiclogos);
            artist.Banners = LoadArt(music.musicbanners);

            return artist;
        }

        private List<Album> LoadAlbums(IEnumerable<Generated.Album> albums)
        {
            return albums.Select(a =>
                new Album()
                {
                    Id = a.id,
                    CdArts = LoadCdArt(a.cdart),
                    Covers = LoadArt(a.albumcover)
                }).ToList();
        }

        private List<Art> LoadArt(IEnumerable<Generated.IArt> list)
        {
            return (list ?? new List<Generated.IArt>()).Select(a =>
                new Art()
                {
                    Id = a.id,
                    Url = new Uri(a.url),
                    Likes = a.likes
                }).ToList();
        }

        private List<CdArt> LoadCdArt(IEnumerable<Generated.ICdArt> list)
        {
            return (list ?? new List<Generated.ICdArt>()).Select(a =>
                new CdArt()
                {
                    Id = a.id,
                    Url = new Uri(a.url),
                    Likes = a.likes,
                    Disc = a.disc,
                    Size = a.size
                }).ToList();
        }

        #endregion
    }
}
