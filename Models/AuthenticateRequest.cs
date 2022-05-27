using System.ComponentModel.DataAnnotations;

namespace TrelloC.Models
{
    public class AuthenticateRequest
    {
        [Required]
        public string Login { get; set; }  
        [Required]
        public string Password { get; set; }

    }
}
