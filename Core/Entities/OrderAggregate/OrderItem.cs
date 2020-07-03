namespace Core.Entities.OrderAggregate
{
    public class OrderItem : BaseEntity
    {
        public OrderItem()
        {
        }

        public OrderItem(ProductItemOrdered itemeOrdered, decimal price, int quantity)
        {
            ItemeOrdered = itemeOrdered;
            Price = price;
            Quantity = quantity;
        }

        public ProductItemOrdered ItemeOrdered { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}