using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Data;
using WayraWasi.Data;
using WayraWasi.Data.Implementations;
using WayraWasi.Models;

namespace WayraWasi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddScoped<DBDapperContext>(provider =>
            {
                var connectionDapper = builder.Configuration.GetConnectionString("DBConnection");
                return new DBDapperContext(connectionDapper);
            });

            builder.Services.AddScoped<IHomeRepository, HomeRepository>();
            builder.Services.AddScoped<ReservaRepository>();
            builder.Services.AddScoped<CabaniaRepository>();

            builder.Services.AddRazorPages();

            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services.AddControllersWithViews();

            //Dapper Connection

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            app.MapRazorPages();

            app.Run();
        }
    }
}
