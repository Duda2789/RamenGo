using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System.Net;

namespace RamenGoAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            if (builder.Environment.IsDevelopment())
            {

                builder.WebHost.ConfigureKestrel(serverOptions =>
                {
                    serverOptions.Listen(IPAddress.Any, 5000);
                });
            }
            else
            {
                builder.WebHost.ConfigureKestrel(serverOptions =>
                {
                    serverOptions.Listen(IPAddress.Any, 5001, listenOptions =>
                    {
                        listenOptions.UseHttps();
                    });
                });
            }

            builder.Services.AddControllersWithViews();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            if (!app.Environment.IsDevelopment())
            {
                app.UseHttpsRedirection();
            }

            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapControllers();
            });

            app.Run();
        }
    }
}
