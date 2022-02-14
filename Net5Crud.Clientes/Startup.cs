using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Serilog;
using System;
using Net5Crud.Clientes.HealthChecks;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Net5Crud.Clientes.Data;

namespace Net5Crud.Clientes
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
             services.AddControllersWithViews();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Net5.Alumnos.API", Version = "v1" });
            });
            //services.AddDbContext<ApplicationDBContext>(options =>
            //options.UseSqlServer(Configuration.GetConnectionString("default")));
            string connectionString = Configuration.GetConnectionString("default");
            services.AddDbContext<ApplicationDBContext>(o => o.UseSqlServer(connectionString));
            //services.AddScoped<IAlumnoRepository, AlumnoRepository>();

            services.AddSession();

            services.AddHealthChecks()
                
               //.AddDbContextCheck<ApplicationDBContext>()
               .AddUrlGroup(new Uri("http://google.com"), name: "Google Inc.")
               .AddCheck<CustomHealthCheck>(name: "New Custom Check")
               .AddCheck("CatalogDB-Check", new SqlConnectionHealthCheck(Configuration.GetConnectionString("default")), HealthStatus.Unhealthy, new string[] { "catalogdb" });

            services.AddHealthChecksUI()
                .AddInMemoryStorage();

            // string connectionString = Configuration.GetConnectionString("default");
            //     services.AddDbContext<ApplicationDBContext>(o => o.UseSqlServer(connectionString));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Net5.Alumnos.API v1"));

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
           // context.EnsureSeeDataForContext();
            app.UseSession();
          
            app.UseSerilogRequestLogging();
             
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Login}/{action=Login}/{id?}");
                   // pattern: "{controller=Clients}/{action=Index}/{id?}");
                //  pattern: "{controller=Home}/{action=Index}/{id?}");
            });

            app.UseHealthChecks("/health", new HealthCheckOptions()
            {
                Predicate = _ => true,
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            });

            app.UseHealthChecksUI(config => config.UIPath = "/health-ui");
        }
    }
}
