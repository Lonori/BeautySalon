using System.ComponentModel;

namespace BeautySalon.DB.Entities
{
    internal class Material
    {
        [DisplayName("ID")]
        public int Id { get; set; }

        [DisplayName("Материал")]
        [TextMultiline]
        public string Name { get; set; }

        [DisplayName("Цена")]
        public float Price { get; set; }

        public Material() { }

        public Material(
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
