using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Social.Api.Contracts;

namespace Social.Api.Controllers
{
    [ApiController]
    [Route("asset")]
    public class AssetController : ControllerBase
    {
        private readonly ImageRepoConfiguration _repo;
        private readonly ILogger<AssetController> _logger;

        public AssetController(IOptionsMonitor<ImageRepoConfiguration> repo, ILogger<AssetController> logger)
        {
            _repo = repo.CurrentValue;
            _logger = logger;
        }

        [HttpPost("{user}"), DisableRequestSizeLimit]
        public async Task<ActionResult<FileResponse>> AddFile(string user)
        {
            var file = Request.Form.Files[0];
            if (file == null)
            {
                return BadRequest("No file present");
            }

            var fileName = file.Name ?? Guid.NewGuid().ToString();

            var destinationFile = Path.Combine(user, fileName);


            _logger.LogInformation($"Uploaded file file {fileName}");
            return Created(destinationFile,
                new FileResponse()
                {
                    IsSuccess = true,
                    RelativePath = destinationFile
                });
        }
    }
}