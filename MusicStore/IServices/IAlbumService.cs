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
        /// <summary>
        /// Metoda zwracająca wszystkie albumy z db
        /// </summary>
        /// <returns></returns>
        public ICollection<Album> GetAlbums();

        /// <summary>
        /// metoda która edytuje zadany album i zapisuje zmiany w bazie
        /// </summary>
        /// <param name="album"></param>
        public void Edit(Album album);

        /// <summary>
        /// please add relative comment
        /// </summary>
        /// <param name="album"></param>
        public void AddAlbumWithoutSpotifyAPI(Album album);

        /// <summary>
        /// zwraca wszystkie albumy o określonym tytule
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        public ICollection<Album> SearchAlbumsByTitle(string title);

        /// <summary>
        /// zwraca wszystkie albumy o określonym artyście
        /// </summary>
        /// <param name="artist"></param>
        /// <returns></returns>
        public ICollection<Album> SearchAlbumsByArtist(string artist);

        /// <summary>
        /// Usuwa album o określonym id z db
        /// </summary>
        /// <param name="id"></param>
        public void DeleteAlbum(int id);
    }
}
