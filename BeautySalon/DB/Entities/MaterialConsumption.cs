using System.ComponentModel;

namespace BeautySalon.DB.Entities
{
    internal class MaterialConsumption
    {
        [DisplayName("Материал")]
        public int MaterialId { get; set; }

        [DisplayName("Цена")]
        public float Price { get; set; }

        [DisplayName("Количество")]
        public int Amount { get; set; }

        public MaterialConsumption() { }

        public MaterialConsumption(
            int materialId,
            float price,
            int amount
        )
        {
            MaterialId = materialId;
            Price = price;
            Amount = amount;
        }
    }
}
