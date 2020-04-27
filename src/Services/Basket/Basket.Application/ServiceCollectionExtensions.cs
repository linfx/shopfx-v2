using Basket.Api.Services;
using LinFx.Extensions.Mediator.Idempotency;
using LinFx.Extensions.MediatR.FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Basket.Application
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// 加载购物车模块
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddBasket(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddHttpClient()
                //.AddTransient(typeof(IRepository<>), typeof(Repository<>))
                //.AddTransient(typeof(IRepository<,>), typeof(Repository<,>))
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
                //.AddSingleton<IRequestHandler<IdentifiedCommand<CancelOrderCommand, bool>, bool>, IdentifiedCommandHandler<CancelOrderCommand, bool>>()
                .AddMediatR(typeof(IBasketService).Assembly)
                .AddFluentValidation(typeof(IBasketService).Assembly);

            return services;
        }
    }
}
