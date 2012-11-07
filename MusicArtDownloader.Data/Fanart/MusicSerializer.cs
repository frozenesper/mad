using MusicArtDownloader.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicArtDownloader.Data.Fanart
{
    internal class MusicSerializer
    {
        private readonly System.Xml.Serialization.XmlSerializer serializer;

        internal MusicSerializer()
        {
            this.serializer = new System.Xml.Serialization.XmlSerializer(typeof(Generated.Fanart));
        }

        public Artist GetArtist(System.IO.Stream stream)
        {
            var o = this.serializer.Deserialize(stream);
            var fanart = (Generated.Fanart)o;
            return GetArtist(fanart.music.First());
        }

        public IEnumerable<Artist> GetArtists(System.IO.Stream stream)
        {
            var o = this.serializer.Deserialize(stream);
            var fanart = (Generated.Fanart)o;
            return GetArtists(fanart);
        }

        public Artist GetArtist(System.IO.TextReader reader)
        {
            var o = this.serializer.Deserialize(reader);
            var fanart = (Generated.Fanart)o;
            return GetArtist(fanart.music.First());
        }

        public IEnumerable<Artist> GetArtists(System.IO.TextReader reader)
        {
            var o = this.serializer.Deserialize(reader);
            var fanart = (Generated.Fanart)o;
            return GetArtists(fanart);
        }

        public System.IO.Stream GetFanart(System.IO.Stream stream, Artist artist)
        {
            return GetFanart(stream, new Artist[] { artist });
        }

        public System.IO.Stream GetFanart(System.IO.Stream stream, IEnumerable<Artist> artists)
        {
            var fanart = GetFanart(artists);
            this.serializer.Serialize(stream, fanart);
            return stream;
        }

        public System.IO.TextWriter GetFanart(System.IO.TextWriter writer, Artist artist)
        {
            return GetFanart(writer, new Artist[] { artist });
        }

        public System.IO.TextWriter GetFanart(System.IO.TextWriter writer, IEnumerable<Artist> artists)
        {
            var fanart = GetFanart(artists);
            this.serializer.Serialize(writer, fanart);
            return writer;
        }

        #region Read Artist from Deserialized XML Object

        private IEnumerable<Artist> GetArtists(Generated.Fanart fanart)
        {
            foreach (var music in fanart.music)
            {
                yield return GetArtist(music);
            }
        }

        private Artist GetArtist(Generated.Music music)
        {
            var artist = new Artist();
            artist.Id = music.id;
            artist.Name = music.name;
            artist.Backgrounds = ConvertArt(music.artistbackgrounds);
            artist.Albums = ConvertAlbums(music.albums);
            artist.Thumbs = ConvertArt(music.artistthumbs);
            artist.ClearLogos = ConvertArt(music.musiclogos);
            artist.HdClearLogo = ConvertArt(music.hdmusiclogos);
            artist.Banners = ConvertArt(music.musicbanners);

            return artist;
        }

        private List<Album> ConvertAlbums(IEnumerable<Generated.Album> albums)
        {
            return albums.Select(a =>
                new Album()
                {
                    Id = a.id,
                    CdArts = ConvertCdArt(a.cdart),
                    Covers = ConvertArt(a.albumcover)
                }).ToList();
        }

        private List<Art> ConvertArt(IEnumerable<Generated.IArt> list)
        {
            return (list ?? new List<Generated.IArt>()).Select(a =>
                new Art()
                {
                    Id = a.id,
                    Url = new Uri(a.url),
                    Likes = a.likes
                }).ToList();
        }

        private List<CdArt> ConvertCdArt(IEnumerable<Generated.ICdArt> list)
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

        #region Write Artist to XML Object

        private Generated.Fanart GetFanart(IEnumerable<Artist> artists)
        {
            var fanart = new Generated.Fanart();
            
            var musics = new List<Generated.Music>();
            foreach (var artist in artists)
            {
                musics.Add(GetMusic(artist));
            }

            fanart.music = musics.ToArray();
            return fanart;
        }

        private Generated.Music GetMusic(Artist artist)
        {
            var music = new Generated.Music();

            music.id = artist.Id;
            music.name = artist.Name;
            music.artistbackgrounds = ConvertArt<Generated.ArtistBackgroundsArtistbackground>(artist.Backgrounds);
            music.albums = ConvertAlbums(artist.Albums);
            music.artistthumbs = ConvertArt<Generated.ArtistThumbsArtistthumb>(artist.Thumbs);
            music.musiclogos = ConvertArt<Generated.MusicLogosMusiclogo>(artist.ClearLogos);
            music.hdmusiclogos = ConvertArt<Generated.HDMusicLogosHdmusiclogo>(artist.HdClearLogo);
            music.musicbanners = ConvertArt<Generated.MusicBannersMusicbanner>(artist.Banners);

            return music;
        }

        private Generated.Album[] ConvertAlbums(IEnumerable<Album> list)
        {
            return list.Select(a =>
                new Generated.Album()
                {
                    id = a.Id,
                    cdart = ConvertCdArt<Generated.AlbumCdart>(a.CdArts),
                    albumcover = ConvertArt<Generated.AlbumAlbumcover>(a.Covers)
                }).ToArray();
        }

        private T[] ConvertArt<T>(IEnumerable<Art> list) where T : Generated.IArt, new()
        {
            return list.Select(a =>
                new T()
                {
                    id = a.Id,
                    url = a.Url.ToString(),
                    likes = a.Likes
                }).ToArray();
        }

        private T[] ConvertCdArt<T>(IEnumerable<CdArt> list) where T : Generated.ICdArt, new()
        {
            return list.Select(a =>
                new T()
                {
                    id = a.Id,
                    url = a.Url.ToString(),
                    likes = a.Likes,
                    disc = a.Disc,
                    size = a.Size
                }).ToArray();
        }
        
        #endregion
    }
}
