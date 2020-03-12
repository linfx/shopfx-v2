using MediatR;
using Ordering.Application.Models;
using Ordering.Application.ViewModels;
using System.Collections.Generic;

namespace Ordering.Domain.Commands
{
    /// <summary>
    /// 创建订单预览命令
    /// </summary>
    public class CreateOrderDraftCommand : IRequest<OrderDraftResult>
    {
        /// <summary>
        /// 客户Id
        /// </summary>
        public string BuyerId { get; set; }

        /// <summary>
        /// 购物车明细
        /// </summary>
        public IEnumerable<BasketItem> Items { get; set; }
    }
}
