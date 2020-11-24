using Online_bookstore.Discount;

namespace Online_bookstore.Products
{
    public interface IDiscountProduct
    {
        public int Discount { get; }

        public bool ApplyDiscount(IDiscount applyDiscount);
        public bool CancelDiscount(IDiscount cancelDiscount);
        public void ClearDiscount();
    }
}
