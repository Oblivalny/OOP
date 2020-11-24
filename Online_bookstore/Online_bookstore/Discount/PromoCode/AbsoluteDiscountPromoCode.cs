using System;
using Online_bookstore.Products;

namespace Online_bookstore.Discount.PromoCode
{
    public class AbsoluteDiscountPromoCode : IPromoCode, IProductDiscount
    {
        public IProduct Product { get; }
        public int Discount { get; }

        public AbsoluteDiscountPromoCode(IProduct product, int discount)
        {
            if (Discount > product.Price || Discount < 0)
            {
                throw new ArgumentException("The parameter discount ranges from 0 to \"price\"");
            }
            Product = product;
            Discount = discount;
        }

        public int GetDiscount(IProduct product)
        {
            return product.Equals(Product) ? Discount : 0;
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