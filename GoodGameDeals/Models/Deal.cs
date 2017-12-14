namespace GoodGameDeals.Models {
    public class Deal {
        public Deal(
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
    }
}