namespace GoodGameDeals.Core.Tests.Entities {
    using System;

    using GoodGameDeals.Core.Entities;

    using Xunit;

    public class DiscountTests {
        private readonly Random random;

        public DiscountTests() {
            this.random = new Random();
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(-17106792)]
        [InlineData(-910881)]
        public void
            Discount_PriceNewIsNegative_ThrowsArgumentOutOfRangeException(
                float priceNew) {
            Assert.Throws<ArgumentOutOfRangeException>(
                () => new Discount(priceNew, this.random.Next()));
        }

        [Theory]
        [InlineData(100, 75)]
        [InlineData(0, 0)]
        [InlineData(4000.563 + float.Epsilon, 4000.563)]
        public void
            Discount_PriceNewLessThanPriceOld_ThrowsArgumentOutOfRangeException(
                float priceNew,
                float priceOld) {
            Assert.Throws<ArgumentOutOfRangeException>(
                () => new Discount(priceNew, priceOld));
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(-30760.909)]
        [InlineData(-710.76743)]
        public void
            Discount_PriceOldIsNegative_ThrowsArgumentOutOfRangeException(
                float priceOld) {
            Assert.Throws<ArgumentOutOfRangeException>(
                () => new Discount(this.random.Next(), priceOld));
        }

        [Fact]
        public void
            PriceDiscountPercentage_PricesArePositiveNumbers_DiscountIsMathematicallyCorrect() {
            var discount = new Discount(75, 100).PriceDiscountPercentage;
            Assert.True(Math.Abs(discount - 0.25f) < float.Epsilon);
            discount = new Discount(44976016, 87183293).PriceDiscountPercentage;
            Assert.True(Math.Abs(discount - 0.48f) < float.Epsilon);
            discount = new Discount(38.56f, 59.99f).PriceDiscountPercentage;
            Assert.True(Math.Abs(discount - 0.36f) < float.Epsilon);
        }

        [Theory]
        [InlineData(75, 100)]
        [InlineData(454.646, 2756251.3484)]
        [InlineData(7954, 261331.6284)]
        public void
            PriceDiscountPercentage_PricesArePositiveNumbers_DiscountIsPositive(
                float priceNew,
                float priceOld) {
            var discount = new Discount(priceNew, priceOld)
                .PriceDiscountPercentage;
            Assert.True(discount > 0);
        }

        [Fact]
        public void
            PriceDiscountPercentage_PricesArePositiveNumbers_DiscountIsRoundedToTwoDecimalPlaces() {
            var discount = new Discount(6, 11).PriceDiscountPercentage;
            Assert.True(Math.Abs(discount - 0.45f) < float.Epsilon);
            discount = new Discount(86549.65231f, 12345678.9101112f)
                .PriceDiscountPercentage;
            Assert.True(Math.Abs(discount - 0.99f) < float.Epsilon);
        }
    }
}