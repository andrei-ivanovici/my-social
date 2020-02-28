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
        private readonly UserRepository _userRepository;
        private readonly AuditRepository _auditRepository;

        public PostRepository(SocialApiContext context, UserRepository userRepository, AuditRepository auditRepository)
        {
            _context = context;
            _userRepository = userRepository;
            _auditRepository = auditRepository;
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
            var useId = await _userRepository.GetUseIdByNameAsync(ownerUsername);

            _context.Add(post);

            post.UserId = useId;
            await _context.SaveChangesAsync();
            await _auditRepository.AddEvent(new AuditEntity()
            {
                Date = DateTime.Now,
                Event = "New post added",
                User = ownerUsername
            });
            return post;
        }

        public List<PostEntity> GetPosts()
        {
            return _context.Posts.ToList();
        }
    }
}