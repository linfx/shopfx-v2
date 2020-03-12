using System;

namespace Ordering.Application.ViewModels
{
    public class OrderSummary
    {
        public long OrderNumber { get; set; }

        public DateTime Date { get; set; }

        public string Status { get; set; }

        public double Total { get; set; }
    }
}
