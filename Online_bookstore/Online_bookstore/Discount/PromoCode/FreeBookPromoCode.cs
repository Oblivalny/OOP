using System;
using Online_bookstore.Products;

namespace Online_bookstore.Discount.PromoCode
{
    public class FreeBookPromoCode : IPromoCode, IProductDiscount
    {
        public IProduct Product { get; }

        public FreeBookPromoCode(IProduct product)
        {
            Product = product;
        }

        public int GetDiscount(IProduct product)
        {
            return product.Equals(Product) ? product.Price : 0;
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
