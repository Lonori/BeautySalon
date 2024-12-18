namespace BeautySalon.DB.Entities
{
    internal class MaterialArrival
    {
        public int ContractId { get; set; }
        public int MaterialId { get; set; }
        public int Amount { get; set; }

        public MaterialArrival() { }

        public MaterialArrival(
            int contractId,
            int materialId,
            int amount
        )
        {
            ContractId = contractId;
            MaterialId = materialId;
            Amount = amount;
        }
    }
}
