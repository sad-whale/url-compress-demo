using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using url_compress_demo.Persistance;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using url_compress_demo.ErrorHandling;
using System.Text;
using url_compress_demo.Services;
using url_compress_demo.CommandHandlers;
using url_compress_demo.Queries;
using System.Security.Claims;

namespace url_compress_demo
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<CompressorContext>(options => options.UseSqlServer(Configuration.GetConnectionString("azuredb")));
            // Add framework services.
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<ICompressorCommandHandler, CompressorCommandHandler>();
            services.AddTransient<ICompressorQueryFacade, CompressorQueryFacade>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseExceptionHandler(errorApp =>
            {
                errorApp.Run(async context =>
                {
                    context.Response.StatusCode = 500; // or another Status accordingly to Exception Type
                    context.Response.ContentType = "application/json";

                    var error = context.Features.Get<IExceptionHandlerFeature>();
                    if (error != null)
                    {
                        var ex = error.Error;

                        await context.Response.WriteAsync(new ErrorDto()
                        {
                            Code = ex.GetType().Name,
                            Message = ex.Message 
                        }.ToString(), Encoding.UTF8);
                    }
                });
            });

            app.Use((context, next) =>
            {
                var cookies = context.Request.Cookies;
                var userIdCookieName = "user_id";

                string userId;

                if (!cookies.TryGetValue(userIdCookieName, out userId))
                    userId = Guid.NewGuid().ToString();

                context.Response.Cookies.Append(userIdCookieName, userId, new CookieOptions() { Expires = DateTimeOffset.MaxValue });

                List<Claim> claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.Name, userId)
                };

                ClaimsIdentity identity = new ClaimsIdentity(claims, "Cookie");
                ClaimsPrincipal principal = new ClaimsPrincipal(identity);

                context.User = principal;

                return next.Invoke();
            });

            DefaultFilesOptions options = new DefaultFilesOptions();
            options.DefaultFileNames.Clear();
            options.DefaultFileNames.Add("app/index.html");

            app.UseDefaultFiles(options);

            app.UseStaticFiles();

            app.UseMvc();
        }
    }
}
