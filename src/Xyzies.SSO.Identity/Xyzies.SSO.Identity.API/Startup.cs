﻿using System;
using System.IO;
using System.IO.Compression;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.AzureADB2C.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Mapster;
using Xyzies.SSO.Identity.Data;
using Xyzies.SSO.Identity.Data.Repository;
using Xyzies.SSO.Identity.Data.Repository.Azure;
using Xyzies.SSO.Identity.Data.Entity.Azure.AzureAdGraphApi;
using Xyzies.SSO.Identity.Services.Mapping;
using Xyzies.SSO.Identity.Services.Service;
using Xyzies.SSO.Identity.Services.Middleware;
using Xyzies.SSO.Identity.Services.Service.Roles;
using Xyzies.SSO.Identity.Services.Service.Permission;
using Xyzies.SSO.Identity.Services.Helpers;

using Xyzies.SSO.Identity.UserMigration;
using System.Collections.Generic;
using System.Linq;
using Xyzies.SSO.Identity.Services.Models;

namespace Xyzies.SSO.Identity.API
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
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
            //services.AddIdentity<ApplicationUser, IdentityRole>()
            //    .AddEntityFrameworkStores<ApplicationDbContext>()
            //    .AddDefaultTokenProviders();

            services.AddAuthentication(AzureADB2CDefaults.BearerAuthenticationScheme)
               .AddAzureADB2CBearer(options => Configuration.Bind("AzureAdB2C", options));

            services.Configure<ProjectSettingsOption>(option => Configuration.Bind("ProjectSettings", option));

            string dbConnectionString = Configuration.GetConnectionString("db");
            if (string.IsNullOrEmpty(dbConnectionString))
            {
                StartupException.Throw("Missing the connection string to database");
            }
            services //.AddEntityFrameworkSqlServer()
                .AddDbContext<IdentityDataContext>(ctxOptions =>
                    ctxOptions.UseSqlServer(dbConnectionString));

            string cablePortalDBConnectionString = Configuration.GetConnectionString("cpdb");
            services //.AddEntityFrameworkSqlServer()
                .AddDbContextPool<CablePortalIdentityDataContext>(ctxOptions =>
                     ctxOptions.UseSqlServer(cablePortalDBConnectionString));

            // Response compression
            // https://docs.microsoft.com/en-us/aspnet/core/performance/response-compression?view=aspnetcore-2.2#brotli-compression-provider
            services.AddResponseCompression(options =>
            {
                // NOTE: Enabling compression on HTTPS connections may expose security problems.
                options.EnableForHttps = true;
                options.Providers.Add<BrotliCompressionProvider>();
                options.Providers.Add<GzipCompressionProvider>();
            });

            services.Configure<BrotliCompressionProviderOptions>(options => options.Level = CompressionLevel.Fastest);
            services.Configure<GzipCompressionProviderOptions>(options => options.Level = CompressionLevel.Fastest);

            JsonConvert.DefaultSettings = () => new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                NullValueHandling = NullValueHandling.Ignore
            };

            services.AddHealthChecks();
            // TODO: Add check for database connection
            //.AddCheck(new SqlConnectionHealthCheck("MyDatabase", Configuration["ConnectionStrings:DefaultConnection"]));

            services.AddCors(setup => setup
                .AddPolicy("dev", policy =>
                    policy.AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials()));

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddMemoryCache();
            #region DI configuration

            services.AddScoped<DbContext, IdentityDataContext>();
            services.AddScoped<DbContext, CablePortalIdentityDataContext>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IPermissionService, PermissionService>();
            services.AddScoped<IAzureAdClient, AzureAdClient>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<ICpUsersRepository, CpUsersRepository>();
            services.AddScoped<ICityRepository, CityRepository>();
            services.AddScoped<IStateRepository, StateRepository>();
            services.AddScoped<ILocaltionService, LocationService>();
            services.AddUserMigrationService();
            #endregion

            services.Configure<AzureAdB2COptions>(Configuration.GetSection("AzureAdB2C"));
            services.Configure<AzureAdGraphApiOptions>(Configuration.GetSection("AzureAdGraphApi"));
            services.Configure<AuthServiceOptions>(Configuration.GetSection("UserAuthorization"));

            services.AddSwaggerGen(options =>
            {
                options.SwaggerGeneratorOptions.IgnoreObsoleteActions = true;

                options.SwaggerDoc("v1", new Info
                {
                    Title = "Xyzies.Identity",
                    Version = $"v1.0.0",
                    Description = ""
                });

                options.AddSecurityDefinition("Bearer", new ApiKeyScheme
                {
                    In = "header",
                    Name = "Authorization",
                    Description = "Please enter JWT with Bearer into field",
                    Type = "apiKey"
                });

                options.AddSecurityRequirement(new Dictionary<string, IEnumerable<string>>
                {
                    { "Bearer", Enumerable.Empty<string>() }
                });

                options.CustomSchemaIds(x => x.FullName);
                options.EnableAnnotations();
                options.DescribeAllEnumsAsStrings();
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory,
                   string.Concat(Assembly.GetExecutingAssembly().GetName().Name, ".xml")));
            });

            TypeAdapterConfig.GlobalSettings.Default.PreserveReference(true);
            UserMappingConfigurations.ConfigureUserMappers();
            RolesMappingConfigurations.ConfigureRoleMappers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (!env.IsDevelopment())
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts()
                    .UseHttpsRedirection();
            }

            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<IdentityDataContext>();
                //context.Database.Migrate();
            }

            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var userService = serviceScope.ServiceProvider.GetRequiredService<IUserService>();
                userService.SetUsersCache();
            }

            app.UseAuthentication()
                .UseProcessClaims()
                .UseHealthChecks("/healthz")
                .UseCors("dev")
                .UseResponseCompression()
                .UseMvc()
                .UseSwagger(options =>
                {
                    options.PreSerializeFilters.Add((swaggerDoc, httpReq) => swaggerDoc.BasePath = "/api/identity/");
                    options.RouteTemplate = "/swagger/{documentName}/swagger.json";
                })
                .UseSwaggerUI(uiOptions =>
                {
                    uiOptions.SwaggerEndpoint("v1/swagger.json", $"v1.0.0");
                    //uiOptions.RoutePrefix = "/api/identity";
                    uiOptions.DisplayRequestDuration();
                });

        }
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
