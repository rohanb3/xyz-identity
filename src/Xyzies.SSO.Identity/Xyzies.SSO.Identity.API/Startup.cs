using System;
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

            //services.AddIdentityServer(c =>
            //{

            //});

            // For Azure Custom Identity Provider
            //services.AddAuthentication()
            //    .AddOpenIdConnect("aad_b2c", "SSO", options =>
            //    {

            //    });

            // TODO: DB connection string
            string dbConnectionString = Configuration.GetConnectionString("db");
            if (string.IsNullOrEmpty(dbConnectionString))
            {
                StartupException.Throw("Missing the connection string to database");
            }
            services //.AddEntityFrameworkSqlServer()
                .AddDbContextPool<IdentityDataContext>(ctxOptions =>
                    ctxOptions.UseSqlServer(dbConnectionString));

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

            #region DI configuration

            services.AddScoped<DbContext, IdentityDataContext>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IPermissionService, PermissionService>();
            services.AddScoped<IAzureAdClient, AzureAdClient>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ICpUsersService, CpUsersService>();
            services.AddScoped<ICpUsersRepository, CpUsersRepository>();
            #endregion

            services.Configure<AzureAdB2COptions>(Configuration.GetSection("AzureAdB2C"));
            services.Configure<AzureAdGraphApiOptions>(Configuration.GetSection("AzureAdGraphApi"));
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new Info
                {
                    Title = "Identity",
                    Version = $"v1.0.0",
                    Description = ""
                });

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

            app.UseAuthentication()
                .UseProcessClaims()
                .UseHealthChecks("/healthz")
                .UseCors("dev")
                .UseResponseCompression()
                .UseMvc()
                .UseSwagger()
                .UseSwaggerUI(uiOptions =>
                {
                    uiOptions.SwaggerEndpoint("/swagger/v1/swagger.json", $"v1.0.0");
                    uiOptions.DisplayRequestDuration();
                });

        }
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
