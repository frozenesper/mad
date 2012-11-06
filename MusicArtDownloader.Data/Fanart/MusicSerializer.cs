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

        public Artist GetArtistFromTextReader(System.IO.TextReader reader)
        {
            var o = this.serializer.Deserialize(reader);
            var fanart = (Generated.Fanart)o;
            return GetArtistFromDeserializedObject(fanart.music.First());
        }

        public Artist GetArtistFromStream(System.IO.Stream stream)
        {
            var o = this.serializer.Deserialize(stream);
            var fanart = (Generated.Fanart)o;
            return GetArtistFromDeserializedObject(fanart.music.First());
        }

        #region Read Artist from Deserialized XML Object

        private IEnumerable<Artist> GetArtistsFromDeserializedObject(Generated.Fanart fanart)
        {
            foreach (var music in fanart.music)
            {
                yield return GetArtistFromDeserializedObject(music);
            }
        }

        private Artist GetArtistFromDeserializedObject(Generated.Music music)
        {
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

        #region Write Artist to XML Object

        private Generated.Music GetXmlObjectFromArtist(Artist artist)
        {
            var music = new Generated.Music();

            music.id = artist.Id;
            music.name = artist.Name;
            music.artistbackgrounds = LoadBackgrounds(artist.Backgrounds);
            // TODO: switch from read to write
            //artist.Backgrounds = LoadArt(music.artistbackgrounds);
            //artist.Albums = LoadAlbums(music.albums);
            //artist.Thumbs = LoadArt(music.artistthumbs);
            //artist.ClearLogos = LoadArt(music.musiclogos);
            //artist.HdClearLogo = LoadArt(music.hdmusiclogos);
            //artist.Banners = LoadArt(music.musicbanners);

            return music;
        }

        private Generated.ArtistBackgroundsArtistbackground[] LoadBackgrounds(List<Art> list)
        {
            return list.Select(a =>
                new Generated.ArtistBackgroundsArtistbackground()
                {
                    id = a.Id,
                    url = a.Url.ToString(),
                    likes = a.Likes
                }).ToArray();
        }



        #endregion
    }
}
