namespace GoodGameDeals.Test.Entities{
    using System;
    using GoodGameDeals.Core.Entities;
    using Xunit;

    public class DiscountTests {
        [Fact]
        public void PercentageIntegerTest() {
            var discount = new Discount(100, 75);
            Assert.True(Math.Abs(discount.PriceDiscountPercentage - 0.25f) <
                        float.Epsilon);
        }

        [Fact]
        public void PercentageFloatTest() {
            var discount = new Discount(59.99f, 38.56f);
            Assert.True(Math.Abs(discount.PriceDiscountPercentage - 0.36f) <
                        float.Epsilon);
        }
    }
}
