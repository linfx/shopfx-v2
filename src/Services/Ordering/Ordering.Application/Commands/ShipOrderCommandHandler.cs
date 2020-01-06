using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Ordering.Domain.Commands
{
    // Regular CommandHandler
    public class ShipOrderCommandHandler : IRequestHandler<ShipOrderCommand, bool>
    {
        //private readonly IOrderRepository _orderRepository;

        //public ShipOrderCommandHandler(IOrderRepository orderRepository)
        //{
        //    _orderRepository = orderRepository;
        //}

        /// <summary>
        /// Handler which processes the command when
        /// administrator executes ship order from app
        /// </summary>
        /// <param name="command"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<bool> Handle(ShipOrderCommand command, CancellationToken cancellationToken)
        {
            //var orderToUpdate = await _orderRepository.GetAsync(command.OrderNumber);
            //if (orderToUpdate == null)
            //{
            //    return false;
            //}

            //orderToUpdate.SetShippedStatus();
            //await _orderRepository.UnitOfWork.SaveChangesAsync();
            return true;
        }
    }
}
