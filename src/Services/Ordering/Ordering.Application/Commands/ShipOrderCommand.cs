using MediatR;
using System.Runtime.Serialization;

namespace Ordering.Domain.Commands
{
    /// <summary>
    /// 订单发货
    /// </summary>
    public class ShipOrderCommand : IRequest<bool>
    {
        /// <summary>
        /// 单号
        /// </summary>
        [DataMember]
        public long OrderNumber { get; private set; }

        public ShipOrderCommand(long orderNumber)
        {
            OrderNumber = orderNumber;
        }
    }
}