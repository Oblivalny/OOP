using System;
using static System.String;

namespace Online_bookstore.Products
{
    public class Book : IProduct
    {
        public ProductTypes Type { get; }
        public string Name { get; }
        public string Author { get; }
        public int Price { get; }
        public bool IsDeliveryPossible { get; }

        public Book(ProductTypes type, string name, string author, int price, bool isDeliveryPossible)
        {
            Type = type;
            Name = name;
            Author = author;
            Price = price;
            IsDeliveryPossible = isDeliveryPossible;
        }

        public override bool Equals(object obj)
        {
            if (obj != null && obj is Book book)
            {
                return Compare(Name, book.Name, StringComparison.Ordinal) == 0 &&
                       Compare(Author, book.Author, StringComparison.Ordinal) == 0 &&
                       Type == book.Type &&
                       Price == book.Price &&
                       IsDeliveryPossible == book.IsDeliveryPossible;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Type, Name, Author, Price, IsDeliveryPossible);
        }
    }
}
