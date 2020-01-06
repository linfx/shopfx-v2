namespace Basket.Application.Models
{
    /// <summary>
    /// 购物车明细
    /// </summary>
    public class BasketItem
    {
        public string Id { get; set; }
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal OldUnitPrice { get; set; }
        public string PictureUrl { get; set; }
        public int Quantity { get; set; }
    }
}
