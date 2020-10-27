using System;
using System.Collections.Generic;
using System.Text;
using Online_bookstore.Delivery;
using Online_bookstore.Products;
using Online_bookstore.PromoСode;

namespace Online_bookstore.Basket
{

    class BasketGoods : IDelivery
    {
        private List<IProducts> listProducts;
        private List<IDiscount> listPromoСode;

        public void AddProduct(IProducts product)
        {
            this.listProducts.Add(product);
        }

        public List<IProducts> GetProducts()
        {
            return this.listProducts;
        }

        public List<IProducts> GetProducts(string type)
        {

            List<IProducts> listSelectProducts = new List<IProducts>();

            foreach (var product in listProducts)
            {
                if (product.GetType() == type)
                {
                    listSelectProducts.Add(product);
                }
                
            }

            return listSelectProducts;
        }


        public void AddPromoСode(IDiscount promoСode)
        {
            this.listPromoСode.Add(promoСode);
        }

        public int GetPriceProducts()
        {
            int price = 0;

            foreach (var product in this.listProducts)
            {
                price += product.GetPrice();
            }

            return price;
        }

        public int GetPriceDelivery()
        {
            int priseDelivery = 200;
            int freePrise = 1000;

            int price = 0;

            foreach (var product in this.listProducts)
            {
                if (product.IsDelivery())
                {
                    price += product.GetPrice();
                }
            }

            if (price>= freePrise)
            {
                return 0;
            }

            return priseDelivery;
        }

        public int GetSumDiscountWithPromoСode()
        {
            int discount = 0;

            foreach (var promoСode in this.listPromoСode)
            {
                discount += promoСode.GetDiscount(this);
            }

            return discount;
        }


        public int GetSumDiscountWithStock()
        {
            int discount = 0;

            foreach (var promoСode in this.listPromoСode)
            {
                discount += promoСode.GetDiscount(this);
            }

            return discount;
        }


    }
}

