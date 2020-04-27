using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Forward.Areas.Identity.Data;
using Forward.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Forward
{
    public class Startup
    {
        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services) {

            // Add support for Razor pages
            services.AddRazorPages();

            // Use the IHttpClient factory to register JobService and to use the uri when called.
            services.AddHttpClient<IJobService, JobService>(client => {
                client.BaseAddress = new Uri("https://localhost:5001/");
                //client.BaseAddress = new Uri("https://localhost:44378/");
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json", 1.0));
            });

            // Use the IHttpClient factory to register JobService and to use the uri when called.
            services.AddHttpClient<IGraphService, GraphService>(client => {
                client.BaseAddress = new Uri("https://localhost:44344/");
            });

            services.AddServerSideBlazor().AddCircuitOptions(options => { options.DetailedErrors = true; }); //Added the option for detailed info delivered to the browser.
            
            // This one is needed to acces the cookie information from other pages where the service is injected.
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddAuthentication("Identity.Application").AddCookie();  //Using ASP.Net Identity cookie

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            } else {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseStaticFiles();
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => {
                endpoints.MapBlazorHub(); //SignalR hub
                endpoints.MapFallbackToPage("/_Host"); // takes request that didn't find the correct page and points to _Host
            });
        }
    }
}
