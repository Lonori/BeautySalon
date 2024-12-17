using System.ComponentModel;

namespace BeautySalon.DB.Entities
{
    internal class ServiceFulfilled
    {
        [DisplayName("Услуга")]
        public int ServiceId { get; set; }

        [DisplayName("Цена")]
        public float Price { get; set; }

        public ServiceFulfilled() { }

        public ServiceFulfilled(
            int serviceId,
            float price
        )
        {
            ServiceId = serviceId;
            Price = price;
        }
    }
}
