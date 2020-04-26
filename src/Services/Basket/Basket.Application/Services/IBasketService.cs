using Basket.Application.Models;
using System.Threading.Tasks;

namespace Basket.Api.Services
{
    /// <summary>
    /// 购物车服务
    /// </summary>
    public interface IBasketService
    {
        /// <summary>
        /// 获取客户购物车
        /// </summary>
        /// <param name="customerId">客户Id</param>
        /// <returns></returns>
        Task<CustomerBasket> GetAsync(string customerId);

        /// <summary>
        /// 更新购物车
        /// </summary>
        /// <param name="basket"></param>
        /// <returns></returns>
        Task<CustomerBasket> UpdateAsync(CustomerBasket basket);

        /// <summary>
        /// 删除购物车
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> DeleteAsync(string id);
    }
}
