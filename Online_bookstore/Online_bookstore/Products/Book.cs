using System;
using System.Collections.Generic;
using Online_bookstore.Discount;
using static System.String;

namespace Online_bookstore.Products
{
    public class Book : IProduct
    {
        private List<IDiscount> _discounts;
        public ProductTypes Type { get; }
        public string Name { get; }
        public string Author { get; }
        public int Price { get; }
        public int Discount { get; private set; }
        public bool IsDeliveryPossible { get; }

        public Book(ProductTypes type, string name, string author, int price, bool isDeliveryPossible)
        {
            if (price < 0)
            {
                throw new ArgumentException("The parameter price cannot be less than 0");
            }
            Type = type;
            Name = name;
            Author = author;
            Price = price;
            IsDeliveryPossible = isDeliveryPossible;
            Discount = 0;
            _discounts = new List<IDiscount>();
        }

        public IProduct Copy()
        {
            return new Book(Type, Name, Author, Price, IsDeliveryPossible);
        }

        public bool ApplyDiscount(IDiscount applyDiscount)
        {
            if (applyDiscount is IDeliveryDiscount)
            {
                return false;
            }
            if (!_discounts.Contains(applyDiscount))
            {
                var currentDiscount = applyDiscount.GetDiscount(this);
                if (currentDiscount > 0)
                {
                    _discounts.Add(applyDiscount);
                    Discount += currentDiscount;
                    if (Discount > Price)
                    {
                        Discount = Price;
                    }
                    return true;
                }
            }
            return false;
        }

        public bool CancelDiscount(IDiscount cancelDiscount)
        {
            if (_discounts.Contains(cancelDiscount))
            {
                _discounts.Remove(cancelDiscount);
                foreach (var discount in _discounts)
                {
                    Discount += discount.GetDiscount(this);
                }
                if (Discount > Price)
                {
                    Discount = Price;
                }
                return true;
            }
            return false;
        }

        public void ClearDiscount()
        {
            _discounts = new List<IDiscount>();
        }

        public override bool Equals(object obj)
        {
            if (obj != null && obj is Book book)
            {
                return Compare(Name, book.Name, StringComparison.Ordinal) == 0 &&
                       Compare(Author, book.Author, StringComparison.Ordinal) == 0 &&
                       Type == book.Type &&
                       IsDeliveryPossible == book.IsDeliveryPossible;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Type, Name, Author, IsDeliveryPossible);
        }
    }
}