using Online_bookstore.Basket;

namespace Online_bookstore.Discount.PromoCode
{
    public class FreeDeliveryPromoCode : IDiscount
    {
        public int GetDiscount(IBasket basket)
        {
            return basket.PriceDelivery;
        }
    }
}
