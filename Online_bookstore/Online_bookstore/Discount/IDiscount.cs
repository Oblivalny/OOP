using Online_bookstore.Basket;

namespace Online_bookstore.Discount
{
    public interface IDiscount
    {
        int GetDiscount(IBasket basket);
    }
}
