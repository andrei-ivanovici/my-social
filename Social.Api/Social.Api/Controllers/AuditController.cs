using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Social.Api.Contracts;
using Social.Api.Data;

namespace Social.Api.Controllers
{
    [ApiController]
    [Route("audit")]
    public class AuditController : ControllerBase
    {
        private readonly AuditRepository _repo;

        public AuditController(AuditRepository repo)
        {
            _repo = repo;
        }

        public ActionResult<List<Audit>> GetAudit()
        {
            return Ok(_repo.GetEventsAsync());
        }
    }
}