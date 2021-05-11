using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Linq;
using BlazorPoll.Server.Dal;
using BlazorPoll.Server.Data;
using BlazorPoll.Server.Hubs;
using BlazorPoll.Server.Services;
using BlazorPoll.Shared.Exceptions;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace BlazorPoll.Server
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSignalR();

            services.AddControllersWithViews().AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            });

            services.AddRazorPages();

            services.AddAuthentication(options =>
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme
            ).AddCookie();
            
            services.AddResponseCompression(opts =>
            {
                opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
                    new[] {"application/octet-stream"});
            });

            
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection"))
                        .EnableSensitiveDataLogging();
            });

            services.AddScoped<IUserService, UsersService>();
            services.AddScoped<IPollsService, PollsService>();
            services.AddScoped<IAnswersService, AnswersService>();
            services.AddScoped<ICommentsService, CommentsService>();

            services.AddScoped<IUsersDao, UsersDao>();
            services.AddScoped<IPollsDao, PollsDao>();
            services.AddScoped<IAnswerDao, AnswerDao>();
            services.AddScoped<ICommentsDao, CommentsDao>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseResponseCompression();
            
            if (env.IsDevelopment())
            {
                app.UseWebAssemblyDebugging();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseBlazorFrameworkFiles();
            app.UseStaticFiles();

            // TODO
            app.UseExceptionHandler("/r");

            app.UseRouting();

            app.UseAuthentication();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllers();
                endpoints.MapHub<PollHub>("/pollhub");
                endpoints.MapFallbackToFile("index.html");
            });
        }
    }
}
