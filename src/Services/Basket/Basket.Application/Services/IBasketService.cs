using Basket.Application.Models;
using System.Threading.Tasks;

namespace Basket.Api.Services
{
    public interface IBasketService
    {
        Task<CustomerBasket> GetBasketAsync(string customerId);
        Task<CustomerBasket> UpdateBasketAsync(CustomerBasket basket);
        Task<bool> DeleteBasketAsync(string id);
    }
}
