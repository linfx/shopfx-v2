using Ordering.Application.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ordering.Application.Services
{
    public interface IOrderService
    {
        /// <summary>
        /// 获取订单
        /// </summary>
        /// <param name="id">订单Id</param>
        /// <returns></returns>
        Task<Order> GetOrderAsync(long id);

        /// <summary>
        /// 获取用户订单
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<IEnumerable<OrderSummary>> GetOrdersFromUserAsync(string userId);

        Task<IEnumerable<CardType>> GetCardTypesAsync();
    }
}
