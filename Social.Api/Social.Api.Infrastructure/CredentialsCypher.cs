using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography;
using System.Text;

namespace Social.Api.Infrastructure
{
    public class CredentialsCypher
    {
        public static string ToSha256(string data)
        {
            // Create a SHA256   
            var sha256Hash = SHA256.Create();
            // ComputeHash - returns byte array  
            var bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(data));

            // Convert byte array to a string   
            var builder = new StringBuilder();
            foreach (var t in bytes)
            {
                builder.Append(t.ToString("x2"));
            }

            return builder.ToString();
        }
    }
}