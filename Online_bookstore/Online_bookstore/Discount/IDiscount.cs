using System;
using System.Collections.Generic;
using System.Text;
using Online_bookstore.Products;
using Online_bookstore.Basket;

namespace Online_bookstore.PromoСode
{
    interface IDiscount
    {
        public int GetDiscount(BasketGoods basket);
    }
}
