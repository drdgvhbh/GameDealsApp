namespace GoodGameDeals.Core.Entities {
    using System;
    using System.Text;

    using GoodGameDeals.Core.Contracts;

    public class Discount {
        public Discount(
                float priceNew, float priceOld) {
            if (priceNew < 0) {
                throw new ArgumentOutOfRangeException(
                    nameof(priceNew),
                    nameof(priceNew) + "was out of range. Must be non-negative.");
            }
            if (priceOld < 0) {
                throw new ArgumentOutOfRangeException(
                    nameof(priceOld),
                    nameof(priceOld) + "was out of range. Must be non-negative.");
            }

            if (priceNew >= priceOld) {
                var message = new StringBuilder();
                message.AppendFormat(
                    "{0} was out of range. Must be less than {1}.",
                    nameof(priceNew),
                    nameof(priceOld));
                throw new ArgumentOutOfRangeException(
                    nameof(priceNew),
                    message.ToString());
            }

            this.PriceNew = priceNew;
            this.PriceOld = priceOld;
        }

        public float PriceDiscountPercentage =>
            (float)Math.Round(1 - this.PriceNew / this.PriceOld, 2);

        public float PriceNew { get; }

        public float PriceOld { get; }
    }
}
