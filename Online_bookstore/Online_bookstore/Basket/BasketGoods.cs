using System;
using System.Collections.Generic;
using System.Linq;
using Online_bookstore.Discount;
using Online_bookstore.Products;

namespace Online_bookstore.Basket
{
    public class BasketGoods : IBasket
    {
        private readonly List<IProduct> _products;
        private readonly List<IDiscount> _promoCodes;
        public int Subtotal { get; private set; }
        public int Discount { get; private set; }
        public int PriceDelivery { get; private set; }
        public int Total => Subtotal - Discount + PriceDelivery;
        public int NumberProducts => _products.Count;

        public BasketGoods()
        {
            _products = new List<IProduct>();
            _promoCodes = new List<IDiscount>();
            Subtotal = 0;
            Discount = 0;
            PriceDelivery = 0;
        }

        public IEnumerable<IProduct> GetProducts()
        {
            return _products.ToList();
        }

        public void AddProduct(IProduct product)
        {
            _products.Add(product);
            Subtotal += product.Price;
            DiscountUpdate();
            if (product.IsDeliveryPossible)
            {
                PriceDeliveryUpdate();
            }
        }

        public void RemoveProduct(IProduct product)
        {
            if (!_products.Contains(product))
            {
                throw new ArgumentException("This product not found!");
            }
            _products.Remove(product);
            Subtotal -= product.Price;
            DiscountUpdate();
            if (product.IsDeliveryPossible)
            {
                PriceDeliveryUpdate();
            }
        }

        public void AddPromoCode(IDiscount discount)
        {
            if (!_promoCodes.Contains(discount))
            {
                _promoCodes.Add(discount);
            }
            if (_products.Count > 0)
            {
                DiscountUpdate();
                PriceDeliveryUpdate();
            }
        }

        public void RemovePromoCode(IDiscount discount)
        {
            if (!_promoCodes.Contains(discount))
            {
                throw new ArgumentException("This promo code not found!");
            }
            _promoCodes.Remove(discount);
            if (_products.Count > 0)
            {
                DiscountUpdate();
                PriceDeliveryUpdate();
            }
        }

        public void PrintTotal()
        {
            Console.WriteLine($"Subtotal: {Subtotal}");
            Console.WriteLine($"PriceDelivery: {PriceDelivery}");
            Console.WriteLine($"Discount: {Discount}");
            Console.WriteLine($"Total: {Total}");
        }

        private void DiscountUpdate()
        {
            Discount = _promoCodes.Sum(promoCode => promoCode.GetDiscount(this));
        }

        private void PriceDeliveryUpdate()
        {
            if (_products.Count(product => !product.IsDeliveryPossible) == _products.Count)
            {
                PriceDelivery = 0;
            }
            PriceDelivery = Subtotal - Discount >= 1000 ? 0 : 200;
        }
    }
}
