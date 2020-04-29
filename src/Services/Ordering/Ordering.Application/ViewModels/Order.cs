using System;
using System.Collections.Generic;

namespace Ordering.Application.ViewModels
{
    public class Order
    {
        public long OrderNumber { get; set; }
        public string Status { get; set; }
        public string Description { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string Zipcode { get; set; }
        public string Country { get; set; }
        public decimal Total { get; set; }
        public DateTime Date { get; set; }
        public List<OrderItem> OrderItems { get; set; }
    }
}