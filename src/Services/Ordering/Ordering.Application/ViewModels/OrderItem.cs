using System.ComponentModel.DataAnnotations;

namespace Ordering.Application.ViewModels
{
    /// <summary>
    /// 订单明细视图
    /// </summary>
    public class OrderItem
    {
        /// <summary>
        /// 商品Id
        /// </summary>
        [Required]
        public long ProductId { get; set; }

        /// <summary>
        /// 商品名称
        /// </summary>
        [Required]
        public string ProductName { get; set; }

        /// <summary>
        /// 价格
        /// </summary>
        [Required]
        public decimal UnitPrice { get; set; }

        /// <summary>
        /// 折扣
        /// </summary>
        [Required]
        public decimal Discount { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        [Required]
        public int Units { get; set; }

        /// <summary>
        /// 商品图片
        /// </summary>
        public string PictureUrl { get; set; }
    }
}
