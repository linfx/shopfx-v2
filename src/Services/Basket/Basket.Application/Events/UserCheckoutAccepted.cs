using Basket.Application.Models;
using System;

namespace Basket.Application.Events
{
    /// <summary>
    /// 用户结算
    /// </summary>
    public class UserCheckoutAccepted
    {
        public long UserId { get; }

        public string UserName { get; }

        public string City { get; set; }

        public string Street { get; set; }

        public string State { get; set; }

        public string Country { get; set; }

        public string ZipCode { get; set; }

        public string CardNumber { get; set; }

        public string CardHolderName { get; set; }

        public DateTime CardExpiration { get; set; }

        public string CardSecurityNumber { get; set; }

        public int CardTypeId { get; set; }

        public string Buyer { get; set; }

        public Guid RequestId { get; set; }

        public CustomerBasket Basket { get; }
    }
}
