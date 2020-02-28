using AutoMapper.Configuration;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Social.Api.Data;
using Social.Api.Infrastructure;
using IConfiguration = Microsoft.Extensions.Configuration.IConfiguration;

namespace Social.Api
{
    public static class SocialServices
    {
        public static IServiceCollection UseSocialServices(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<SocialApiContext>();
            services.Configure<ImageRepoConfiguration>(configuration.GetSection("ImageRepo"));
            services.AddScoped<AuditRepository>();
            services.AddScoped<UserRepository>();
            services.AddScoped<PostRepository>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<TenantAccessor>();
            return services;
        }
    }
}