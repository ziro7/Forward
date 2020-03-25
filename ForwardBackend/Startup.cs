using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ForwardBackend.Models;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace ForwardBackend
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services) {

            // Registers that the DB Context uses a SQL Server and the connection string to the DB is the default defined in the appsettings.json file.
            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddControllers();

            // Registering the the models
            services.AddTransient<IJobRepository, JobRepository>(); //When asking for IJobRepository a New JobRepository is returned.
            services.AddScoped<IJobRepository, JobRepository>(); // Needed to create the Entity database model
            services.AddScoped<IWorkExperienceRepository, WorkExperienceRepository>();

            services.AddMvc(option => option.EnableEndpointRouting = false)
                .SetCompatibilityVersion(CompatibilityVersion.Version_3_0)
                .AddNewtonsoftJson(opt => opt.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);
            //services.AddOData(); Not supported yet

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger) {

            // Middleware component
            /*The request handling pipeline is composed as a series of middleware components. 
             * Each component performs asynchronous operations on an HttpContext and then either
             * invokes the next middleware in the pipeline or terminates the request.*/

            if (env.IsDevelopment()) {
                logger.LogInformation("In Development environment");
                app.UseDeveloperExceptionPage(); // gets more information on crash - should not be in release tho
            } else {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            //app.UseHttpsRedirection(); //Might be needed at some point?
            app.UseStaticFiles(); // Enables static files (search in www. files) - might not be needed in api
            //app.UseCookiePolicy(); // TODO look into cookies

            app.UseRouting();
            // app.UseRequestLocalization(); // TODO look into it later
            // app.UseCors(); // TODO look into it later

            //app.UseAuthentication(); //TODO Add later
            //app.UseAuthorization(); //TODO add later
            //app.UseSession(); //TODO look into it later

            app.UseStatusCodePages(); // adds the status code 200 etc.

            // Odata is not yet supported for 3.0.0
            //app.UseMvc(routebuilder => 
            //{
            //    routebuilder.EnableDependencyInjection(); // Adding support for Odata.
            //    routebuilder.Expand().Select().OrderBy(); // Currently I only plan to use orderby.
            //});

            app.UseEndpoints(endpoints => {
                endpoints.MapControllers();
            });
        }
    }
}
