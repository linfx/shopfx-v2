using MassTransit;
using System;
using System.Threading.Tasks;

namespace Basket.Application.Events
{
    /// <summary>
    /// 通知
    /// </summary>
    public class OrderStartedConsumer : IConsumer<OrderStarted>
    {
        public Task Consume(ConsumeContext<OrderStarted> context)
        {
            throw new NotImplementedException();
        }
    }
}

