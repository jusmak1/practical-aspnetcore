using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Primitives;
using Microsoft.AspNetCore;
using Microsoft.Extensions.Hosting;

namespace PracticalAspNetCore
{
    public class Startup
    {
        public void Configure(IApplicationBuilder app)
        {
            //These are the three default services available at Configure
            app.Run(async context =>
            {
                context.Response.Headers.Add("Content-Type", "text/html");

                StringValues queryString = context.Request.Query["message"];

                await context.Response.WriteAsync("<html><body>");
                await context.Response.WriteAsync("<h1>Query String with multiple values</h1>");
                await context.Response.WriteAsync(@"<a href=""?message=hello&message=world&message=again"">click this link to add query string</a><br/><br/>");
                await context.Response.WriteAsync("<ul>");
                foreach (string v in queryString)
                {
                    await context.Response.WriteAsync($"<li>{v}</li>");
                }
                await context.Response.WriteAsync("</ul>");
                await context.Response.WriteAsync("</body></html>");
            });
        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                    webBuilder.UseStartup<Startup>()
                );
    }
}