using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace TrelloC.Models.Entities
{
    public class User : BaseEntity
    {
        [Required]
        public string Login { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        public byte[]? Avatar { get; set; }
        [JsonIgnore]
        [Required]
        public string Password { get; set; }

        [JsonIgnore]
        public List<RefreshToken> RefreshTokens { get; set; }
    }
}
