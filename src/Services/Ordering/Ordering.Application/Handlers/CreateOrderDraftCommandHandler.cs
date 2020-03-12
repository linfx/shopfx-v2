using MediatR;
using Ordering.Application.ViewModels;
using Ordering.Domain.Commands;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Ordering.Application.Handlers
{
    /// <summary>
    /// 订单预览
    /// </summary>
    public class CreateOrderDraftCommandHandler : IRequestHandler<CreateOrderDraftCommand, OrderDraftResult>
    {
        Task<OrderDraftResult> IRequestHandler<CreateOrderDraftCommand, OrderDraftResult>.Handle(CreateOrderDraftCommand request, CancellationToken cancellationToken)
        {
            var order = Domain.Models.OrderAggregate.Order.NewDraft();
            //var orderItems = request.Items.Select(i => i.MapTo<);

            //foreach (var item in orderItems)
            //{
            //    order.AddOrderItem(item.ProductId, item.ProductName, item.UnitPrice, item.Discount, item.PictureUrl, item.Units);
            //}

            var result = order.MapTo<OrderDraftResult>();
            return Task.FromResult(result);
        }
    }
}
