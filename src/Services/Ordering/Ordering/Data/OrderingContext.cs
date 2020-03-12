using MediatR;
using Microsoft.EntityFrameworkCore;
using Ordering.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Ordering.Data
{
    public class OrderingContext : LinFx.Data.DbContext
    {
        public const string DEFAULT_SCHEMA = "mallfx.services.ordering";

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
}