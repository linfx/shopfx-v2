using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Ordering.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Ordering.Data
{
    public class OrderingContext : LinFx.Data.DbContext
    {
        public OrderingContext(DbContextOptions<OrderingContext> options) : base(options) { }

        public OrderingContext(DbContextOptions options, IMediator mediator) : base(options, mediator) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var typeToRegisters = new List<Type>();
            foreach (var module in Global.Modules)
            {
                typeToRegisters.AddRange(module.Assembly.DefinedTypes.Select(t => t.AsType()));
            }

            //RegisterEntities(modelBuilder, typeToRegisters);
            base.OnModelCreating(modelBuilder);
            //modelBuilder.ApplyConfiguration(new ClientRequestEntityTypeConfiguration());
            //modelBuilder.ApplyConfiguration(new PaymentMethodEntityTypeConfiguration());
            //modelBuilder.ApplyConfiguration(new OrderEntityTypeConfiguration());
            //modelBuilder.ApplyConfiguration(new OrderItemEntityTypeConfiguration());
            //modelBuilder.ApplyConfiguration(new CardTypeEntityTypeConfiguration()); 
            //modelBuilder.ApplyConfiguration(new OrderStatusEntityTypeConfiguration());
            //modelBuilder.ApplyConfiguration(new BuyerEntityTypeConfiguration());
        }
    }

    public class OrderingContextDesignFactory : IDesignTimeDbContextFactory<OrderingContext>
    {
        public OrderingContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<OrderingContext>()
                .UseMySql("server=10.0.1.222;port=3316;database=shopfx-v2;uid=root;pwd=123456;Connection Timeout=60;Allow Zero Datetime=True;Allow User Variables=True;pooling=true;min pool size=5;max pool size=512;SslMode=None;");

            return new OrderingContext(optionsBuilder.Options);
        }
    }
}