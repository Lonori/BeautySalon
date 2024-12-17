using System.ComponentModel;

namespace BeautySalon.DB.Entities
{
    internal class Service
    {
        [DisplayName("ID")]
        public int Id { get; set; }

        [DisplayName("Услуга")]
        [TextMultiline]
        public string Name { get; set; }

        [DisplayName("Цена")]
        public float Price { get; set; }

        public Service() { }

        public Service(
            int id,
            string name,
            float price
        )
        {
            Id = id;
            Name = name;
            Price = price;
        }
    }
}
