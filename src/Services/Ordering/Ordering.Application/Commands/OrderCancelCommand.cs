using MediatR;
using System.Runtime.Serialization;

namespace Ordering.Domain.Commands
{
    /// <summary>
    /// 订单取消命令
    /// </summary>
    public class OrderCancelCommand : IRequest<bool>
    {
        /// <summary>
        /// 订单No
        /// </summary>
        [DataMember]
        public long OrderNumber { get; set; }
    }
}
