using System;
using Online_bookstore.Products;

namespace Online_bookstore.Discount.PromoCode
{
    public class PercentageDiscountPromoCode : IPromoCode, IProductDiscount
    {
        public IProduct Product { get; }
        public int PercentageDiscount { get; }

        public PercentageDiscountPromoCode(IProduct product, int percentageDiscount)
        {
            if (percentageDiscount < 0 || percentageDiscount > 100)
            {
                throw new ArgumentException("The parameter percentageDiscount ranges from 0 to 100");
            }
            Product = product;
            PercentageDiscount = percentageDiscount;
        }

        public int GetDiscount(IProduct product)
        {
            var discount = product.Price * PercentageDiscount / 100;
            return product.Equals(Product) ? discount : 0;
        }

        public override bool Equals(object obj)
        {
            if (obj != null && obj is PercentageDiscountPromoCode other)
            {
                return Equals(Product, other.Product) &&
                       PercentageDiscount == other.PercentageDiscount;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Product, PercentageDiscount);
        }
    }
}
