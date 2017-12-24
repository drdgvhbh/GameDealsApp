namespace GoodGameDeals.Core.Entities {
    using System;

    public class Discount {
        public Discount(
            float priceNew, float priceOld) {
            this.PriceNew = priceNew;
            this.PriceOld = priceOld;
        }

        public float PriceDiscountPercentage =>
            (float)(Math.Round(1 - this.PriceOld / this.PriceNew, 2));

        public float PriceNew { get; }

        public float PriceOld { get; }
    }
}
