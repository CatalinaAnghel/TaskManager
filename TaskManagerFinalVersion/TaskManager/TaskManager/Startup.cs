using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TaskManager.ApplicationLogic.Services.Abstractions;
using TaskManager.DataAccess.Data;
using TaskManager.DataAccess.DataModels;
using TaskManager.ApplicationLogic.Services;
using TaskManager.DataAccess.UnitOfWork;

namespace TaskManager
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<TaskManagerDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("TaskManagerConnection")));
            services.AddIdentity<Users, Roles>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<TaskManagerDbContext>().AddDefaultTokenProviders();
            services.Configure<IdentityOptions>(options =>
            {
                // password options
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 8;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = false;

                // lockout options
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

                // email options
                options.User.RequireUniqueEmail = true;
            });

            services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.HttpOnly = true;
                options.LoginPath = "/Identity/Account/Login";
                options.AccessDeniedPath = "/Identity/Account/AccessDenied";
                options.SlidingExpiration = true;
            });
           
            services.AddControllersWithViews();

            // UnitOfWork
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            // Services
            services.AddScoped<ISignInService, SignInService>();
            services.AddScoped<ILoginService, LoginService>();
            services.AddScoped<IProjectsService, ProjectsService>();
            services.AddScoped<IProjectTasksService, ProjectTasksService>();
            services.AddScoped<IUsersService, UsersService>();
            services.AddScoped<ITeamsService, TeamsService>();
            services.AddScoped<IBadgesService, BadgesService>();
            services.AddScoped<IUserBadgesService, UserBadgesService>();
            services.AddScoped<IUserTeamsService, UserTeamsService>();
            services.AddScoped<IImageService, ImageService>();
            services.AddScoped<ISessionService, SessionService>();

            services.AddRazorPages();
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromSeconds(3600);
            });

            
        }

        /*
         * This method gets called by the runtime.
         * Use this method to configure the HTTP request pipeline.
         */
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseSession();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
