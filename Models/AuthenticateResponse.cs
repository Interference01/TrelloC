using System.Text.Json.Serialization;
using TrelloC.Models.Entities;

namespace TrelloC.Models
{
    public class AuthenticateResponse
    {
        public Guid Id { get; set; }
        public string Login { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public byte[]? Avatar { get; set; }
        public string JwtToken { get; set; }

        [JsonIgnore] // refresh token is returned in http only cookie
        public string RefreshToken { get; set; }

        public AuthenticateResponse(User user, string jwtToken, string refreshToken)
        {
            Id = user.Id;
            Name = user.Name;
            Login = user.Login; 
            Email = user.Email;
            Avatar = user.Avatar; 
            JwtToken = jwtToken;
            RefreshToken = refreshToken;
        }
    }
}
