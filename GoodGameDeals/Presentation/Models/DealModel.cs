namespace GoodGameDeals.Models {
    using System;
    using System.Collections.Generic;

    public class DealModel : IEquatable<DealModel> {
        public DealModel(
                string url,
                long discount,
                double gamePriceOld,
                double gamePrice,
                string store) {
            this.Url = url;
            this.Discount = discount;
            this.GamePriceOld = gamePriceOld;
            this.GamePrice = gamePrice;
            this.Store = store;
        }

        public long Discount { get; }

        public double GamePrice { get; }

        public double GamePriceOld { get; }

        public string Store { get; }

        public string Url { get; }

        public static bool operator ==(DealModel deal1, DealModel deal2) =>
            EqualityComparer<DealModel>.Default.Equals(deal1, deal2);

        public static bool operator !=(DealModel deal1, DealModel deal2) =>
            !(deal1 == deal2);

        public override bool Equals(object obj) => this.Equals(obj as DealModel);

        public bool Equals(DealModel other) {
            return other != null
                && this.Discount == other.Discount
                && Math.Abs(this.GamePrice - other.GamePrice) < double.Epsilon
                && Math.Abs(this.GamePriceOld - other.GamePriceOld)
                    < double.Epsilon
                && this.Store == other.Store
                && this.Url == other.Url;
        }

        public override int GetHashCode() {
            var hashCode = 628291855;
            hashCode = (hashCode * -1521134295) + this.Discount.GetHashCode();
            hashCode = (hashCode * -1521134295) + this.GamePrice.GetHashCode();
            hashCode = (hashCode * -1521134295) + this.GamePriceOld.GetHashCode();
            hashCode = (hashCode * -1521134295)
                       + EqualityComparer<string>.Default.GetHashCode(
                           this.Store);
            hashCode = (hashCode * -1521134295)
                       + EqualityComparer<string>.Default.GetHashCode(this.Url);
            return hashCode;
        }
    }
}