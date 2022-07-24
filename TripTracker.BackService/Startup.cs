using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;
using TripTracker.BackService.Data;
using System.Data.Sql;
using Microsoft.EntityFrameworkCore;

namespace TripTracker.BackService
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
           // services.AddTransient<Models.Repository>();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            string connectionString = @"data source=.\SqlExpress;initial catalog=TripTracking;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework";
            
            //services.AddDbContext<TripContext>(o => o.UseSqlite("Data Source =JeffTrips.db"));

            services.AddDbContext<TripContext>(options => options.UseSqlServer(connectionString));
            services.AddSwaggerGen(options => 
            options.SwaggerDoc("v1", new Info {Title="Trip Tracker", Version="v1" })
            );
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseSwagger();
            if (env.IsDevelopment() || env.IsStaging())
            {
               
                app.UseSwaggerUI(options =>
                    options.SwaggerEndpoint("/swagger/v1/swagger.json", "Trip Tracker v1"));
            }
            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
            TripContext.SeedData(app.ApplicationServices);
        }
    }
}
