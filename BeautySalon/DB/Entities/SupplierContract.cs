using System;

namespace BeautySalon.DB.Entities
{
    internal class SupplierContract
    {
        public int Id { get; set; }
        public DateTime Time { get; set; }
        public int SupplierId { get; set; }

        public SupplierContract() { }

        public SupplierContract(
            int id,
            DateTime time,
            int supplierId
        )
        {
            Id = id;
            Time = time;
            SupplierId = supplierId;
        }
    }
}
