using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Social.Api.Contracts.Posts;
using Social.Api.Data.Model;

namespace Social.Api.Data
{
    public class PostRepository
    {
        private readonly SocialApiContext _context;

        public PostRepository(SocialApiContext context)
        {
            _context = context;
        }

        public Task<List<Post>> GetPostsAsync()
        {
            return _context.Posts.AsNoTracking()
                .Include(post => post.User)
                .Select(entity => new Post()
                {
                    CreatedOn = entity.CreatedOn,
                    Text = entity.Text,
                    Owner = new EntryOwner
                    {
                        Name = entity.User.Name,
                        Username = entity.User.Username
                    }
                })
                .OrderByDescending(post => post.CreatedOn)
                .ToListAsync();
        }

        public async Task<PostEntity> AddPostAsync(PostEntity post, string ownerUsername)
        {
            var owner = await _context.Users
                .AsNoTracking()
                .Select(x => new UserEntity
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Username = x.Username,
                    }
                ).SingleOrDefaultAsync(user => user.Username == ownerUsername);
            if (owner == null)
            {
                throw new Exception("Not Found");
            }

            _context.Add(post);

            post.User = owner;
            await _context.SaveChangesAsync();
            return post;
        }
    }
}