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
    }
}
