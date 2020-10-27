using System;
using System.Collections.Generic;
using System.Text;
using Online_bookstore.Products;
using Online_bookstore.PromoСode;
using Online_bookstore.Basket;

namespace Online_bookstore.Stock
{

    class TwoBooksByOneAuthorStock : IDiscount
    {
        public int GetDiscount(BasketGoods basket)
        {
            int discount = 0;
            Dictionary<string, List<Book>> dictAuthor = new Dictionary<string, List<Book>>();

            foreach (var book in basket.GetProducts("Book"))
            {
               
                if (!dictAuthor.ContainsKey(book.GetAuthor()))
                {

                }
                
                

            }

            return discount;
        }
    }
}
