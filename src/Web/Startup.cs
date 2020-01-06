using System.Reflection;
using Catalog.EntityFrameworkCore;
using Mall.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace Mall.Web
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
            services.AddDbContext<CatalogContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly(typeof(CatalogContext).Assembly.FullName)));

            services.AddLinFx()
                .AddCatalog()
                .AddEventBus(options =>
                {
                    options.UseRabbitMQ(x =>
                    {
                        x.Host = Configuration.GetConnectionString("RibbitMqConnection");
                        x.UserName = "admin";
                        x.Password = "admin.123456";
                        x.BrokerName = "shopfx_event_bus";
                        x.QueueName = "shopfx_process_queue";
                    });
                })
                .AddDbContext<CatalogContext>(options =>
                {
                    options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
                });

            services.AddHttpClient<ICatalogService, CatalogService>();

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddMvc()
                .AddApplicationPart(Assembly.GetAssembly(typeof(Catalog.Api.Startup)))
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            //services.AddSwaggerGen(options =>
            //{
            //    options.DescribeAllEnumsAsStrings();
            //    options.SwaggerDoc("v1", new Swashbuckle.AspNetCore.Swagger.Info
            //    {
            //        Title = "ShopFx - Catalog HTTP API",
            //        Version = "v1",
            //        Description = "The Catalog Microservice HTTP API.",
            //    });
            //    //options.DocInclusionPredicate((version, apiDescription) =>
            //    //{
            //    //    var values = apiDescription.RelativePath
            //    //        .Split('/')
            //    //        .Select(v => v.Replace("v{version}", version));

            //    //    apiDescription.RelativePath = string.Join("/", values);

            //    //    return true;
            //    //});
            //});
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvcWithDefaultRoute();
        }
    }
}
