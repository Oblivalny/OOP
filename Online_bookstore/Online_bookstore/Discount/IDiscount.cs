using Online_bookstore.Products;

namespace Online_bookstore.Discount
{
    public interface IDiscount
    {
        int GetDiscount(IProduct product);
    }
}
