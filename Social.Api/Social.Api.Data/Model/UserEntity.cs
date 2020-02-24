using System.ComponentModel.DataAnnotations.Schema;
using Social.Api.Infrastructure;

namespace Social.Api.Data.Model
{
    [Table("Users")]
    public class UserEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string AvatarUrl { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public bool IsAuthorized(string candidatePassword)
        {
            var hashedPwd = CredentialsCypher.ToSha256(candidatePassword);
            return Password == hashedPwd;
        }
    }
}