namespace Order.API.Models
    {
    public class CreateOrderRequest
        {
        public required string CustomerName { get; set; }
        public required string Email { get; set; }
        public decimal Amount { get; set; }
        }
    }
