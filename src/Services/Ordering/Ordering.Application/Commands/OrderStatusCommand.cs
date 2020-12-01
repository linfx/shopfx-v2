using MediatR;
using System.Collections.Generic;

namespace Ordering.Domain.Commands
{
    public class SetAwaitingValidationOrderStatusCommand : IRequest<bool>
    {
        public long OrderNumber { get; private set; }

        public SetAwaitingValidationOrderStatusCommand(long orderNumber)
        {
            OrderNumber = orderNumber;
        }
    }

    public class SetPaidOrderStatusCommand : IRequest<bool>
    {
        public long OrderNumber { get; private set; }

        public SetPaidOrderStatusCommand(long orderNumber)
        {
            OrderNumber = orderNumber;
        }
    }

    public class SetStockConfirmedOrderStatusCommand : IRequest<bool>
    {
        public long OrderNumber { get; private set; }

        public SetStockConfirmedOrderStatusCommand(long orderNumber)
        {
            OrderNumber = orderNumber;
        }
    }

    public class SetStockRejectedOrderStatusCommand : IRequest<bool>
    {
        public long OrderNumber { get; private set; }

        public List<int> OrderStockItems { get; private set; }

        public SetStockRejectedOrderStatusCommand(long orderNumber, List<int> orderStockItems)
        {
            OrderNumber = orderNumber;
            OrderStockItems = orderStockItems;
        }
    }
}