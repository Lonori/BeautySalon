using System.ComponentModel;

namespace BeautySalon.DB.Entities
{
    internal class MaterialArrival
    {
        [DisplayName("Материал")]
        public int MaterialId { get; set; }

        [DisplayName("Количество")]
        public int Amount { get; set; }

        public MaterialArrival() { }

        public MaterialArrival(
            int materialId,
            int amount
        )
        {
            MaterialId = materialId;
            Amount = amount;
        }
    }
}
