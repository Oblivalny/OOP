using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Online_bookstore.Discount;
using Online_bookstore.Discount.PromoCode;
using Online_bookstore.Discount.Stock;
using Online_bookstore.Products;

namespace Online_bookstore.Basket
{
    public class BasketGoods : IBasket
    {
        private readonly List<IProduct> _products;
        private readonly List<IDiscount> _discounts;
        private int _basketDiscount;
        private int _deliveryDiscount;
        private int _productsDiscount;
        public int Subtotal { get; private set; }
        public int PriceDelivery { get; private set; }
        public int Discount => _basketDiscount + _deliveryDiscount + _productsDiscount;
        public int Total => Subtotal - Discount + PriceDelivery;
        public int NumberProducts => _products.Count;

        public BasketGoods()
        {
            _products = new List<IProduct>();
            _discounts = GetStock();
            Subtotal = 0;
            PriceDelivery = 0;
            _basketDiscount = 0;
            _deliveryDiscount = 0;
            _productsDiscount = 0;
        }

        private List<IDiscount> GetStock()
        {
            var discounts = new List<IDiscount>();
            var implementations = Assembly.GetExecutingAssembly().ExportedTypes
                                          .Where(IsImplementedIStock);
            foreach (var type in implementations)
            {
                var ctor = type.GetConstructor(new Type[] { });
                if (ctor.Invoke(new object[] { }) is IStock stock)
                {
                    discounts.Add(stock);
                }
            }

            return discounts;
        }

        private static bool IsImplementedIStock(Type type)
        {
            return typeof(IStock).IsAssignableFrom(type) &&
                   !type.IsInterface &&
                   !type.IsAbstract;
        }

        public IEnumerable<IProduct> GetProducts()
        {
            return _products.ToList();
        }

        public void AddProduct(IProduct product)
        {
            _products.Add(product.Copy());
            Subtotal += product.Price;
            UpdateDiscount();
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
            UpdateDiscount();
            if (product.IsDeliveryPossible)
            {
                PriceDeliveryUpdate();
            }
        }

        public void AddPromoCode(IPromoCode promoCode)
        {
            if (!_discounts.Contains(promoCode))
            {
                _discounts.Add(promoCode);
            }
            if (_products.Count > 0)
            {
                UpdateDiscount();
                PriceDeliveryUpdate();
            }
        }

        public void RemovePromoCode(IPromoCode promoCode)
        {
            if (!_discounts.Contains(promoCode))
            {
                throw new ArgumentException("This promo code not found!");
            }
            _discounts.Remove(promoCode);
            if (_products.Count > 0)
            {
                UpdateDiscount();
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

        private void UpdateDiscount()
        {
            _basketDiscount = 0;
            _deliveryDiscount = 0;
            foreach (var discount in _discounts)
            {
                switch (discount)
                {
                    case IBasketDiscount basketDiscount:
                        ApplyBasketDiscount(basketDiscount);
                        break;
                    case IDeliveryDiscount deliveryDiscount:
                        ApplyDeliveryDiscount(deliveryDiscount);
                        break;
                    case IProductDiscount productDiscount:
                        ApplyProductsDiscount(productDiscount);
                        break;
                }
            }
        }

        private void ApplyBasketDiscount(IBasketDiscount basketDiscount)
        {
            foreach (var discountProduct in basketDiscount.GetDiscountProducts(this))
            {
                foreach (var product in _products.Where(prod => ReferenceEquals(prod, discountProduct)))
                {
                    _basketDiscount += basketDiscount.GetDiscount(product);
                    product.ApplyDiscount(basketDiscount);
                }
            }
        }

        private void ApplyDeliveryDiscount(IDeliveryDiscount deliveryDiscount)
        {
            _deliveryDiscount += deliveryDiscount.GetTotalDiscount(this);
            if (_deliveryDiscount > PriceDelivery)
            {
                _deliveryDiscount = PriceDelivery;
            }
        }

        private void ApplyProductsDiscount(IProductDiscount productDiscount)
        {
            foreach (var product in _products)
            {
                var discount = product.Discount;
                product.ApplyDiscount(productDiscount);
                _productsDiscount += product.Discount - discount;
            }
        }

        private void PriceDeliveryUpdate()
        {
            if (_products.All(product => !product.IsDeliveryPossible))
            {
                PriceDelivery = 0;
            }
            PriceDelivery = Subtotal - Discount >= 1000 ? 0 : 200;
        }
    }
}