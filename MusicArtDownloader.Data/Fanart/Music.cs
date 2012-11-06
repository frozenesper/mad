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
        private const string findArtistMask = "http://fanart.tv/webservice/artist/{0}/{1}/xml/";
        private readonly string apiKey;
        internal Music(string apiKey)
        {
            this.apiKey = apiKey;
        }

        public Artist FindArtistByMusicBrainzId(string id)
        {
            var url = String.Format(findArtistMask, this.apiKey, id);
            return ReadArtistFromUrl(url);
        }

        private Artist ReadArtistFromUrl(string url)
        {
            var xml = GetXml(url);
            return ReadArtistFromXml(xml);
        }

        public Artist ReadArtistFromXml(string xml)
        {
            var serializer = new System.Xml.Serialization.XmlSerializer(typeof(Generated.Fanart));

            Generated.Fanart fanart;
            using (var sr = new System.IO.StringReader(xml))
            {
                var o = serializer.Deserialize(sr);
                fanart = (Generated.Fanart)o;
            }

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

        private string GetXml(string url)
        {
            throw new NotImplementedException();
        }
    }
}
