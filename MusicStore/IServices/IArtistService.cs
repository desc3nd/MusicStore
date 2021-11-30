using MusicStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicStore.IServices
{
    public interface IArtistService
    {
        public Artist GetArtist(int? id);
    }
}
