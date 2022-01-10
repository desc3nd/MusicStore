using MusicStore.Models;
using System.Collections.Generic;


namespace MusicStore.IServices
{
    public interface IGenreService
    {
        public ICollection<Genre> GetGenres();
        public void AddGenre(string genre);

        public Genre GetGenre(int id);
    }
}
