using AutoMapper;
using System.Text;
using FluentValidation;
using Microsoft.OpenApi.Models;
using System.Collections.Generic;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Essentials.Data;
using Essentials.Domain;
using Essentials.Common;
using Microsoft.Extensions.DependencyInjection;

namespace Essentials.Api
{   
    public class Startup(IConfiguration configuration)
    {
        public IConfiguration Configuration { get; } = configuration;

        public void ConfigureServices(IServiceCollection services)
        {

            // JWT
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(jwtBearerOptions =>
            {
                jwtBearerOptions.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateActor = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = Configuration["Issuer"],
                    ValidAudience = Configuration["Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["SigningKey"]))
                };
            });

            // DbContext
            services.AddDbContext<EssentialsDbContext>(options =>
            options.UseSqlServer("Server=ismail-demo-db.database.windows.net;Database=ismail-demo-db;User ID=ismailAzureSqlAdmin;Password=HD45W4*F537rtK!;Trusted_Connection=False;Encrypt=True;"));

            services.AddControllers().AddFluentValidation(fv =>
            {
                fv.RegisterValidatorsFromAssemblyContaining<Startup>();
            });
            services.AddSingleton<IValidator<Product>, ProductValidator>();

            // Redis
            services.AddDistributedRedisCache(option =>
            {
                option.Configuration = "localhost:6379";
            });

            //Scructor(türetilen servis ve repolarý otomatik inject eder.)
            services.Scan(scan => scan
            .FromApplicationDependencies()
            .AddClasses(classes => classes.AssignableTo<IScopedLifetime>())
            .AsImplementedInterfaces()
            .WithScopedLifetime()
            .AddClasses(classes => classes.AssignableTo<ITransientLifetime>())
            .AsImplementedInterfaces()
            .WithTransientLifetime()
            .AddClasses(classes => classes.AssignableTo<ISingletonLifetime>())
            .AsImplementedInterfaces()
            .WithSingletonLifetime());

            //Response Cache service
            services.AddResponseCaching(options =>
            {
                options.UseCaseSensitivePaths = true;
                options.MaximumBodySize = 1024;
            });

            // Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Net Core Example Api", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n 
                      Enter 'Bearer' [space] and then your token in the text input below.
                      \r\n\r\nExample: 'Bearer 12345abcdef'",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header,
                        },
                        new List<string>()
                    }
                });
            });
            AddAutoMapperConfiguration(services);
        }

        /// <summary>
        /// Auto Mapper Konfigrasyonunu .Net Core'ekler.
        /// </summary>
        /// <param name="services">.</param>
        private void AddAutoMapperConfiguration(IServiceCollection services)
        {
            //Auto Mapper
            services.AddAutoMapper(typeof(Startup));
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AllowNullCollections = true;
                mc.AddProfile(new MapperProfile());
            });
            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);
        }

        /// <summary>
        /// The Configure.This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app">The app<see cref="IApplicationBuilder"/>.</param>
        /// <param name="env">The env<see cref="IWebHostEnvironment"/>.</param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "NetCoreExampleApi v1"));
            
            app.UseStaticFiles();
            app.UseAuthentication();

            app.UseResponseCaching();
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
