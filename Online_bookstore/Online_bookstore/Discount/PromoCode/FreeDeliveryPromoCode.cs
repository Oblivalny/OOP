using Online_bookstore.Basket;
using Online_bookstore.Products;

namespace Online_bookstore.Discount.PromoCode
{
    public class FreeDeliveryPromoCode : IPromoCode, IDeliveryDiscount
    {
        public int GetTotalDiscount(IBasket basket)
        {
            return basket.PriceDelivery;
        }

        public int GetDiscount(IProduct product)
        {
            return 0;
        }
    }
}
