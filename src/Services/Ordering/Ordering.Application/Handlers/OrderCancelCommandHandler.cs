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
    /// 取消订单命令处理器
    /// </summary>
    public class OrderCancelCommandHandler : IRequestHandler<OrderCancelCommand, bool>
    {
        private readonly IRepository<Order> _orderRepository;

        public OrderCancelCommandHandler(IRepository<Order> orderRepository)
        {
            _orderRepository = orderRepository;
        }

        /// <summary>
        /// Handler which processes the command when
        /// customer executes cancel order from app
        /// </summary>
        /// <param name="command"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<bool> Handle(OrderCancelCommand command, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.FirstOrDefaultAsync(command.OrderNumber);
            if (order == null)
                return false;

            order.SetCancelledStatus();
            await _orderRepository.SaveChangesAsync();
            return true;
        }
    }
}
