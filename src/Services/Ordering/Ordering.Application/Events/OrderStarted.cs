namespace Ordering.Application.Events
{
    public class OrderStarted
    {
        public string UserId { get; set; }

        public OrderStarted(string userId) => UserId = userId;
    }
}
