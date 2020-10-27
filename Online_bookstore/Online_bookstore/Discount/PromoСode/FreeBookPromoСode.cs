using System;
using System.Collections.Generic;
using System.Text;
using Online_bookstore.Products;
using Online_bookstore.Basket;

namespace Online_bookstore.PromoСode
{
    class FreeProductPromoСode : IDiscount
    {
        public IProducts freeProduct;

        public FreeProductPromoСode(IProducts freeProduct)
        {
            this.freeProduct = freeProduct;
        }
        public int GetDiscount(BasketGoods basket)
        {
            int discount = 0;

            foreach (var product in basket.GetProducts())
            {

                if (product == this.freeProduct)
                {
                    discount += product.GetPrice();
                }
            }

            return discount;
        }
    }
}
