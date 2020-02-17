using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Social.Api.Contracts;
using Social.Api.Contracts.Posts;
using Social.Api.Data;
using Social.Api.Data.Model;

namespace Social.Api.Controllers
{
    [Route("posts")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly PostRepository _repo;
        private readonly IMapper _mapper;

        public PostsController(PostRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<Post>>> GetPosts()
        {
            try
            {
                var posts = await _repo.GetPostsAsync();
                return Ok(posts);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ApiError
                {
                    Error = "Failed to load posts"
                });
            }
        }

        [HttpPost]
        public async Task<Post> AddPost(Post newPost)
        {
            var postEntity = _mapper.Map<PostEntity>(newPost);
            var post = await _repo.AddPostAsync(postEntity, newPost.Owner.Username);
            return _mapper.Map<Post>(post);
        }
    }
}