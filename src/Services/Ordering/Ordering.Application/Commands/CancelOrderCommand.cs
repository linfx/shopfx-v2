using MediatR;
using System.Runtime.Serialization;

namespace Ordering.Domain.Commands
{
    /// <summary>
    /// 订单取消
    /// </summary>
    public class CancelOrderCommand : IRequest<bool>
    {
        /// <summary>
        /// 单号
        /// </summary>
        [DataMember]
        public long OrderNumber { get; set; }
    }
}
