using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using MyResume.WebApp.Models;
using Microsoft.AspNetCore.Identity;
using Portfolio_Website_Core.Utilities.MailService;

namespace MyResume.WebApp
{
    public class Startup
    {
        private readonly IConfiguration _config;

        public Startup(IConfiguration configuration)
        {
            _config = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContextPool<AppDbContext>(options => options.UseSqlServer(_config.GetConnectionString("MyResumeDBConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequiredLength = 5;
                options.Password.RequiredUniqueChars = 1;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireDigit = true;

                options.User.RequireUniqueEmail = true;

                options.SignIn.RequireConfirmedEmail = true;

                options.Lockout.MaxFailedAccessAttempts = 2;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(3); // default is 5 min


            });

            services.AddMvc(options => options.EnableEndpointRouting = false);

            services.AddScoped<IUserInfoRepo, UserInfoRepoSQL>();
            // services.AddScoped<ICommentRepository, SQLCommentRepository>();

            //  services.AddSingleton<IAuthorizationHandler, CanEditOnluOtherAdminRolesAndClaimsHandler>();
            //  services.AddSingleton<IAuthorizationHandler, SuperAdminHandler>();
            //  services.AddSingleton<DataProtectionPurposeStrings>();

            services.AddTransient<IMessageService, MessageService>();


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();

                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseStatusCodePagesWithReExecute("/Error/{0}"); // -> /Controller(error)/Action()  The {0} is filled in with the Error code
            }

            app.UseHttpsRedirection(); // http redirected to httpS
            app.UseStaticFiles();

            app.UseAuthentication();
            app.UseAuthorization();

            // Serves files from outside wwwroot
            //app.UseStaticFiles(new StaticFileOptions
            //{
            //    FileProvider = new PhysicalFileProvider(
            //Path.Combine(Directory.GetCurrentDirectory(), "MyStaticFiles")),
            //    RequestPath = "/StaticFiles"
            //});

            app.UseMvcWithDefaultRoute(); // Terminal Middleware
        }
    }
}
