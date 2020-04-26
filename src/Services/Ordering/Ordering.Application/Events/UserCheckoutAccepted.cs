using Ordering.Application.Models;
using System;

namespace Ordering.Application.Events
{
    /// <summary>
    /// 结算消息
    /// </summary>
    public class UserCheckoutAccepted
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        public long UserId { get; set; }

        /// <summary>
        /// 用户账号
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 客户
        /// </summary>
        public string Buyer { get; set; }

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

        public Guid RequestId { get; set; }

        /// <summary>
        /// 购物车
        /// </summary>
        public CustomerBasket Basket { get; }
    }
}
