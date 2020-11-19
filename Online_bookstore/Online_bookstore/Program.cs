using Online_bookstore.Basket;
using Online_bookstore.Discount.PromoCode;
using Online_bookstore.Discount.Stock;
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

            basket.AddPromoCode(new EBookAsGiftStock("author"));
            //basket.RemovePromoCode(new EBookAsGiftStock("author"));

            basket.AddPromoCode(new FreeBookPromoCode(book));

            basket.AddProduct(book);
            basket.AddProduct(book);

            basket.AddProduct(eBook);
            basket.RemoveProduct(eBook);

            basket.PrintTotal();
        }
    }
}
