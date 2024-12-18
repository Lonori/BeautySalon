namespace BeautySalon.DB.Entities
{
    internal class MaterialArrival
    {
        public int ContractId { get; set; }
        public int MaterialId { get; set; }
        public float Price { get; set; }
        public int Amount { get; set; }

        public MaterialArrival() { }

        public MaterialArrival(
            int contractId,
            int materialId,
            float price,
            int amount
        )
        {
            ContractId = contractId;
            MaterialId = materialId;
            Price = price;
            Amount = amount;
        }
    }
}
