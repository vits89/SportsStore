namespace SportsStore.AppCore.Entities
{
    public class CartLine
    {
        public int CartLineID { get; set; }
        public int Quantity { get; set; }

        public Product Product { get; set; }
    }
}
