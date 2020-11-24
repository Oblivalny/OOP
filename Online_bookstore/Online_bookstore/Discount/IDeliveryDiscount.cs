using Online_bookstore.Basket;

namespace Online_bookstore.Discount
{
    public interface IDeliveryDiscount : IDiscount
    {
        int GetTotalDiscount(IBasket basket);
    }
}
