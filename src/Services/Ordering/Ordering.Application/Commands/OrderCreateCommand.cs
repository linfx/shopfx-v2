using MediatR;
using Ordering.Application.Models;
using Ordering.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Ordering.Domain.Commands
{
    /// <summary>
    /// 订单创建
    /// </summary>
    public partial class OrderCreateCommand : IRequest<bool>
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        public long UserId { get; private set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; private set; }

        public string City { get; private set; }

        public string Street { get; private set; }

        public string State { get; private set; }

        public string Country { get; private set; }

        public string ZipCode { get; private set; }

        public string CardNumber { get; private set; }

        public string CardHolderName { get; private set; }

        public DateTime CardExpiration { get; private set; }

        public string CardSecurityNumber { get; private set; }

        public int CardTypeId { get; private set; }

        /// <summary>
        /// 商品明细
        /// </summary>
        public IEnumerable<OrderItem> OrderItems { get; } = new List<OrderItem>();

        public OrderCreateCommand(List<BasketItem> basketItems, long userId, string userName,
            string city, string street, string state, string country, string zipcode,
            string cardNumber, string cardHolderName, DateTime cardExpiration,
            string cardSecurityNumber, int cardTypeId)
        {
            UserId = userId;
            UserName = userName;
            City = city;
            Street = street;
            State = state;
            Country = country;
            ZipCode = zipcode;
            CardNumber = cardNumber;
            CardHolderName = cardHolderName;
            CardExpiration = cardExpiration;
            CardSecurityNumber = cardSecurityNumber;
            CardTypeId = cardTypeId;
            CardExpiration = cardExpiration;
            OrderItems = basketItems.MapTo<OrderItem[]>().ToList();
        }
    }
}