﻿using AspNetCoreRateLimit;
using Business.Diary.GenericServices.Contracts;
using Business.Diary.GenericServices.Implmentation;
using Business.ServiceLocator.Contracts;
using Business.ServiceLocator.Implmentation;
using Domain.Auth;
using Domain.ConfigurationModels;
using Marvin.Cache.Headers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Presentation.Controllers;
using Repository.Context;
using Repository.UnitOfWork;
using Shared.LoggerService;
using System.Reflection;
using System.Text;

namespace Api.Host.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureCors(this IServiceCollection services) =>
             services.AddCors(options =>
             {
                 options.AddPolicy("CorsPolicy", builder =>
                 builder.AllowAnyOrigin()
                 .AllowAnyMethod()
                 .WithExposedHeaders("X-Pagination")
                 .AllowAnyHeader());
             });

        public static void ConfigureIISIntegration(this IServiceCollection services) =>
         services.Configure<IISOptions>(options =>
         {

         });


        public static void ConfigureLoggerService(this IServiceCollection services) =>
                services.AddSingleton<ILoggerManager, LoggerManager>();

        public static void ConfigureRepositoryManager(this IServiceCollection services) =>
                services.AddScoped<IUnitOfWork, UnitOfWork>();


        public static void ConfigureServiceManager(this IServiceCollection services) =>
                services.AddScoped<IServiceLocator, ServiceLocator>();


        public static void ConfigureSqlContext(this IServiceCollection services, IConfiguration configuration) =>
            services.AddDbContext<RepositoryContext>(
                opts => opts.UseSqlServer(configuration.GetConnectionString("sqlConnection")));


        public static void ConfigureVersioning(this IServiceCollection services)
        {
            services.AddApiVersioning(opt =>
            {
                opt.ReportApiVersions = true;
                opt.AssumeDefaultVersionWhenUnspecified = true;
                opt.DefaultApiVersion = new ApiVersion(1, 0);
                opt.ApiVersionReader = new HeaderApiVersionReader("api-version");
                opt.ApiVersionReader = new QueryStringApiVersionReader("api-version");
            });
        }
        public static void ConfigureResponseCaching(this IServiceCollection services) =>
                services.AddResponseCaching();

        public static void ConfigureHttpCacheHeaders(this IServiceCollection services) =>
             services.AddHttpCacheHeaders((expirationOpt) =>
             {
                 expirationOpt.MaxAge = 65;
                 expirationOpt.CacheLocation = CacheLocation.Private;
             },
             (validationOpt) =>
             {
                 validationOpt.MustRevalidate = true;
             });
        public static void ConfigureRateLimitingOptions(this IServiceCollection services)
        {
            var rateLimitRules = new List<RateLimitRule>
            {
                new RateLimitRule
                {
                    Endpoint = "*",
                    Limit =1000,
                    Period = "5m"
                 }
             };
             services.Configure<IpRateLimitOptions>(opt => {
                     opt.GeneralRules = rateLimitRules;
             });
            services.AddSingleton<IRateLimitCounterStore, MemoryCacheRateLimitCounterStore>();
            services.AddSingleton<IIpPolicyStore, MemoryCacheIpPolicyStore>();
            services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
            services.AddSingleton<IProcessingStrategy, AsyncKeyLockProcessingStrategy>();
        }

        public static void ConfigureIdentity(this IServiceCollection services)
        {
            var builder = services.AddIdentity<User, IdentityRole>(o =>
            {
                o.Password.RequireDigit = true;
                o.Password.RequireLowercase = false;
                o.Password.RequireUppercase = false;
                o.Password.RequireNonAlphanumeric = false;
                o.Password.RequiredLength = 10;
                o.User.RequireUniqueEmail = true;
            })
            .AddEntityFrameworkStores<RepositoryContext>()
            .AddDefaultTokenProviders();
        }



        public static void ConfigureJWT(this IServiceCollection services, IConfiguration configuration)

        {

            var jwtConfiguration = new JwtConfiguration();
            configuration.Bind(jwtConfiguration.Section, jwtConfiguration);

            var jwtSettings = configuration?.GetSection("JwtSettings");
            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtConfiguration.ValidIssuer,
                    ValidAudience = jwtConfiguration.ValidAudience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["secretKey"]))
                };
            });
        }

        public static void AddJwtConfiguration(this IServiceCollection services,
            IConfiguration configuration) =>
                    services.Configure<JwtConfiguration>(configuration.GetSection("JwtSettings"));


    

        public static void ConfigureValidateIFexists(this IServiceCollection services)
            => services.AddScoped<IValidateIFexists, ValidateIFexists>();



       public static void ConfigureAuthenticationHandler(this IServiceCollection services)
            => services.AddAuthentication("Bearer")
            .AddJwtBearer("Bearer", options =>
            {
                options.Authority = "https://localhost:5005";
                options.Audience = "diaryApi";
            });
    }
}
