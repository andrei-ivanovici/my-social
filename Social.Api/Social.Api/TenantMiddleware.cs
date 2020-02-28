using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace Social.Api.Infrastructure
{
    public class TenantMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly TenantAccessor _accessor;

        public TenantMiddleware(RequestDelegate next, TenantAccessor accessor)
        {
            _next = next;
            _accessor = accessor;
        }

        public Task Invoke(HttpContext context)
        {
            _accessor.ActiveUser = context.Request.Headers["x-user"];
            return _next.Invoke(context);
        }
    }

    public static class Tenants
    {
        public static void UseTenantMiddleware(this IApplicationBuilder source)
        {
            source.UseMiddleware<TenantMiddleware>();
        }
    }
}