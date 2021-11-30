using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicStore.Models
{
    public class Album
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public int ArtistId { get; set; }

        public Artist Artist { get; set; }

        public int GenreId { get; set; }

        public Genre Genre { get; set; }

        public int YearOfPublish { get; set; }

        public DateTime DateOfPublish { get; set; }

        public decimal Price { get; set; }

        public int? Amount { get; set; }

        public int SwearWordsId { get; set; }

        public SwearWords SwearWords { get; set; }

        public string Description { get; set; }

        public ICollection<Track> Tracks { get; set; }

    }
}
