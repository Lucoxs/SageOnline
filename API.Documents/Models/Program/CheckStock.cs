namespace API.Documents.Models.Program
{
    public class CheckStock
    {
        public int Id { get; set; }
        public int Stock { get; set; }
        public int Quantity { get; set; }

        public CheckStock(int id, int stock, int quantity)
        {
            Id = id;
            Stock = stock;
            Quantity = quantity;
        }
    }
}
