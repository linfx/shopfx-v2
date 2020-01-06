using MediatR;
using System.Runtime.Serialization;

namespace Ordering.Domain.Commands
{
    /// <summary>
    /// 取消订单
    /// </summary>
    public class CancelOrderCommand : IRequest<bool>
    {
        /// <summary>
        /// 单号
        /// </summary>
        [DataMember]
        public int OrderNo { get; set; }
    }
}
