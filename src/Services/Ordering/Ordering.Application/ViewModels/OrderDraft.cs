using System.Collections.Generic;

namespace Ordering.Application.ViewModels
{
    public class OrderDraft
    {
        public IEnumerable<OrderItem> OrderItems { get; set; }
    }

    //public class OrderDraftDTO
    //{
    //    public IEnumerable<OrderItemDto> OrderItems { get; set; }
    //    public decimal Total { get; set; }

    //    public static OrderDraftDTO FromOrder(Order order)
    //    {
    //        return new OrderDraftDTO()
    //        {
    //            OrderItems = order.OrderItems.Select(oi => new OrderItemDto
    //            {
    //                Discount = oi.GetCurrentDiscount(),
    //                ProductId = oi.ProductId,
    //                UnitPrice = oi.GetUnitPrice(),
    //                PictureUrl = oi.GetPictureUri(),
    //                Units = oi.GetUnits(),
    //                ProductName = oi.GetOrderItemProductName()
    //            }),
    //            Total = order.GetTotal()
    //        };
    //    }
    //}
}
