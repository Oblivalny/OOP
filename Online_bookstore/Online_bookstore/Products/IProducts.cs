using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Reflection.Metadata;
using System.Text;

namespace Online_bookstore.Products
{
    interface IProducts

    {
        public string GetType();
        public void SetName(string name);
        public string GetName();
        public void SetPrice(int prise);
        public int GetPrice();
        public bool IsDelivery();
    }
}
