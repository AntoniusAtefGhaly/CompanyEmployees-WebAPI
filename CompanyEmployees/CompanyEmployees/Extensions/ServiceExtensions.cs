﻿using CompanyEmployees.Controllers;
using Contracts;
using Entities;
using LoggerService;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Repository;
using System.Linq;

namespace CompanyEmployees.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(
               options =>
               {
                   options.AddPolicy("CorsPolicy", builder =>
                   builder.AllowAnyOrigin().
                   AllowAnyMethod().AllowAnyHeader());
               });
        }

        public static void ConfigureIISIntegration(this IServiceCollection services)
        {
            services.Configure<IISOptions>(options =>
            {
            });
        }

        public static void ConfigureLoggerService(this IServiceCollection services)
        {
            services.AddScoped<ILoggerManager, LoggerManager>();
        }

        public static void ConfigureSqlContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<RepositoryContext>(
                opts => opts.UseSqlServer(configuration.GetConnectionString("sqlConnection"), b => b.MigrationsAssembly("CompanyEmployees"))
                );
        }

        public static void ConfigureRepository(this IServiceCollection services)
        {
            services.AddScoped<IRepositoryManager, RepositoryManager>();
        }

        public static IMvcBuilder AddCustomCSVFormatter(this IMvcBuilder builder)
        {
            return builder.AddMvcOptions(config => config.OutputFormatters.Add(new
CsvOutputFormatter()));
        }

        public static void AddCustomMediaTypes(this IServiceCollection services)
        {
            services.Configure<MvcOptions>(config =>
            {
                var newtonsoftJsonOutputFormatter = config.OutputFormatters
                .OfType<NewtonsoftJsonOutputFormatter>()?.FirstOrDefault();
                if (newtonsoftJsonOutputFormatter != null)
                {
                    newtonsoftJsonOutputFormatter
                    .SupportedMediaTypes
                    .Add("application/vnd.codemaze.hateoas+json");
                }
                var xmlOutputFormatter = config.OutputFormatters
               .OfType<XmlDataContractSerializerOutputFormatter>()?.FirstOrDefault();
                if (xmlOutputFormatter != null)
                {
                    xmlOutputFormatter
                    .SupportedMediaTypes
                    .Add("application/vnd.codemaze.hateoas+xml");
                }
            });
        }

        public static void ConfigureVersioning(this IServiceCollection services) =>
            services.AddApiVersioning(
                opt =>
                {
                    opt.ReportApiVersions = true;
                    opt.AssumeDefaultVersionWhenUnspecified = true;
                    opt.DefaultApiVersion = new ApiVersion(1, 0);
                    opt.ApiVersionReader = ApiVersionReader.Combine(
                        new HeaderApiVersionReader("api-version"),
                       new QueryStringApiVersionReader("ver", "version")
                        );

                    opt.Conventions.Controller<CompaniesController>().HasApiVersion(new ApiVersion(1, 0));
                    opt.Conventions.Controller<CompaniesV2Controller>().HasDeprecatedApiVersion(new
                    ApiVersion(3, 0));

                    //opt.ApiVersionReader = new QueryStringApiVersionReader("api-version");
                    //opt.ApiVersionReader = ApiVersionReader.Combine(
                    //    new HeaderApiVersionReader("x-version", "ver"),
                    //    new QueryStringApiVersionReader("ver", "version"));
                });

        //public static void ConfigureAutoMapper (this IServiceCollection services)
        //{
        //    services.AddAutoMapper(typeof(Startup));
        //}
    }
}