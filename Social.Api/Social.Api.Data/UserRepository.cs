using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Social.Api.Data.Model;

namespace Social.Api.Data
{
    public class UserRepository
    {
        private readonly SocialApiContext _context;

        public UserRepository(SocialApiContext context)
        {
            _context = context;
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

        public async Task<int> GetUseIdByNameAsync(string ownerUsername)
        {
            var foundUser = await _context.Users
                .AsNoTracking()
                .Select(x => new UserEntity{Id = x.Id}
                ).SingleOrDefaultAsync(user => user.Username == ownerUsername);

            if (foundUser == null)
            {
                throw new Exception($"user {ownerUsername} not found");
            }

            return foundUser.Id;
        }
    }
}