using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Artour.Domain.EntityFramework.Context;
using Artour.WebAPI.Configurations;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;
using AutoMapper;
using Artour.Domain.Dapper.Context;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Artour.BLL.Models;

namespace Artour.WebAPI
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
            ApplicationSettingsManager<ApplicationSettings>.BindToObject(Configuration);
            JwtSettings jwtSettings = new JwtSettings();
            Configuration.Bind(nameof(JwtSettings), jwtSettings);

            services.AddMemoryCache();
            services.AddDbContext<ApplicationDbContext>();
            services.AddSingleton<DapperDbContext>();
            services.ConfigureBLLServices();
            services.AddAutoMapper();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        // will the lifetime be validated
                        ValidateLifetime = jwtSettings.ValidateLifetime,

                        // specifies whether the publisher will validate when validating the token
                        ValidateIssuer = jwtSettings.ValidateIssuer,

                        // a string representing the publisher
                        ValidIssuer = jwtSettings.ValidIssuer,

                        // setting the token consumer
                        ValidAudience = jwtSettings.ValidAudience,

                        // Will the token consumer be validated
                        ValidateAudience = jwtSettings.ValidateAudience,

                        // validate the security key
                        ValidateIssuerSigningKey = jwtSettings.ValidateIssuerSigningKey,

                        // set the security key
                        IssuerSigningKey =
                            new SymmetricSecurityKey(Convert.FromBase64String(jwtSettings.IssuerSigningKey))
                    };
                });

            services.AddAuthorization();

            services.AddCors();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Title = "ArTour API",
                    Description = "Swagger Core API documentation",
                    Version = "v1"
                });
                var xmlPath = AppDomain.CurrentDomain.BaseDirectory + @"Artour.WebAPI.xml";
                c.IncludeXmlComments(xmlPath);
                c.OperationFilter<FileOperationFilter>();
                c.AddSecurityDefinition("Bearer", new ApiKeyScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = "header",
                    Type = "apiKey"
                });
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(builder => builder
              .AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader()
              .AllowCredentials());

            app.UseAuthentication();

            app.UseMvc();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Core API");
                c.RoutePrefix = String.Empty;
            });
        }
    }
}
