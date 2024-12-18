namespace BeautySalon.DB.Entities
{
    internal class MaterialConsumption
    {
        public int OrderId { get; set; }
        public int MaterialId { get; set; }
        public float Price { get; set; }
        public int Amount { get; set; }

        public MaterialConsumption() { }

        public MaterialConsumption(
            int orderId,
            int materialId,
            float price,
            int amount
        )
        {
            OrderId = orderId;
            MaterialId = materialId;
            Price = price;
            Amount = amount;
        }
    }
}
