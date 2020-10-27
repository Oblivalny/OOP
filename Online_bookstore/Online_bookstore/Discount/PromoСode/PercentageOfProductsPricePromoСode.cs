using System;
using System.Collections.Generic;
using System.Text;
using Online_bookstore.Basket;

namespace Online_bookstore.PromoСode
{
    class PercentageOfProductsPricePromoСode : IDiscount
    {
        public int percent;

        public PercentageOfProductsPricePromoСode(int percent)
        {
            this.percent = percent;
        }
        public int GetDiscount(BasketGoods basket)
        {
            return basket.GetPriceProducts() * (1 - this.percent);
        }
    }
}
