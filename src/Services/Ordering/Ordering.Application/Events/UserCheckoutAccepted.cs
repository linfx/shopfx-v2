using Ordering.Application.Models;
using System;

namespace Ordering.Application.Events
{
    /// <summary>
    /// 用户结算
    /// </summary>
    public class UserCheckoutAccepted
    {
        public long UserId { get; set; }

        public string UserName { get; set; }

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

        /// <summary>
        /// 购物车
        /// </summary>
        public CustomerBasket Basket { get; }
    }
}
