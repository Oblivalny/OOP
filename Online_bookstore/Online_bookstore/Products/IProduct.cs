namespace Online_bookstore.Products
{
    public interface IProduct : IDiscountProduct
    {
        public ProductTypes Type { get; }
        public string Name { get; }
        public string Author { get; }
        public int Price { get; }
        public bool IsDeliveryPossible { get; }

        public IProduct Copy();
    }
}
