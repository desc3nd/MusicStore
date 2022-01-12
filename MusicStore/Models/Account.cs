
using System.ComponentModel.DataAnnotations;

namespace MusicStore.Models
{
    public class Account
    {        
        public string Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Surname { get; set; }

        [Required]
        public string Login { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string LoginStatus { get; set; }

    }
}
