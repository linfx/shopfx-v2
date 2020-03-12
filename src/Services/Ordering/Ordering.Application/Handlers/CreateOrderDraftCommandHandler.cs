using MediatR;
using Ordering.Application.ViewModels;
using Ordering.Domain.Commands;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Ordering.Application.Handlers
{
    /// <summary>
    /// 订单预览
    /// </summary>
    public class CreateOrderDraftCommandHandler : IRequestHandler<CreateOrderDraftCommand, OrderDraft>
    {
        Task<OrderDraft> IRequestHandler<CreateOrderDraftCommand, OrderDraft>.Handle(CreateOrderDraftCommand request, CancellationToken cancellationToken)
        {
            var order = Domain.Models.OrderAggregate.Order.NewDraft();
            var orderItems = request.Items.Select(p => p.MapTo<OrderItem>());

            foreach (var item in orderItems)
            {
                order.AddOrderItem(item.ProductId, item.ProductName, item.UnitPrice, item.Discount, item.PictureUrl, item.Units);
            }

            var result = order.MapTo<OrderDraft>();
            return Task.FromResult(result);
        }
    }
}
