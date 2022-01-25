using MusicStore.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace MusicStore.IServices
{
    public interface IAlbumService
    {
        public ICollection<Album> GetAlbums();

        public void Edit(Album album);

        /// <param name="album"></param>
        public void AddAlbumWithoutSpotifyAPI(Album album);

        /// <param name="title"></param>
        /// <returns></returns>
        public ICollection<Album> SearchAlbumsByTitle(string title);

        /// <param name="artist"></param>
        /// <returns></returns>
        public ICollection<Album> SearchAlbumsByArtist(string artist);

        /// <param name="id"></param>
        public void DeleteAlbum(int id);
    }
}
