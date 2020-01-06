﻿using MediatR;
using Ordering.Application.Models;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Ordering.Domain.Commands
{
    /// <summary>
    /// 创建订单
    /// DDD and CQRS patterns comment: Note that it is recommended to implement immutable Commands
    // In this case, its immutability is achieved by having all the setters as private
    // plus only being able to update the data just once, when creating the object through its constructor.
    // References on Immutable Commands:  
    // http://cqrs.nu/Faq
    // https://docs.spine3.org/motivation/immutability.html 
    // http://blog.gauffin.org/2012/06/griffin-container-introducing-command-support/
    // https://msdn.microsoft.com/en-us/library/bb383979.aspx
    /// </summary>
    [DataContract]
    public class CreateOrderCommand : IRequest<bool>
    {
        [DataMember]
        private readonly List<OrderItemDto> _orderItems;

        [DataMember]
        public long UserId { get; private set; }

        [DataMember]
        public string UserName { get; private set; }

        [DataMember]
        public string City { get; private set; }

        [DataMember]
        public string Street { get; private set; }

        [DataMember]
        public string State { get; private set; }

        [DataMember]
        public string Country { get; private set; }

        [DataMember]
        public string ZipCode { get; private set; }

        [DataMember]
        public string CardNumber { get; private set; }

        [DataMember]
        public string CardHolderName { get; private set; }

        [DataMember]
        public DateTime CardExpiration { get; private set; }

        [DataMember]
        public string CardSecurityNumber { get; private set; }

        [DataMember]
        public int CardTypeId { get; private set; }

        [DataMember]
        public IEnumerable<OrderItemDto> OrderItems => _orderItems;

        public CreateOrderCommand()
        {
            _orderItems = new List<OrderItemDto>();
        }

        public CreateOrderCommand(List<BasketItem> basketItems, long userId, string userName, string city, string street, string state, string country, string zipcode,
            string cardNumber, string cardHolderName, DateTime cardExpiration,
            string cardSecurityNumber, int cardTypeId) : this()
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
            //_orderItems = basketItems.ToOrderItemsDTO().ToList();
        }

        public class OrderItemDto
        {
            public int ProductId { get; set; }
            public string ProductName { get; set; }
            public decimal UnitPrice { get; set; }
            public decimal Discount { get; set; }
            public int Units { get; set; }
            public string PictureUrl { get; set; }
        }
    }
}
