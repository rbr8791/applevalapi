using System.Collections.Generic;
using System.IO;
using applevalApi.Common;
using applevalApi.DAL;
using applevalApi.DAL.Interfaces;
using AutoMapper;
using applevalApi.Helpers;
using applevalApi.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyModel;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.SwaggerGen;
namespace applevalApi
{
    /// <summary>
    /// This class is based on some of the suggestions bty K. Scott Allen in
    /// his NDC 2017 talk https://www.youtube.com/watch?v=6Fi5dRVxOvc
    /// </summary>
    public static class ConfigureContainerExtenstions
    {
        private static string DbConnectionString => new DatabaseConfiguration().GetDatabaseConnectionString();
        private static string CorsPolicyName => new CorsConfiguration().GetCorsPolicyName();

        public static void AddDbContext(this IServiceCollection serviceCollection,
            string connectionString = null)
        {
            serviceCollection.AddDbContext<ApplDbContext>(options =>
                options.UseSqlite(connectionString ?? DbConnectionString));
        }

        public static void AddTransientServices(this IServiceCollection serviceCollection)
        {

            // Service Injection
            
            serviceCollection.AddTransient<IDatabaseService, DatabaseService>();

            serviceCollection.AddTransient<IActiveTokenService, ActiveTokenService>();
            serviceCollection.AddTransient<IAuditLogService, AuditLogService>();
            serviceCollection.AddTransient<IAuditLogPurchaseService, AuditLogPurchaseService>();

            serviceCollection.AddTransient<ICountryService, CountryService >();
            serviceCollection.AddTransient<ICustomerService, CustomerService>();
            serviceCollection.AddTransient<IInvoiceDetailService, InvoiceDetailService>();
            serviceCollection.AddTransient<IInvoiceService, InvoiceService>();
            serviceCollection.AddTransient<IOrderDetailService, OrderDetailService>();
            serviceCollection.AddTransient<IOrderService, OrderService>();
            serviceCollection.AddTransient<IProductService, ProductService>();
            serviceCollection.AddTransient<IRoleService, RoleService>();
            serviceCollection.AddTransient<IStockMovementService, StockMovementService>();
            serviceCollection.AddTransient<IStockMovementTypeService, StockMovementTypeService>();
            serviceCollection.AddTransient<IStockService, StockService>();
            serviceCollection.AddTransient<IUserService, UserService>();
            //serviceCollection.AddTransient<IMapper, Mapper>();

            

        }

        public static void AddCustomizedMvc(this IServiceCollection serviceCollection)
        {
            // serviceCollection.AddMvc(options => options.EnableEndpointRouting = true).SetCompatibilityVersion (CompatibilityVersion.Version_2_1);
            serviceCollection.AddMvc();
        }
       
        //public static void AddAutoMapper(this IServiceCollection serviceCollection)
        //{
        //    var config = new MapperConfiguration(cfg => {
        //        cfg.AddProfile<AutoMapperProfile>();
        //    });
        //    serviceCollection.AddAutoMapper();
        //}

        public static void AddCorsPolicy(this IServiceCollection serviceCollection, string corsPolicyName = null)
        {
            serviceCollection.AddCors(options =>
            {
                options.AddPolicy(corsPolicyName ?? CorsPolicyName,
                    builder => builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials());
            });
        }



        /// <summary>
        /// Used to register and add the Swagger generator to the service Colelction
        /// </summary>
        /// <param name="serviceCollection">
        /// The <see cref="IServiceCollection"/> which is used in the Containter
        /// </param>
        /// <param name="versionNumberString">The version number for the application</param>
        /// <param name="includeXmlDocumentation">
        /// Whether or not to include XmlDocumentation (defaults to True)
        /// </param>
        /// <remarks>
        /// <param name="includeXmlDocumentation"/> requries:
        ///   <PropertyGroup Condition="'$(Configuration)|$(Platform)'=='Debug|AnyCPU'">
        ///     <DocumentationFile>bin\Debug\netcoreapp2.1\applevalApi.xml</DocumentationFile>
        ///  </PropertyGroup>
        /// for debug builds and:
        ///   <PropertyGroup Condition="'$(Configuration)|$(Platform)'=='Release|AnyCPU'">
        ///     <DocumentationFile>bin\Release\netcoreapp2.1\applevalApi.xml</DocumentationFile>
        ///  </PropertyGroup>
        /// </remarks>
        public static void AddSwagger(this IServiceCollection serviceCollection, string versionNumberString,
            bool includeXmlDocumentation = true)
        {
            // Register the Swagger generator, defining one or more Swagger documents
            //serviceCollection.AddSwaggerGen(c =>
            //{

            //    c.SwaggerDoc($"v{CommonHelpers.GetVersionNumber()}",
            //        new Info
            //        {
            //            Title = "applevalApi",
            //            Version = $"v{CommonHelpers.GetVersionNumber()}",
            //            Description = "Applaudo Studio API Test",
            //            Contact = new Contact
            //            {
            //                Name = "R@ul Berrios Rivera",
            //                Email = "raul.berrios@outlook.com",
            //                Url = ""
            //            }
            //        }
            //    );

            //    var security = new Dictionary<string, IEnumerable<string>>
            //    {
            //        {"Bearer",  new string[] { }},
            //    };

            //    c.AddSecurityDefinition("Bearer", new ApiKeyScheme
            //    {
            //        Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
            //        Name = "Authorization",
            //        In = "header",
            //        Type = "apiKey"

            //    });
            //    c.AddSecurityRequirement(security);




            //    if (!includeXmlDocumentation) return;
            //    // Set the comments path for the Swagger JSON and UI.
            //    var basePath = Directory.GetCurrentDirectory();
            //    var xmlPath = Path.Combine(basePath, "applevalApi.xml");
            //    if (File.Exists(xmlPath))
            //    {
            //        c.IncludeXmlComments(xmlPath);
            //    }




            //}); // swagger gen

            // Configure swagger
            serviceCollection.AddSwaggerGen(options =>
            {
                // Specify two versions 
                options.SwaggerDoc("v1",
                    new Info()
                    {
                        Version = "v1",
                        Title = "applevalApi v1 API",
                        Description = "Applaudo Studio API Test",
                        TermsOfService = "Evaluation API Test Usage v1",
                        Contact = new Contact
                        {
                            Name = "R@ul Berrios Rivera",
                            Email = "raul.berrios@outlook.com",
                            Url = ""
                        }
                    }); ;

                //options.SwaggerDoc("v2",
                //    new Info()
                //    {
                //        Version = "v2",
                //        Title = "v2 API",
                //        Description = "v2 API Description",
                //        TermsOfService = "Terms of usage v2"
                //    });

                // This call remove version from parameter, without it we will have version as parameter 
                // for all endpoints in swagger UI
                options.OperationFilter<RemoveVersionFromParameter>();

                // This make replacement of v{version:apiVersion} to real version of corresponding swagger doc.
                options.DocumentFilter<ReplaceVersionWithExactValueInPath>();

                // This on used to exclude endpoint mapped to not specified in swagger version.
                // In this particular example we exclude 'GET /api/v2/Values/otherget/three' endpoint,
                // because it was mapped to v3 with attribute: MapToApiVersion("3")
                //options.DocInclusionPredicate((version, desc) =>
                //{
                //    var versions = desc.ControllerAttributes()
                //        .OfType<ApiVersionAttribute>()
                //        .SelectMany(attr => attr.Versions);

                //    var maps = desc.ActionAttributes()
                //        .OfType<MapToApiVersionAttribute>()
                //        .SelectMany(attr => attr.Versions)
                //        .ToArray();

                //    return versions.Any(v => $"v{v.ToString()}" == version) && (maps.Length == 0 || maps.Any(v => $"v{v.ToString()}" == version));
                //});

                options.DocInclusionPredicate((version, desc) =>
                {
                    if (!desc.TryGetMethodInfo(out MethodInfo methodInfo)) return false;
                    var versions = methodInfo.DeclaringType
                            .GetCustomAttributes(true)
                            .OfType<ApiVersionAttribute>()
                        .SelectMany(attr => attr.Versions);
                    return versions.Any(v => $"v{v.ToString()}" == version);
                });

                var security = new Dictionary<string, IEnumerable<string>>
                    {
                        {"Bearer",  new string[] { }},
                    };

                options.AddSecurityDefinition("Bearer", new ApiKeyScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = "header",
                    Type = "apiKey"

                });
                options.AddSecurityRequirement(security);


                if (!includeXmlDocumentation) return;
                // Set the comments path for the Swagger JSON and UI.
                var basePath = Directory.GetCurrentDirectory();
                var xmlPath = Path.Combine(basePath, "applevalApi.xml");
                if (File.Exists(xmlPath))
                {
                    options.IncludeXmlComments(xmlPath);
                }

                options.OperationFilter<AuthorizationHeaderParameterOperationFilter>();

            }); // End Swagger gen

            
        }


        /// <summary>
        /// Used to register and add the Swagger generator to the service Colelction
        /// </summary>
        /// <param name="serviceCollection">
        /// The <see cref="IServiceCollection"/> which is used in the Containter
        /// </param>
        /// <param name="versionNumberString">The version number for the application</param>
        /// <param name="includeXmlDocumentation">
        /// Whether or not to include XmlDocumentation (defaults to True)
        /// </param>
        /// <remarks>
        /// <param name="includeXmlDocumentation"/> requries:
        ///   <PropertyGroup Condition="'$(Configuration)|$(Platform)'=='Debug|AnyCPU'">
        ///     <DocumentationFile>bin\Debug\netcoreapp2.1\applevalApi.xml</DocumentationFile>
        ///  </PropertyGroup>
        /// for debug builds and:
        ///   <PropertyGroup Condition="'$(Configuration)|$(Platform)'=='Release|AnyCPU'">
        ///     <DocumentationFile>bin\Release\netcoreapp2.1\applevalApi.xml</DocumentationFile>
        ///  </PropertyGroup>
        /// </remarks>
        public static void AddSwagger(this IServiceCollection serviceCollection, bool includeXmlDocumentation = true)
        {
            // Register the Swagger generator, defining one or more Swagger documents
            //serviceCollection.AddSwaggerGen(c =>
            //{

            //    c.SwaggerDoc($"v{CommonHelpers.GetVersionNumber()}",
            //        new Info
            //        {
            //            Title = "applevalApi",
            //            Version = $"v{CommonHelpers.GetVersionNumber()}",
            //            Description = "Applaudo Studio API Test",
            //            Contact = new Contact
            //            {
            //                Name = "R@ul Berrios Rivera",
            //                Email = "raul.berrios@outlook.com",
            //                Url = ""
            //            }
            //        }
            //    );

            //    var security = new Dictionary<string, IEnumerable<string>>
            //    {
            //        {"Bearer",  new string[] { }},
            //    };

            //    c.AddSecurityDefinition("Bearer", new ApiKeyScheme
            //    {
            //        Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
            //        Name = "Authorization",
            //        In = "header",
            //        Type = "apiKey"

            //    });
            //    c.AddSecurityRequirement(security);




            //    if (!includeXmlDocumentation) return;
            //    // Set the comments path for the Swagger JSON and UI.
            //    var basePath = Directory.GetCurrentDirectory();
            //    var xmlPath = Path.Combine(basePath, "applevalApi.xml");
            //    if (File.Exists(xmlPath))
            //    {
            //        c.IncludeXmlComments(xmlPath);
            //    }




            //}); // swagger gen

            // Configure swagger
            serviceCollection.AddSwaggerGen(options =>
            {
                // Specify two versions 
                options.SwaggerDoc("v1",
                    new Info()
                    {
                        Version = "v1",
                        Title = "applevalApi v1 API",
                        Description = "Applaudo Studio API Test",
                        TermsOfService = "Evaluation API Test Usage v1",
                        Contact = new Contact
                        {
                            Name = "R@ul Berrios Rivera",
                            Email = "raul.berrios@outlook.com",
                            Url = ""
                        }
                    }); ;

                //options.SwaggerDoc("v2",
                //    new Info()
                //    {
                //        Version = "v2",
                //        Title = "v2 API",
                //        Description = "v2 API Description",
                //        TermsOfService = "Terms of usage v2"
                //    });

                // This call remove version from parameter, without it we will have version as parameter 
                // for all endpoints in swagger UI
                options.OperationFilter<RemoveVersionFromParameter>();

                // This make replacement of v{version:apiVersion} to real version of corresponding swagger doc.
                options.DocumentFilter<ReplaceVersionWithExactValueInPath>();

                // This on used to exclude endpoint mapped to not specified in swagger version.
                // In this particular example we exclude 'GET /api/v2/Values/otherget/three' endpoint,
                // because it was mapped to v3 with attribute: MapToApiVersion("3")
                options.DocInclusionPredicate((version, desc) =>
                {
                    var versions = desc.ControllerAttributes()
                        .OfType<ApiVersionAttribute>()
                        .SelectMany(attr => attr.Versions);

                    var maps = desc.ActionAttributes()
                        .OfType<MapToApiVersionAttribute>()
                        .SelectMany(attr => attr.Versions)
                        .ToArray();

                    return versions.Any(v => $"v{v.ToString()}" == version) && (maps.Length == 0 || maps.Any(v => $"v{v.ToString()}" == version));
                });

                var security = new Dictionary<string, IEnumerable<string>>
                    {
                        {"Bearer",  new string[] { }},
                    };

                options.AddSecurityDefinition("Bearer", new ApiKeyScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = "header",
                    Type = "apiKey"

                });
                options.AddSecurityRequirement(security);


                if (!includeXmlDocumentation) return;
                // Set the comments path for the Swagger JSON and UI.
                var basePath = Directory.GetCurrentDirectory();
                var xmlPath = Path.Combine(basePath, "applevalApi.xml");
                if (File.Exists(xmlPath))
                {
                    options.IncludeXmlComments(xmlPath);
                }

            }); // End Swagger gen
        }


    } // End Class
}