using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog.Events;
using Serilog;
using WayraWasi.Data;
using WayraWasi.Data.Implementations;
using WayraWasi.Validators;

namespace WayraWasi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .CreateLogger();

            try
            {
                Log.Information("Iniciando...");
                var builder = WebApplication.CreateBuilder(args);

                // Uso del Serilog para el logging
                builder.Host.UseSerilog();

                builder.Services.AddDbContext<DBDapperContext>(options =>
                    options.UseSqlServer(builder.Configuration.GetConnectionString("DBConnection")));

                builder.Services.AddIdentity<IdentityUser, IdentityRole>()
                    .AddEntityFrameworkStores<DBDapperContext>()
                    .AddDefaultTokenProviders();

                builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                    .AddCookie(options =>
                    {
                        options.LoginPath = "/Account/Login";
                        options.LogoutPath = "/Account/Logout";
                        options.AccessDeniedPath = "/Home/Privacy";
                        options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
                        options.Cookie.HttpOnly = true;
                        options.Cookie.IsEssential = true;
                    });

                builder.Services.AddControllersWithViews()
                    .AddFluentValidation(fv =>
                    {
                        fv.RegisterValidatorsFromAssemblyContaining<ReservasValidator>();
                        fv.RegisterValidatorsFromAssemblyContaining<CabaniasValidator>();
                        fv.RegisterValidatorsFromAssemblyContaining<LoginViewModelValidator>();
                        fv.RegisterValidatorsFromAssemblyContaining<RegisterViewModelValidator>();
                    })
                    .ConfigureApiBehaviorOptions(options =>
                    {
                        options.SuppressModelStateInvalidFilter = true;
                    });

                builder.Services.AddAuthorization();
                builder.Services.AddHttpContextAccessor();

                builder.Services.AddScoped<IHomeRepository, HomeRepository>();
                builder.Services.AddScoped<ReservaRepository>();
                builder.Services.AddScoped<CabaniaRepository>();

                builder.Services.AddRazorPages();

                builder.Services.AddDatabaseDeveloperPageExceptionFilter();

                var app = builder.Build();

                // Configure the HTTP request pipeline.
                if (app.Environment.IsDevelopment())
                {
                    app.UseMigrationsEndPoint();
                }
                else
                {
                    app.UseExceptionHandler("/Home/Error");
                    app.UseHsts();
                }

                app.UseStatusCodePagesWithRedirects("/Home/Error/{0}");

                app.UseHttpsRedirection();
                app.UseStaticFiles();

                app.UseRouting();

                app.UseAuthentication();
                app.UseAuthorization();

                app.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                app.MapRazorPages();

                app.Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Error al iniciar la Aplicacion Web");
                throw;
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }
    }
}
