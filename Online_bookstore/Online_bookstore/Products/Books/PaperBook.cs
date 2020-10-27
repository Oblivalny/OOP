using System;
using System.Collections.Generic;
using System.Text;

namespace Online_bookstore.Products
{
    class PaperBook : Book
    {
        private string publishingHouse;

        public PaperBook(string name, string author, int price, string publishingHouse)
        {
            this.SetName(name);
            this.SetAuthor(author);
            this.SetPrice(price);
            this.SetPublishingHouse(publishingHouse);
        }

        public string GetPublishingHouse()
        {
            return this.publishingHouse;
        }

        public void SetPublishingHouse(string publishingHouse)
        {
            this.publishingHouse = publishingHouse;
        }

    }
}
