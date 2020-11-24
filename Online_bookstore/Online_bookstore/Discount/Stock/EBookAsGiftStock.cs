using System;
using System.Collections.Generic;
using System.Linq;
using Online_bookstore.Basket;
using Online_bookstore.Products;

namespace Online_bookstore.Discount.Stock
{
    public class EBookAsGiftStock : IStock, IBasketDiscount
    {
        public IEnumerable<IProduct> GetDiscountProducts(IBasket basket)
        {
            foreach (var (author, numberOfPaperBooks) in GetNumberOfPaperBooks(basket))
            {
                var booksAsGift = numberOfPaperBooks / 2;
                if (booksAsGift > 0)
                {
                    foreach (var eBook in TakeEBooksThisAuthor(basket, author, booksAsGift))
                    {
                        yield return eBook;
                    }
                }
            }
        }

        public int GetDiscount(IProduct product)
        {
            return product.Price;
        }

        private IEnumerable<IProduct> TakeEBooksThisAuthor(IBasket basket, string author, int count)
        {
            return basket.GetProducts()
                         .Where(product => IsEBookThisAuthor(product, author))
                         .OrderBy(product => product.Price)
                         .Take(count);
        }

        private Dictionary<string, int> GetNumberOfPaperBooks(IBasket basket)
        {
            var paperBooks = new Dictionary<string, int>();
            foreach (var product in basket.GetProducts())
            {
                if (product.Type == ProductTypes.PaperBook)
                {
                    if (!paperBooks.ContainsKey(product.Author))
                    {
                        paperBooks[product.Author] = 0;
                    }
                    paperBooks[product.Author]++;
                }
            }
            return paperBooks;
        }

        private bool IsEBookThisAuthor(IProduct product, string author)
        {
            return string.Compare(product.Author, author, StringComparison.Ordinal) == 0 &&
                   product.Type == ProductTypes.EBook;
        }
    }
}
