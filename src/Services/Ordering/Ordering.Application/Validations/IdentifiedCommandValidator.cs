﻿using FluentValidation;
using LinFx.Extensions.Mediator.Idempotency;
using Ordering.Domain.Commands;

namespace Ordering.Domain.Validations
{
    public class IdentifiedCommandValidator : AbstractValidator<IdentifiedCommand<CreateOrderCommand, bool>>
    {
        public IdentifiedCommandValidator()
        {
            RuleFor(command => command.Id).NotEmpty();
        }
    }
}
