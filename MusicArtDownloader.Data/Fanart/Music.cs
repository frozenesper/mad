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
            var fanart = Generated.fanart.Parse(xml);
            var music = fanart.music;

            var artist = new Artist();
            artist.Id = music.id;
            artist.Name = music.name;
            // TODO: finish loading artists
            artist.Albums = LoadAlbums(music.albums);

            return artist;
        }

        private List<Album> LoadAlbums(Generated.Albums albums)
        {
            return albums.album.Select(a =>
                new Album()
                {
                    Id = a.id,
                    CdArts = a.cdart.Select(ca =>
                        new CdArt()
                        {
                            Id = ca.id,
                            Url = ca.url,
                            Likes = ca.likes,
                            Disc = ca.disc,
                            Size = ca.size
                        }).ToList(),
                    Covers = a.albumcover.Select(ac =>
                        new Art()
                        {
                            Id = ac.id,
                            Url = ac.url,
                            Likes = ac.likes
                        }).ToList()
                }).ToList();
        }

        private string GetXml(string url)
        {
            throw new NotImplementedException();
        }
    }
}
