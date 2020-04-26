using System;

namespace Basket.Application.Models
{
    /// <summary>
    /// 结算表单
    /// </summary>
    public class BasketCheckout
    {
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

        /// <summary>
        /// 请求Id
        /// </summary>
        public Guid RequestId { get; set; }
    }
}
