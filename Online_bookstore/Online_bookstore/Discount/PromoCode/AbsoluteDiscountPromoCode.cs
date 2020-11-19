using System;
using System.Linq;
using Online_bookstore.Basket;
using Online_bookstore.Products;

namespace Online_bookstore.Discount.PromoCode
{
    public class AbsoluteDiscountPromoCode : IDiscount
    {
        public IProduct Product { get; }
        public int Discount { get; }

        public AbsoluteDiscountPromoCode(IProduct product, int discount)
        {
            Product = product;
            Discount = discount;
        }

        public int GetDiscount(IBasket basket)
        {
            return basket.GetProducts()
                         .Where(product => Product.Equals(product))
                         .Sum(product => Discount);
        }

        public override bool Equals(object obj)
        {
            if (obj != null && obj is AbsoluteDiscountPromoCode other)
            {
                return Equals(Product, other.Product) &&
                       Discount == other.Discount;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Product, Discount);
        }
    }
}
