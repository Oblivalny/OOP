using System;
using System.Linq;
using Online_bookstore.Basket;
using Online_bookstore.Products;

namespace Online_bookstore.Discount.PromoCode
{
    public class FreeBookPromoCode : IDiscount
    {
        public IProduct Product { get; }

        public FreeBookPromoCode(IProduct product)
        {
            Product = product;
        }

        public int GetDiscount(IBasket basket)
        {
            return basket.GetProducts()
                         .Where(product => Product.Equals(product))
                         .Sum(product => product.Price);
        }

        public override bool Equals(object obj)
        {
            if (obj != null && obj is FreeBookPromoCode other)
            {
                return Equals(Product, other.Product);
            }
            return false;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Product);
        }
    }
}
