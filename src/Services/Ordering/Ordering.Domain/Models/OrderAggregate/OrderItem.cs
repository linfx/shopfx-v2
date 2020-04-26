using LinFx.Domain.Models;

namespace Ordering.Domain.Models.OrderAggregate
{
    /// <summary>
    /// 订单明细
    /// </summary>
    public class OrderItem : Entity<long>
    {
        private readonly string _productName;
        private readonly string _pictureUrl;
        private readonly decimal _unitPrice;
        private decimal _discount;
        private int _units;
        public long ProductId { get; private set; }

        /// <summary>
        /// 订单明细
        /// </summary>
        protected OrderItem() { }

        /// <summary>
        /// 订单明细
        /// </summary>
        /// <param name="productId">商品Id</param>
        /// <param name="productName">商品名称</param>
        /// <param name="unitPrice">单价</param>
        /// <param name="discount">折扣</param>
        /// <param name="pictureUrl">商品图片</param>
        /// <param name="units">数量</param>
        public OrderItem(long productId, string productName, decimal unitPrice, decimal discount, string pictureUrl, int units = 1)
        {
            if (units <= 0)
                throw new OrderingDomainException("Invalid number of units");

            if (unitPrice * units < discount)
                throw new OrderingDomainException("The total of order item is lower than applied discount");

            ProductId = productId;
            _productName = productName;
            _unitPrice = unitPrice;
            _discount = discount;
            _pictureUrl = pictureUrl;
            _units = units;
        }

        /// <summary>
        /// 获取图片Uri
        /// </summary>
        /// <returns></returns>
        public string GetPictureUri() => _pictureUrl;

        /// <summary>
        /// 获取当前折扣
        /// </summary>
        /// <returns></returns>
        public decimal GetCurrentDiscount() => _discount;

        /// <summary>
        /// 获取数量
        /// </summary>
        /// <returns></returns>
        public int GetUnits() => _units;

        /// <summary>
        /// 获取单价
        /// </summary>
        /// <returns></returns>
        public decimal GetUnitPrice() => _unitPrice;

        /// <summary>
        /// 获取商品名称
        /// </summary>
        /// <returns></returns>
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
