using System;
using System.Linq;
using Online_bookstore.Basket;
using Online_bookstore.Products;
using static System.String;

namespace Online_bookstore.Discount.Stock
{
    public class EBookAsGiftStock : IDiscount
    {
        public string Author { get; }

        public EBookAsGiftStock(string author)
        {
            Author = author;
        }

        public int GetDiscount(IBasket basket)
        {
            var count = basket.GetProducts()
                              .Count(product => product.Author.Equals(Author) &&
                                                product.Type == ProductTypes.PaperBook);
            return basket.GetProducts()
                         .Where(product => product.Author.Equals(Author) &&
                                           product.Type == ProductTypes.EBook)
                         .Take(count / 2)
                         .Sum(product => product.Price);
        }

        public override bool Equals(object obj)
        {
            if (obj != null && obj is EBookAsGiftStock other)
            {
                return Compare(Author, other.Author, StringComparison.Ordinal) == 0;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Author);
        }
    }
}
