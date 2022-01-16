using MusicStore.Models;
using System.Collections.Generic;


namespace MusicStore.IServices
{
    public interface IGenreService
    {
        public ICollection<Genre> GetGenres();

        public Genre GetGenre(int? id);

        public void Create(Genre genre);

        public void Edit(Genre genre);

        public void Delete(int id);
    }
}
