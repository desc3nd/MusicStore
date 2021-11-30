using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicStore.Models
{
    public class Track
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public int ArtistId { get; set; }

        public Artist Artist { get; set; }

        public int GenreId { get; set; }

        public Genre Genre { get; set; }    

        public DateTime Date { get; set; }
        public string Duration { get; set; }

    }
}
