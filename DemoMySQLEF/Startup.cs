using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DemoMySqlEF.Models;
using DemoMySQLEF.Areas.Identity.Data;
using DemoMySQLEF.Models.Concrete;
using DemoMySQLEF.Models.Interface;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;

namespace DemoMySQLEF
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<RouteOptions>(options =>
            {
                options.AppendTrailingSlash = true;
                options.LowercaseUrls = true;
            });

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            // services.AddRouting(options => {
            //     options.AppendTrailingSlash = true;
            //     options.LowercaseUrls = true;
            // });
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IEmailSender, EmailSender>();
            services.AddDbContext<DataContext>(options => options.UseMySql(Configuration.GetConnectionString("DBConnStr")));


            services.AddAuthentication().AddFacebook(facebookOptions =>
            {
                facebookOptions.AppId = Configuration["Authentication:Facebook:AppId"];
                facebookOptions.AppSecret = Configuration["Authentication:Facebook:AppSecret"];
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            //app.UseHttpsRedirection();
            app.UseStaticFiles();
            // app.UseStaticFiles(new StaticFileOptions
            // {
            //     FileProvider = new PhysicalFileProvider(
            //     Path.Combine(Directory.GetCurrentDirectory(), "MyStaticFiles")),
            //     RequestPath = "/StaticFiles"
            // });
            app.UseCookiePolicy();
            app.UseAuthentication();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    null,
                    template: "sinhvien",
                    defaults: new
                    {
                        controller = "Student",
                        action = "Index"
                    }
                );

                //đặt trước sinhvien-id, nếu không sẽ route đến sinhvien-id trước
                routes.MapRoute(
                    name: "Student",
                    template: "sinhvien-chi-tiet-{id}",
                    defaults: new
                    {
                        controller = "Student",
                        action = "Details"
                    }
                );

                routes.MapRoute(
                    null,
                    template: "sinhvien-{id}",
                    defaults: new
                    {
                        controller = "Student",
                        action = "Edit"
                    }
                );

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
                // routes.MapGet("hello/{name}", context =>
                // {
                //     var name = context.GetRouteValue("name");
                //     return context.Response.WriteAsync($"Hi, {name}!");
                // });

            });
            // // app.UseMvcWithDefaultRoute();

            // var trackPackageRouteHandler = new RouteHandler(context =>
            // {
            //     var routeValues = context.GetRouteData().Values;
            //     return context.Response.WriteAsync(
            //         $"Hello! Route values: {string.Join(", ", routeValues)}");
            // });
            // var routeBuilder = new RouteBuilder(app, trackPackageRouteHandler);

            // routeBuilder.MapRoute(
            //     "Track Package Route",
            //     "package/{operation:regex(^(track|create|detonate)$)}/{id:int}");

            // routeBuilder.MapRoute(
            // name: "default",
            // template: "{controller=Home}/{action=Index}/{id?}");

            // routeBuilder.MapGet("hello/{name}", context =>
            // {
            //     var name = context.GetRouteValue("name");
            //     return context.Response.WriteAsync($"Hi, {name}!");
            // });

            // var routes = routeBuilder.Build();
            // app.UseRouter(routes);
        }
    }
}
