using System.ComponentModel.DataAnnotations;

namespace FirebaseMVC.Models.Auth
{
    public class Credentials
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
