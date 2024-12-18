namespace BeautySalon.DB.Entities
{
    internal class ServiceFulfilled
    {
        public int OrderId { get; set; }
        public int ServiceId { get; set; }
        public float Price { get; set; }

        public ServiceFulfilled() { }

        public ServiceFulfilled(
            int orderId,
            int serviceId,
            float price
        )
        {
            OrderId = orderId;
            ServiceId = serviceId;
            Price = price;
        }
    }
}
