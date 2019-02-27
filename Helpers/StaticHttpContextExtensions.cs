using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace CapitalFunds.Api.Helpers
{
    public static class StaticHttpContextExtensions
    {

        public static IApplicationBuilder UseStaticHttpContext(this IApplicationBuilder app)
        {
            var httpContextAccessor = app.ApplicationServices.GetRequiredService<IHttpContextAccessor>();
            HttpContext.Configure(httpContextAccessor);
            return app;
        }
    }
}