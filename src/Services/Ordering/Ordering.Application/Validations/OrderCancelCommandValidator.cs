using FluentValidation;
using Ordering.Domain.Commands;

namespace Ordering.Domain.Validations
{
    /// <summary>
    /// 订单取消验证
    /// </summary>
    public class OrderCancelCommandValidator : AbstractValidator<OrderCancelCommand>
    {
        public OrderCancelCommandValidator()
        {
            RuleFor(order => order.OrderNumber).NotEmpty().WithMessage("找不到订单");
        }
    }
}
