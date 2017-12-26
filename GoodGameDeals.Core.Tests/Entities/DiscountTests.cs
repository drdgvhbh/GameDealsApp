namespace GoodGameDeals.Core.Tests.Entities {
    using System;

    using GoodGameDeals.Core.Entities;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class DiscountTests {
        private readonly Random random;

        public DiscountTests() {
            this.random = new Random();
        }

        [DataTestMethod]
        [DataRow(-1)]
        [DataRow(-17106792)]
        [DataRow(-910881)]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void
            Discount_PriceNewIsNegative_ThrowsArgumentOutOfRangeException(
                float priceNew) {
            new Discount(priceNew, this.random.Next());
        }

        [DataTestMethod]
        [DataRow(100, 75)]
        [DataRow(0, 0)]
        [DataRow(4000.563f + float.Epsilon, 4000.563f)]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void
            Discount_PriceNewLessThanPriceOld_ThrowsArgumentOutOfRangeException(
                float priceNew,
                float priceOld) {
            new Discount(priceNew, priceOld);
        }

        [DataTestMethod]
        [DataRow(-1)]
        [DataRow(-30760.909f)]
        [DataRow(-710.76743f)]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void
            Discount_PriceOldIsNegative_ThrowsArgumentOutOfRangeException(
                float priceOld) {
            new Discount(this.random.Next(), priceOld);
        }

        [TestMethod]
        public void
            PriceDiscountPercentage_PricesArePositiveNumbers_DiscountIsMathematicallyCorrect() {
            var discount = new Discount(75, 100).PriceDiscountPercentage;
            Assert.IsTrue(Math.Abs(discount - 0.25f) < float.Epsilon);
            discount = new Discount(44976016, 87183293).PriceDiscountPercentage;
            Assert.IsTrue(Math.Abs(discount - 0.48f) < float.Epsilon);
            discount = new Discount(38.56f, 59.99f).PriceDiscountPercentage;
            Assert.IsTrue(Math.Abs(discount - 0.36f) < float.Epsilon);
        }

        [DataTestMethod]
        [DataRow(454.646f, 2756251.3484f)]
        [DataRow(7954, 261331.6284f)]
        [DataRow(75, 100)]
        public void
            PriceDiscountPercentage_PricesArePositiveNumbers_DiscountIsPositive(
                float priceNew,
                float priceOld) {
            var discount = new Discount(priceNew, priceOld)
                .PriceDiscountPercentage;
            Assert.IsTrue(discount > 0);
        }

        [TestMethod]
        public void
            PriceDiscountPercentage_PricesArePositiveNumbers_DiscountIsRoundedToTwoDecimalPlaces() {
            var discount = new Discount(6, 11).PriceDiscountPercentage;
            Assert.IsTrue(Math.Abs(discount - 0.45f) < float.Epsilon);
            discount = new Discount(86549.65231f, 12345678.9101112f)
                .PriceDiscountPercentage;
            Assert.IsTrue(Math.Abs(discount - 0.99f) < float.Epsilon);
        }
    }
}