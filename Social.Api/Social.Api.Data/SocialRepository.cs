using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Social.Api.Data.Model;

namespace Social.Api.Data
{
    public class SocialRepository
    {
        private readonly SocialApiContext _context;

        public SocialRepository(SocialApiContext context)
        {
            _context = context;
        }

        public List<PostEntity> GetPosts()
        {
            return _context.Posts.ToList();
        }

        public async Task<UserEntity> RegisterAsync(UserEntity newUser)
        {
            _context.Add(newUser);
            await _context.SaveChangesAsync();
            return newUser;
        }

        public Task<UserEntity> GetUserByUsernameAsync(string username)
        {
            return _context.Users.SingleAsync(u => u.Username == username);
        }
    }
}