using MediatR;

namespace Ordering.Domain.Commands
{
    /// <summary>
    /// 订单发货
    /// </summary>
    public class OrderShipCommand : IRequest<bool>
    {
        /// <summary>
        /// 单号
        /// </summary>
        public long OrderNumber { get; private set; }

        /// <summary>
        /// 订单发货
        /// </summary>
        /// <param name="orderNumber">订单No</param>
        public OrderShipCommand(long orderNumber)
        {
            OrderNumber = orderNumber;
        }
    }
}