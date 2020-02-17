using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Social.Api.Contracts;
using Social.Api.Data;
using Social.Api.Data.Model;
using Social.Api.Infrastructure;

namespace Social.Api.Controllers
{
    [Route("auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly SocialRepository _socialRepository;

        private readonly LinkGenerator _linker;
        private readonly IMapper _mapper;


        public AuthController(SocialRepository socialRepository, IMapper mapper, LinkGenerator linker)
        {
            _socialRepository = socialRepository;
            _mapper = mapper;
            _linker = linker;
        }

        [HttpPost("register")]
        public async Task<ActionResult<User>> Register(CreateUser newUser)
        {
            if (newUser.Password != newUser.ConfirmPassword)
            {
                return BadRequest(new ApiError() {Error = "[ConfirmPassword] and [Password] must match"});
            }

            var user = _mapper.Map<UserEntity>(newUser);
            user.Password = CredentialsCypher.ToSha256(user.Password);
            var result = await _socialRepository.RegisterAsync(user);
            var link = _linker.GetPathByAction(HttpContext, nameof(Login));
            return Created(link, _mapper.Map<User>(result));
        }


        [HttpPost("login")]
        public async Task<ActionResult<User>> Login(Credentials credentials)
        {
            var user = await _socialRepository.GetUserByUsernameAsync(credentials.Username);
            if (!user.isAuthorized(credentials.Password))
            {
                return Unauthorized("Invalid username or password");
            }

            return Ok(_mapper.Map<User>(user));
        }
    }
}