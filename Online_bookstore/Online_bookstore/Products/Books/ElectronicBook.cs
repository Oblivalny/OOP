using System;
using System.Collections.Generic;
using System.Text;

namespace Online_bookstore.Products
{
    class ElectronicBook : Book
    {

        public ElectronicBook(string name, string author, int price)
        {
            this.SetName(name);
            this.SetAuthor(author);
            this.SetPrice(price);
        }

        public new bool IsDelivery()
        {
            return false;
        }
    }
}
