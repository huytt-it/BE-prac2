using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using BE_prac2.Helpers;
using Data.DataAcess;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;
using Services.Services;

namespace BE_prac2.Extensions
{
    public static class StartupExtensions
    {
        public static void ConfigMongoDbContext(this IServiceCollection services, string connectionString, string databaseName)
        {
            services.AddSingleton<IMongoClient>(s => new MongoClient(connectionString));
            services.AddScoped(s => new MongoDbContext(s.GetRequiredService<IMongoClient>(), databaseName));
        }

        public static void AddBusinessServices(this IServiceCollection services)
        {
            services.AddScoped<IProductServices, ProductServices>();
            services.AddScoped<ICustomerServices, CustomerServices>();
        }

        public static void ConfigCors(this IServiceCollection services)
        {
            services.AddCors(options => 
                options.AddPolicy("AllowAll", builder => builder.AllowAnyHeader()
                .AllowAnyMethod()
                .SetIsOriginAllowed(_ => true)
                .AllowCredentials()));
        }

        public static void ConfigJwt(this IServiceCollection services, IConfiguration Configuration)
        {
            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);

            // configure jwt authentication
            var appSettings = appSettingsSection.Get<AppSettings>();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);
            services.AddAuthentication(x =>
                {
                    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(x =>
                {
                    x.RequireHttpsMetadata = false;
                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });
        }
    }
}
