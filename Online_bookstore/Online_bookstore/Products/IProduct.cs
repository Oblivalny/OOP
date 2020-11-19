namespace Online_bookstore.Products
{
    public interface IProduct
    {
        public ProductTypes Type { get; }
        public string Name { get; }
        public string Author { get; }
        public int Price { get; }
        public bool IsDeliveryPossible { get; }
    }
}
