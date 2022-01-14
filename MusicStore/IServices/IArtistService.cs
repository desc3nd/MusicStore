using MusicStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicStore.IServices
{
    public interface IArtistService
    {
        public ICollection<Artist> GetArtists();

        public Artist GetArtist(int? id);
        public void Create(Artist artist);

        public void Edit(Artist artist);

        public void Delete(int id);

    }
}
