
using System.ComponentModel.DataAnnotations;

namespace MusicStore.Models
{
    public class Account
    {        
        public int Id { get; set; }

        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Surname")]
        public string Surname { get; set; }

        [Required]
        [Display(Name = "Login")]
        public string Login { get; set; }

        [Required]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required]
        public string LoginStatus { get; set; }

        public static bool isLoggedIn { get; set; }

    }
}
