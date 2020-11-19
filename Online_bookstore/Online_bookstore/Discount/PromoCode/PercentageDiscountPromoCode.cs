using System;
using System.Linq;
using Online_bookstore.Basket;
using Online_bookstore.Products;

namespace Online_bookstore.Discount.PromoCode
{
    public class PercentageDiscountPromoCode : IDiscount
    {
        public IProduct Product { get; }
        public int PercentageDiscount { get; }

        public PercentageDiscountPromoCode(IProduct product, int percentageDiscount)
        {
            if (percentageDiscount < 0 || percentageDiscount > 100)
            {
                throw new ArgumentException("The percentageDiscount parameter ranges from 0 to 100");
            }
            Product = product;
            PercentageDiscount = percentageDiscount;
        }

        public int GetDiscount(IBasket basket)
        {
            return basket.GetProducts()
                         .Where(product => product.Equals(Product))
                         .Sum(product => product.Price * PercentageDiscount / 100);
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
