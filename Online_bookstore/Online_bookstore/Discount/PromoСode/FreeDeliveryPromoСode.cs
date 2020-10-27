using System;
using System.Collections.Generic;
using System.Text;
using Online_bookstore.Basket;

namespace Online_bookstore.PromoСode
{
    class FreeDeliveryPromoСode : IDiscount
    {
        public int GetDiscount(BasketGoods basket)
        {
            return basket.GetPriceDelivery();
        }
    }
}

