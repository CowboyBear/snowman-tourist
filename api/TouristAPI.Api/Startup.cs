using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TouristAPI.Database.Context;
using Microsoft.EntityFrameworkCore;
using TouristAPI.Database.Repository;
using System;
using TouristAPI.Service;
using TouristAPI.Service.Validators;

namespace TouristAPI.Api
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
            services.AddControllers();
                        
            services.AddScoped<ILocationService, LocationService>();    
            services.AddScoped<ILocationFormValidator, LocationFormValidator>();

            services.AddScoped<ILocationRepository, LocationRepository>();            
            services.AddScoped<IDatabaseContext, DatabaseContext>();

            services.AddDbContext<DatabaseContext>(
              options => options.UseSqlServer(
                Environment.GetEnvironmentVariable("TOURIST_API_DB_CONNECTION_STRING"), 
                b => b.MigrationsAssembly("TouristAPI.Api")
              )
            );
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
