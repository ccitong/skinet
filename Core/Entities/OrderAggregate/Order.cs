
namespace Core.Entities.OrderAggregate
{
    public class Order : BaseEntity
    {
        private List<OrderItem> items;
        private Address shippingAddress;
        private DeliveryMethod deliveryMethod;

        public Order()
        {
        }

        public Order(List<OrderItem> items, string buyerEmail, Address shippingAddress, DeliveryMethod deliveryMethod, decimal subtotal)
        {
            this.items = items;
            BuyerEmail = buyerEmail;
            this.shippingAddress = shippingAddress;
            this.deliveryMethod = deliveryMethod;
            Subtotal = subtotal;
        }

        public Order(IReadOnlyList<OrderItem> orderItems, string buyerEmail, Address shipToAddress, 
            DeliveryMethod deliveryMethod, decimal subtotal, string paymentIntentId)
        {
            BuyerEmail = buyerEmail;
            ShipToAddress = shipToAddress;
            DeliveryMethod = deliveryMethod;
            OrderItems = orderItems;
            Subtotal = subtotal;
            PaymentIntentId = paymentIntentId;
        }

        public string BuyerEmail { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.UtcNow;
        public Address ShipToAddress { get; set; }
        public DeliveryMethod DeliveryMethod { get; set; }
        public IReadOnlyList<OrderItem> OrderItems { get; set; }
        public decimal Subtotal { get; set; }
        public OrderStatus Status { get; set; } = OrderStatus.Pending;
        public string PaymentIntentId { get; set; }

        public decimal GetTotal()
        {
            return Subtotal + DeliveryMethod.Price;
        }
    }
}