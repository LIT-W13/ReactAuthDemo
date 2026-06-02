using Microsoft.AspNetCore.Authentication.Cookies;

namespace ReactAuthDemo.Web;

public class Program
{
    private static string CookieScheme = "ReactAuthDemo";

    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddAuthentication(CookieScheme)
               .AddCookie(CookieScheme, options =>
               {
                   options.Events = new CookieAuthenticationEvents
                   {
                       OnRedirectToLogin = context =>
                       {
                           context.Response.StatusCode = 401;
                           context.Response.ContentType = "application/json";
                           var result = System.Text.Json.JsonSerializer.Serialize(new { error = "You are not authenticated" });
                           return context.Response.WriteAsync(result);
                       }
                   };
               });

        builder.Services.AddSession();

        builder.Services.AddControllersWithViews();

        var app = builder.Build();

        if (!app.Environment.IsDevelopment())
        {
            app.UseHsts();
        }

        //app.UseHttpsRedirection();
        app.UseStaticFiles();
        if (app.Environment.IsDevelopment())
        {
            var psi = new System.Diagnostics.ProcessStartInfo
            {
                FileName = "npm",
                Arguments = "run dev",
                WorkingDirectory = Path.Combine(Directory.GetCurrentDirectory(), "ClientApp"),
                UseShellExecute = true
            };
        
            var spaProcess = System.Diagnostics.Process.Start(psi);
            app.Lifetime.ApplicationStopping.Register(() =>
            {
                if (spaProcess is { HasExited: false })
                {
                    spaProcess.Kill(true);
                }
            });
        }
        app.UseRouting();

        app.UseSession();
        app.UseAuthentication();
        app.UseAuthorization();


        app.MapControllerRoute(
            name: "default",
            pattern: "{controller}/{action=Index}/{id?}");

        app.MapFallbackToFile("index.html");

        app.Run();
    }
}