using System;
using System.Collections.Generic;
using System.Text;

namespace Online_bookstore.Products
{

    abstract class Book : IProducts
    {
        private string name;
        private string author;
        private string type = "Book";
        private int price;

        public string GetAuthor()
        {
            return this.author;
        }

        public string GetName()
        {
            return this.name;
        }

        public int GetPrice()
        {
            return this.price;
        }

        public void SetName(string name)
        {
            this.name = name;
        }

        public void SetPrice(int price)
        {
            this.price = price;
        }

        public void SetAuthor(string author)
        {
            this.author = author;
        }

        public bool IsDelivery()
        {
            return true;
        }

        public new string GetType()
        {
            return this.type;
        }
    }
}
