using System.Collections.Generic;
using Online_bookstore.Delivery;
using Online_bookstore.Discount.PromoCode;
using Online_bookstore.Products;

namespace Online_bookstore.Basket
{
    public interface IBasket : IDelivery
    {
        int Subtotal { get; }
        int Discount { get; }
        int Total { get; }
        int NumberProducts { get; }
        IEnumerable<IProduct> GetProducts();
        void AddProduct(IProduct product);
        void RemoveProduct(IProduct product);
        void AddPromoCode(IPromoCode promoCode);
        void RemovePromoCode(IPromoCode promoCode);
        void PrintTotal();
    }
}