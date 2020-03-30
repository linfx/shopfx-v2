using LinFx.Domain.Models;

namespace Ordering.Domain.Models.OrderAggregate
{
    public class OrderItem : Entity<long>
    {
        private readonly string _productName;
        private readonly string _pictureUrl;
        private readonly decimal _unitPrice;
        private decimal _discount;
        private int _units;
        public int ProductId { get; private set; }

        protected OrderItem() { }

        public OrderItem(int productId, string productName, decimal unitPrice, decimal discount, string pictureUrl, int units = 1)
        {
            if (units <= 0)
                throw new OrderingDomainException("Invalid number of units");

            if (unitPrice * units < discount)
                throw new OrderingDomainException("The total of order item is lower than applied discount");

            ProductId = productId;
            _productName = productName;
            _unitPrice = unitPrice;
            _discount = discount;
            _units = units;
            _pictureUrl = pictureUrl;
        }

        public string GetPictureUri() => _pictureUrl;

        public decimal GetCurrentDiscount() => _discount;

        public int GetUnits() => _units;

        public decimal GetUnitPrice() => _unitPrice;

        public string GetOrderItemProductName() => _productName;

        /// <summary>
        /// 设置新折扣
        /// </summary>
        /// <param name="discount"></param>
        public void SetNewDiscount(decimal discount)
        {
            if (discount < 0)
                throw new OrderingDomainException("Discount is not valid");

            _discount = discount;
        }

        /// <summary>
        /// 增加数量
        /// </summary>
        /// <param name="units"></param>
        public void AddUnits(int units)
        {
            if (units < 0)
                throw new OrderingDomainException("Invalid units");

            _units += units;
        }
    }
}
