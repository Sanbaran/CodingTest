using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using ManageCart;

namespace TestPromotionEngine
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void ScenarioATest()
        {
            //define test input and output values
            decimal expectedResult = 100;
            IEnumerable<CartSKU> purchasedSKUs = GetPurchasedSKUs();
            IEnumerable<Promotion> activePromotions = GetActivePromotions();
                        
            PromotionEngine systemUnderTest = new PromotionEngine();
            decimal actualResult = systemUnderTest.CalculateTotal(purchasedSKUs, activePromotions);

            //verify the result
            Assert.AreEqual(expectedResult, actualResult);
        }

        private static IEnumerable<Promotion> GetActivePromotions()
        {
            return new List<Promotion>()
            {
                new Promotion()
                {
                    SKUs = new List<PromotionSKU>()
                    {
                        new PromotionSKU()
                        {
                            Id = "A", UnitPrice = 50, UnitsPurchased = 3
                        }
                    },
                    FixedPrice = 130
                },
                new Promotion()
                {
                    SKUs = new List<PromotionSKU>()
                    {
                        new PromotionSKU()
                        {
                            Id = "B", UnitPrice = 30, UnitsPurchased = 2
                        }
                    },
                    FixedPrice = 45
                },
                new Promotion()
                {
                    SKUs = new List<PromotionSKU>()
                    {
                        new PromotionSKU()
                        {
                            Id = "C", UnitPrice = 20, UnitsPurchased = 1
                        },
                        new PromotionSKU()
                        {
                            Id = "D", UnitPrice = 15, UnitsPurchased = 1
                        }
                    },
                    FixedPrice = 30
                },
            };
        }

        private IEnumerable<CartSKU> GetPurchasedSKUs()
        {
            return new List<CartSKU>()
            {
                new CartSKU()
                {
                    Id = "A", UnitPrice = 50, UnitsPurchased = 1
                },
                new CartSKU()
                {
                    Id = "B", UnitPrice = 30, UnitsPurchased = 1
                },
                new CartSKU()
                {
                    Id = "C", UnitPrice = 20, UnitsPurchased = 1
                }
            };
        }
    }
}
