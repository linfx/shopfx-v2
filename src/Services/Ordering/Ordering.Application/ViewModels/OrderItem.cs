namespace Ordering.Application.ViewModels
{
    public class OrderItem
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Discount { get; set; }

        /// <summary>
        /// 单位
        /// </summary>
        public int Units { get; set; }
        public string PictureUrl { get; set; }
    }
}
