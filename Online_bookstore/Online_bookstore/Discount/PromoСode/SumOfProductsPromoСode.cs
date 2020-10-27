using System;
using System.Collections.Generic;
using System.Text;
using Online_bookstore.Basket;

namespace Online_bookstore.PromoСode
{
    class SumOfProductsPromoСode : IDiscount
    {
        int sum;

        public SumOfProductsPromoСode(int sum)
        {
            this.sum = sum;
        }
        public int GetDiscount(BasketGoods basket)
        {
            return sum;
        }
    }
}
