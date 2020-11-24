using System.Collections.Generic;
using Online_bookstore.Basket;
using Online_bookstore.Products;

namespace Online_bookstore.Discount
{
    public interface IBasketDiscount : IDiscount
    {
        IEnumerable<IProduct> GetDiscountProducts(IBasket basket);
    }
}
