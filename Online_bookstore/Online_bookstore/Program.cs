using Online_bookstore.Basket;
using Online_bookstore.Discount.PromoCode;
using Online_bookstore.Products;

namespace Online_bookstore
{
    static class Program
    {
        private static void Main()
        {
            //Example
            var basket = new BasketGoods();
            var book = new Book(ProductTypes.PaperBook, "A", "author", 400, true);
            var eBook = new Book(ProductTypes.EBook, "A", "author", 150, false);

            basket.AddProduct(eBook);
            basket.AddProduct(book);
            basket.AddProduct(book);

            basket.AddPromoCode(new AbsoluteDiscountPromoCode(book, 400));
            basket.AddPromoCode(new AbsoluteDiscountPromoCode(eBook, 200));
            basket.AddPromoCode(new PercentageDiscountPromoCode(eBook, 50));
            basket.AddPromoCode(new FreeBookPromoCode(eBook));
            basket.AddPromoCode(new FreeDeliveryPromoCode());

            basket.PrintTotal();
        }
    }
}
