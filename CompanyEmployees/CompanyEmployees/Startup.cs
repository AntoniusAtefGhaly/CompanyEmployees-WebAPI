using CompanyEmployees.ActionFilters;
using CompanyEmployees.Extensions;
using CompanyEmployees.Utility;
using Contracts;
using Entities.DataTransferObjects;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NLog;
using Repository.DataShaping;
using System.IO;
using System.Reflection;

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
            //services.AddDbContext<RepositoryContext>(options =>
            //options.UseSqlServer(
            //    Configuration.GetConnectionString("sqlConnection"),
            //        b => b.MigrationsAssembly(typeof(RepositoryContext).Assembly.FullName)));

            //ConfigureCors
            services.ConfigureCors();
            services.ConfigureIISIntegration();
            services.ConfigureLoggerService();
            services.AddControllers();
            services.ConfigureRepository();
            services.ConfigureSqlContext(Configuration);
            services.AddCustomMediaTypes();
            services.ConfigureVersioning();
            services.ConfigureResponseCaching();
            services.ConfigureHttpCacheHeaders();

            services.AddScoped<ValidationFilterAttribute>();
            services.AddScoped<ValidateCompanyExistsAttribute>();
            services.AddScoped<ValidateEmployeeCompanyExistsFilter>();
            services.AddScoped<ValidateMediaTypeAttribute>();

            services.AddScoped<IDataShaper<EmployeeDto>, DataShaper<EmployeeDto>>();
            services.AddScoped<EmployeeLinks, EmployeeLinks>();

            // services.AddAutoMapper(typeof(MappingProfile));
            // var x=System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;
            //services.AddAutoMapper(typeof(Startup));
            services.AddAutoMapper(Assembly.Load("Entities"));

            //services.AddAutoMapper(Assembly.GetExecutingAssembly());

            //accept header

            services.AddControllers(config =>
            {
                config.RespectBrowserAcceptHeader = true;
                config.ReturnHttpNotAcceptable = true;
                config.CacheProfiles.Add("120second",
                    new CacheProfile
                    {
                        Duration = 120
                    });
            }
            ).AddNewtonsoftJson()
.AddXmlDataContractSerializerFormatters()
            .AddCustomCSVFormatter();

            services.Configure<Microsoft.AspNetCore.Mvc.ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerManager logger)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }
            app.ConfigureExceptionHandler(logger);

            app.UseHttpsRedirection();

            app.UseStaticFiles();
            app.UseCors("CorsPolicy");
            app.UseForwardedHeaders(new ForwardedHeadersOptions { ForwardedHeaders = ForwardedHeaders.All });

            app.UseResponseCaching();
            app.UseHttpCacheHeaders();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                     name: "defaut",
                     pattern: "{controller=Home}/{action=Index}/{id?}"
                     );
            });
        }
    }
}