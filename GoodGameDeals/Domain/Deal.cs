namespace GoodGameDeals.Domain {
    public class Deal {
        public long Added { get; set; }

        public string[] Drm { get; set; }

        public string Plain { get; set; }

        public long PriceCut { get; set; }

        public double PriceNew { get; set; }

        public double PriceOld { get; set; }

        public Shop Shop { get; set; }

        public string Title { get; set; }

        public DealUrl DealUrl { get; set; }
    }
}
