using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ForwardBackend.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace ForwardBackend
{
    public class Startup
    {
        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services) {
            // Registering the the models
            services.AddTransient<IJobRepository, JobRepository>(); //When asking for IJobRepository a New JobRepository is returned.
            services.AddMvc(); //maybe it should be mvcCore. Enabling the mvc services
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {

            // Middleware component
            app.UseDeveloperExceptionPage(); // gets more information on crash - should not be in release tho
            app.UseStatusCodePages(); // adds the status code 200 etc.
            app.UseStaticFiles(); // Enables static files (search in www. files) - might not be needed in api

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => {
                endpoints.MapControllers();
            });
        }
    }
}
