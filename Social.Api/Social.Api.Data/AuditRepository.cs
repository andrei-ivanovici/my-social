using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Social.Api.Data.Model;

namespace Social.Api.Data
{
    public class AuditRepository
    {
        private readonly SocialApiContext _context;

        public AuditRepository(SocialApiContext context)
        {
            _context = context;
        }

        public async Task AddEvent(AuditEntity newEvent)
        {
            await _context.Auidits.AddAsync(newEvent);
        }

        public Task<List<AuditEntity>> GetEventsAsync()
        {
            return _context.Auidits.ToListAsync();
        }
    }
}