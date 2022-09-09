using CompanyEmployees.Extensions;
using Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NLog;
using System.IO;

namespace CompanyEmployees
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            LogManager.LoadConfiguration(string.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<RepositoryContext>(options =>
     options.UseSqlServer(
         Configuration.GetConnectionString("sqlConnection"),
             b => b.MigrationsAssembly(typeof(RepositoryContext).Assembly.FullName)));


            //ConfigureCors
            services.ConfigureCors();
            services.ConfigureIISIntegration();
            services.ConfigureLoggerService();
            services.AddControllers();
            //services.ConfigureSqlContext(Configuration);
            //        services.AddDbContext<ApplicationDbContext>(options =>
            //options.UseSqlServer(
            //    Configuration.GetConnectionString("DefaultConnection"),
            //        b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));


            //services.AddDbContext<RepositoryContext>(
            //    options => 
            //    options.UseSqlServer(
            //        Configuration.GetConnectionString("sqlConnection"),
            //            b => b.MigrationsAssembly(typeof(RepositoryContext).Assembly.FullName)));
            //services.AddDbContext<RepositoryContext>(
            //options => options.UseSqlServer("server=.; database=CompanyEmployee; Integrated Security=true"));


            services.ConfigureRepository();

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
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();
            app.UseCors("CorsPolicy");
            app.UseForwardedHeaders(new ForwardedHeadersOptions { ForwardedHeaders = ForwardedHeaders.All });

            app.UseAuthorization();
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name:"defaut",
                    pattern:"{controller=Home}/{action=Index}/{id?}"
                    );
            });
        }
    }
}