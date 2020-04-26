using LinFx.Data;
using LinFx.Data.Abstractions;
using LinFx.Data.Linq;
using MediatR;
using Ordering.Domain.Commands;
using Ordering.Domain.Models.OrderAggregate;
using System.Threading;
using System.Threading.Tasks;

namespace Ordering.Application.Handlers
{
    /// <summary>
    /// 订单发货
    /// </summary>
    public class ShipOrderCommandHandler : IRequestHandler<ShipOrderCommand, bool>
    {
        private readonly IRepository<Order> _orderRepository;

        public ShipOrderCommandHandler(IRepository<Order> orderRepository)
        {
            _orderRepository = orderRepository;
        }

        /// <summary>
        /// Handler which processes the command when
        /// administrator executes ship order from app
        /// </summary>
        /// <param name="command"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<bool> Handle(ShipOrderCommand command, CancellationToken cancellationToken)
        {
            var orderToUpdate = await _orderRepository.FirstOrDefaultAsync(command.OrderNumber);
            if (orderToUpdate == null)
                return false;

            orderToUpdate.SetShippedStatus();
            await _orderRepository.SaveChangesAsync();
            return true;
        }
    }
}
