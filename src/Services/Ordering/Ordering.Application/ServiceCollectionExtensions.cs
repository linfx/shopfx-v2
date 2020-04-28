using LinFx.Data.Abstractions;
using LinFx.Extensions.Mediator.Idempotency;
using LinFx.Extensions.MediatR.FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Ordering.Data;
using Ordering.Domain.Commands;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// 加载订单模块
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddOrdering(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddHttpClient()
                .AddTransient(typeof(IRepository<>), typeof(Repository<>))
                .AddTransient(typeof(IRepository<,>), typeof(Repository<,>))
                //services.AddTransient<IAuthorizationHandler, PermissionHandler>();
                //services.AddScoped<SlugRouteValueTransformer>();
                //.AddScoped<ServiceFactory>(p => p.GetService)
                //.AddScoped<IMediator, SequentialMediator>()
                .AddMemoryCache()
                .AddDistributedMemoryCache();
            //.AddSingleton(new CacheOptions
            //{
            //    RedisCachingConnection = config.RedisCachingConnection,
            //    RedisCachingEnabled = config.RedisCachingEnabled
            //})
            //.AddScoped<ICacheManager, PerRequestCacheManager>();

            services
                .AddTransient<IRequestManager, RequestManager>()
                .AddSingleton<IRequestHandler<IdentifiedCommand<OrderCancelCommand, bool>, bool>, IdentifiedCommandHandler<OrderCancelCommand, bool>>()
                .AddMediatR(typeof(OrderingContext).Assembly)
                .AddFluentValidation(typeof(OrderingContext).Assembly);

            return services.AddDbContextPool<OrderingContext>(options =>
            {
                options.EnableSensitiveDataLogging();
                options.UseMySql(configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly("Ordering.Api"));
            });
        }
    }
}