using Microsoft.OpenApi.Models;
using System;
using System.IO;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddModules(this IServiceCollection services)
        {
            //foreach (var module in new ModuleConfigurationManager().GetModules())
            //{
            //    module.Assembly = Assembly.Load(new AssemblyName(module.Id));
            //    var moduleInitializerType = module.Assembly.GetTypes().FirstOrDefault(t => typeof(IModuleInitializer).IsAssignableFrom(t));
            //    if (moduleInitializerType != null && moduleInitializerType != typeof(IModuleInitializer))
            //    {
            //        services.AddSingleton(typeof(IModuleInitializer), moduleInitializerType);
            //    }
            //    Global.Modules.Add(module);
            //}

            //var sp = services.BuildServiceProvider();
            //var moduleInitializers = sp.GetServices<IModuleInitializer>();
            //foreach (var moduleInitializer in moduleInitializers)
            //{
            //    moduleInitializer.ConfigureServices(services);
            //}
            return services;
        }

        public static IServiceCollection AddCustomizedMvc(this IServiceCollection services)
        {
            // Add framework services.
            services.AddControllers(options =>
            {
                //options.Filters.Add(typeof(HttpGlobalExceptionFilter));
            })
            // Added for functional tests
            //.AddApplicationPart(typeof(OrdersController).Assembly)
            //.AddNewtonsoftJson()
            //.SetCompatibilityVersion(CompatibilityVersion.Version_3_0)
            ;

            return services;
        }

        public static IServiceCollection AddCustomizedSwagger(this IServiceCollection services)
        {
            return services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ShopFx Api", Version = "v2" });
                c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "Ordering.Api.xml"), true);
            });
        }
    }
}
