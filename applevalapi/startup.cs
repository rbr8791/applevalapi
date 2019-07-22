using System.Linq;
using ClacksMiddleware.Extensions;
using applevalApi.Helpers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using AutoMapper;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.PlatformAbstractions;
using System.Reflection;
using System.IO;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Threading.Tasks;
using applevalApi.DAL.Interfaces;

namespace applevalApi
{
    public class Startup
    {

        //public IConfigurationRoot Configuration { get; set; }

        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
           
            Configuration = configuration;

            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);
            if (env.IsDevelopment())
            {
               // builder.AddUserSecrets();
            }
            builder.AddEnvironmentVariables();
            Configuration = builder.Build();


        }
        
        public IConfiguration Configuration { get; }
        

        

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddResponseCaching();
            services.AddResponseCompression(options =>
            {
                options.Providers.Add<GzipCompressionProvider>();
                options.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(new[]
                    {
                        "text/plain", "application/json"
                    });
            });

            AppSettings app = new AppSettings();
            var secret = Configuration.GetSection("AppSettings:Secret");

            app.Secret = secret.Value;


            var key = Encoding.ASCII.GetBytes(app.Secret);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.Events = new JwtBearerEvents
                {
                    OnTokenValidated = context =>
                    {
                        var userService = context.HttpContext.RequestServices.GetRequiredService<IUserService>();
                        var userId = int.Parse(context.Principal.Identity.Name);
                        var user = userService.GetById(userId);
                        if (user == null)
                        {
                            context.Fail("Unauthorized");
                        }
                        return Task.CompletedTask;
                    }
                };
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

            //services.Configure<AppSettings>(Configuration.GetSection("AppSettingsOptions"));
            services.Configure<AppSettings>(options => Configuration.GetSection("AppSettings").Bind(options));
            services.AddCustomizedMvc();
           
            services.AddCorsPolicy();
            services.AddDbContext();

            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new Helpers.AutoMapperProfile());
            });

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);
            services.AddSingleton(app);
            //services.AddApiVersioning(
            //    o =>
            //    {
            //        o.AssumeDefaultVersionWhenUnspecified = true;
            //        o.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(new System.DateTime(2019, 7, 20));
            //    });

            services.AddApiVersioning(
                o =>
                {
                    o.AssumeDefaultVersionWhenUnspecified = true;
                    o.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1,0);
                    o.ReportApiVersions = true;
                });
            services.AddVersionedApiExplorer(
                 options =>
                 {
                    // add the versioned api explorer, which also adds IApiVersionDescriptionProvider service
                    // note: the specified format code will format the version as "'v'major[.minor][-status]"
                    options.GroupNameFormat = "'v'VVV";

                    // note: this option is only necessary when versioning by url segment. the SubstitutionFormat
                    // can also be used to control the format of the API version in route templates
                    options.SubstituteApiVersionInUrl = true;
                 });

            //services.AddVersionedApiExplorer(
            //   options =>
            //   {
            //        // add the versioned api explorer, which also adds IApiVersionDescriptionProvider service
            //        // note: the specified format code will format the version as "'v'major[.minor][-status]"
            //        options.GroupNameFormat = "'v'VVV";

            //        // note: this option is only necessary when versioning by url segment. the SubstitutionFormat
            //        // can also be used to control the format of the API version in route templates
            //        options.SubstituteApiVersionInUrl = true;
            //   });

            //services.AddAutoMapper(typeof(Helpers.AutoMapperProfile));
            services.AddTransientServices();

           

            //services.AddSwagger($"v{CommonHelpers.GetVersionNumber()}");
            services.AddSwagger();
            




        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, IApiVersionDescriptionProvider provider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.EnsureDatabaseIsSeeded(false);
            }

            // Only block and upgrade all insecure requests when not in dev
            // temporarily commented out, in order to get the docker container runnning
            //app.UseSecureHeaders(env.IsProdOrStaging());
            app.UseResponseCaching();
            app.UseResponseCompression();
            app.GnuTerryPratchett();
            //app.UseCorsPolicy();
            app.UseCors(x => x
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());

            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseSwagger($"/swagger/v{CommonHelpers.GetVersionNumber()}/swagger.json",
                $"applevalApi {CommonHelpers.GetVersionNumber()}", provider);
           
            app.UseCustomisedMvc();
        }

        static string XmlCommentsFilePath
        {
            get
            {
                var basePath = PlatformServices.Default.Application.ApplicationBasePath;
                var fileName = typeof(Startup).GetTypeInfo().Assembly.GetName().Name + ".xml";
                return Path.Combine(basePath, fileName);
            }
        }

    }
}