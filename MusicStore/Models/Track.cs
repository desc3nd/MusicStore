using System;
using System.ComponentModel.DataAnnotations;

namespace MusicStore.Models
{
    public class Track
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        [Display(Name = "Artist")]
        public int ArtistId { get; set; }

        public Artist Artist { get; set; }

        [Required]
        public int AlbumId { get; set; }

        [Required]
        [Display(Name = "Genre")]
        public int GenreId { get; set; }

        public Genre Genre { get; set; }

        [Display(Name = "Duration time")]
        public DateTime Time { get; set; }


        [Display(Name = "Swear words?")]
        public bool SwearWords { get; set; }

    }
}
