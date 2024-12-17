using System.ComponentModel;

namespace BeautySalon.DB.Entities
{
    internal class Supplier
    {
        [DisplayName("ID")]
        public int Id { get; set; }

        [DisplayName("Поставщик")]
        [TextMultiline]
        public string Name { get; set; }

        [DisplayName("Юр. Адрес")]
        public string Address { get; set; }

        [DisplayName("ИНН")]
        public string INN { get; set; }

        [DisplayName("КПП")]
        public string KPP { get; set; }

        public Supplier() { }

        public Supplier(
            int id,
            string name,
            string address,
            string inn,
            string kpp
        )
        {
            Id = id;
            Name = name;
            Address = address;
            INN = inn;
            KPP = kpp;
        }
    }
}
