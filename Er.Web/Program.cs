using Er.Core.Extensions;
using Er.Infrastructure.Extensions;

namespace Er.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllersWithViews();
            builder.Services.AddCore();
            builder.Services.AddInfrastructure();

            var app = builder.Build();

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();
            app.MapControllerRoute(name: "default", pattern: "{controller=Offer}/{action=Offers}/{id?}");

            app.Run();
        }
    }
}