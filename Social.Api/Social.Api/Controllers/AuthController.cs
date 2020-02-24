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
        private readonly LinkGenerator _linker;
        private readonly UserRepository _userRepository;
        private readonly IMapper _mapper;


        public AuthController(UserRepository userRepository, IMapper mapper, LinkGenerator linker)
        {
            _userRepository = userRepository;
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
            var result = await _userRepository.RegisterAsync(user);
            var link = _linker.GetPathByAction(HttpContext, nameof(Login));
            return Created(link, _mapper.Map<User>(result));
        }


        [HttpPost("login")]
        public async Task<ActionResult<User>> Login(Credentials credentials)
        {
            var user = await _userRepository.GetUserByUsernameAsync(credentials.Username);
            if (!user.IsAuthorized(credentials.Password))
            {
                return Unauthorized("Invalid username or password");
            }

            return Ok(_mapper.Map<User>(user));
        }
    }
}