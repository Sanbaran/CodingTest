using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using ManageCart;

namespace TestPromotionEngine
{
    [TestClass]
    public class UnitTest1
    {
        public const string ScenarioA = "A";
        public const string ScenarioB = "B";
        public const string ScenarioC = "C";

        public const string SKUIdA = "A";
        public const string SKUIdB = "B";
        public const string SKUIdC = "C";
        public const string SKUIdD = "D";

        [TestMethod]
        public void ScenarioATest()
        {
            //define test input and output values
            decimal expectedResult = 100;
            IEnumerable<CartSKU> purchasedSKUs = GetPurchasedSKUs(ScenarioA);
            IEnumerable<Promotion> activePromotions = GetActivePromotions();

            PromotionEngine systemUnderTest = new PromotionEngine();
            decimal actualResult = systemUnderTest.CalculateTotal(purchasedSKUs, activePromotions);

            //verify the result
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void ScenarioBTest()
        {
            //define test input and output values
            decimal expectedResult = 370;
            IEnumerable<CartSKU> purchasedSKUs = GetPurchasedSKUs(ScenarioB);
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

        private IEnumerable<CartSKU> GetPurchasedSKUs(string scenario)
        {
            switch (scenario)
            {
                case ScenarioA:
                    return new List<CartSKU>()
                    {
                        new CartSKU()
                        {
                            Id = SKUIdA, UnitPrice = 50, UnitsPurchased = 1
                        },
                        new CartSKU()
                        {
                            Id = SKUIdB, UnitPrice = 30, UnitsPurchased = 1
                        },
                        new CartSKU()
                        {
                            Id = SKUIdC, UnitPrice = 20, UnitsPurchased = 1
                        }
                    };
                case ScenarioB:
                    return new List<CartSKU>()
                    {
                        new CartSKU()
                        {
                            Id = SKUIdA, UnitPrice = 50, UnitsPurchased = 5
                        },
                        new CartSKU()
                        {
                            Id = SKUIdB, UnitPrice = 30, UnitsPurchased = 5
                        },
                        new CartSKU()
                        {
                            Id = SKUIdC, UnitPrice = 20, UnitsPurchased = 1
                        }
                    };
                case ScenarioC:
                    return new List<CartSKU>()
                    {
                        new CartSKU()
                        {
                            Id = SKUIdA, UnitPrice = 50, UnitsPurchased = 3
                        },
                        new CartSKU()
                        {
                            Id = SKUIdB, UnitPrice = 30, UnitsPurchased = 5
                        },
                        new CartSKU()
                        {
                            Id = SKUIdC, UnitPrice = 20, UnitsPurchased = 1
                        },
                        new CartSKU()
                        {
                            Id = SKUIdD, UnitPrice = 15, UnitsPurchased = 1
                        }
                    };
                default:
                    return new List<CartSKU>();
            }
        }
    }
}
