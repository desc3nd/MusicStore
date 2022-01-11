using System;

namespace MusicStore.Models
{
    public class Track
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public int ArtistId { get; set; }

        public Artist Artist { get; set; }

        public int AlbumId { get; set; }

        public int GenreId { get; set; }

        public Genre Genre { get; set; }    

        public DateTime Time { get; set; }

        public bool SwearWords { get; set; }

    }
}
