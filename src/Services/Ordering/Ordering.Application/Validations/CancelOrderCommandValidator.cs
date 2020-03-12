﻿using FluentValidation;
using Ordering.Domain.Commands;

namespace Ordering.Domain.Validations
{
    public class CancelOrderCommandValidator : AbstractValidator<CancelOrderCommand>
    {
        public CancelOrderCommandValidator()
        {
            RuleFor(order => order.OrderNumber).NotEmpty().WithMessage("找不到订单");
        }
    }
}
