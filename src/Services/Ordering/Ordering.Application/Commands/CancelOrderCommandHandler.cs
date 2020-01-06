﻿using LinFx.Data;
using LinFx.Extensions.Mediator.Idempotency;
using MediatR;
using Ordering.Domain.Models.OrderAggregate;
using System.Threading;
using System.Threading.Tasks;

namespace Ordering.Domain.Commands
{
    /// <summary>
    /// 取消订单命令处理器
    /// </summary>
    public class CancelOrderCommandHandler : IRequestHandler<CancelOrderCommand, bool>
    {
        //private readonly IRepository<Order> _orderRepository;

        //public CancelOrderCommandHandler(IRepository<Order> orderRepository)
        //{
        //    _orderRepository = orderRepository;
        //}

        /// <summary>
        /// Handler which processes the command when
        /// customer executes cancel order from app
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public async Task<bool> Handle(CancelOrderCommand command, CancellationToken cancellationToken)
        {
            //var order = await _orderRepository.FirstOrDefaultAsync(command.OrderNumber);
            //if (order == null)
            //    return false;

            //order.SetCancelledStatus();
            //await _orderRepository.UnitOfWork.SaveChangesAsync();
            return true;
        }
    }
}
