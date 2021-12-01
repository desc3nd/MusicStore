using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MusicStore.Models
{
    public class Album
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        [Display(Name ="Artist")]
        public int ArtistId { get; set; }

        public Artist Artist { get; set; }

        [Required]
        [Display(Name ="Genre")]
        public int GenreId { get; set; }

        public Genre Genre { get; set; }

        [Required]
        [Display(Name = "Year of publish")]
        public int YearOfPublish { get; set; }

        [Required]
        [Display(Name = "Date of publish")]
        public DateTime DateOfPublish { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public int? Amount { get; set; }

        [Required]
        [Display(Name = "Are there swear words?")]
        public bool SwearWords { get; set; }


        public string Description { get; set; }

        public ICollection<Track> Tracks { get; set; }

    }
}
