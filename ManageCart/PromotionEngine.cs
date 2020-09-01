using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace ManageCart
{
    public class PromotionEngine
    {
        public decimal CalculateTotal(IEnumerable<CartSKU> productsPurchased, IEnumerable<Promotion> activePromotions)
        {
            if (productsPurchased.Count() == 0)
            {
                return 0;
            }

            if(activePromotions.Count() == 0)
            {
                return productsPurchased.Select(x => x.UnitPrice * x.UnitsPurchased).Sum();
            }

            decimal total = 0;
            // apply the promotions
            foreach (Promotion promotion in activePromotions)
            {
                var elligibleSKUs = productsPurchased.Intersect(promotion.SKUs, new SKUComparer());
                if (elligibleSKUs.Count() == promotion.SKUs.Count())
                {
                    // check if the promotion has a fixed price
                    if(promotion.FixedPrice.HasValue)
                    {
                        total += promotion.FixedPrice.Value;
                    }
                    else
                    {
                        // else calculate based on the % discount on unit price
                    }

                    //remove the SKUs on which promotion has been applied
                    foreach(var sku in promotion.SKUs)
                    {
                        productsPurchased.Where(x => string.Equals(x.Id, sku.Id)).FirstOrDefault().UnitsPurchased -= sku.UnitsPurchased;
                    }
                }
            }

            // calulate the SKUs left out after applying promotions
            foreach(var sku in productsPurchased)
            {
                total += sku.UnitPrice * sku.UnitsPurchased;
            }

            return total;
        }
    }

    public class SKU
    {
        public string Id { get; set; }
        public decimal UnitPrice { get; set; }

    }

    public class CartSKU : SKU
    {
        public int UnitsPurchased { get; set; }
    }

    public class PromotionSKU : CartSKU
    {
        public decimal? DiscountOnUnitPrice { get; set; }
    }

    public class Promotion
    {
        public Promotion()
        {
            this.SKUs = new List<PromotionSKU>();
        }

        public IEnumerable<PromotionSKU> SKUs { get; set; }
        public decimal? FixedPrice { get; set; }
    }

    public class SKUComparer : IEqualityComparer<CartSKU>
    {
        public int GetHashCode(CartSKU a)
        {
            if (a == null)
            {
                return 0;
            }

            return a.Id.GetHashCode();
        }

        public bool Equals(CartSKU a, CartSKU b)
        {
            if (string.Equals(a.Id, b.Id) && a.UnitsPurchased <= b.UnitsPurchased)
            {
                return true;
            }

            return false;
        }
    }
}
