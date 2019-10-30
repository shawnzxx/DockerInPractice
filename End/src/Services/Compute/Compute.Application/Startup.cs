using System;
using System.Reflection;
using Compute.Application.Services;
using Compute.Domain.Models;
using Compute.Infrastructure;
using Compute.Infrastructure.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace Compute.Application
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

            #region Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Compute API", Version = "v1" });
            });
            #endregion

            #region EntityFramework Core
            var connection = Configuration.GetConnectionString("ComputeDBConnection");
            services.AddDbContext<OperationDbContext>(optionsBuilder =>
            {
                optionsBuilder
                .UseSqlServer(connection, sqlServerOptionsAction: sqlOptions => {
                    sqlOptions.MigrationsAssembly(typeof(Startup).GetTypeInfo().Assembly.GetName().Name);
                    sqlOptions.EnableRetryOnFailure(maxRetryCount: 10, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
                })
                .EnableSensitiveDataLogging(true);
            });
            #endregion

            #region Database repository
            services.AddScoped<IOperationRepository, OperationRepository>();
            #endregion

            #region Register our own services
            services.AddHttpClient<IAuditService, AuditService>();
            #endregion

            services.AddHttpsRedirection(options =>
            {
                options.HttpsPort = 443;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            #region Use Swagger Middleware
            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();
            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), 
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Compute API V1");
            });
            #endregion

            //Apply migrations to database. Create database if it does not exist
            //https://blog.rsuter.com/automatically-migrate-your-entity-framework-core-managed-database-on-asp-net-core-application-start/
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                using (var context = serviceScope.ServiceProvider.GetRequiredService<OperationDbContext>())
                {
                    context.Database.Migrate();
                }
            }
        }
    }
}
