namespace GoodGameDeals.Core.Entities {
    using System;
    using System.Collections.Generic;

    public class Deal {
        public Deal(
                DateTime added,
                IList<string> drm,
                string gameTitle,
                Discount discount,
                Store store,
                string url) {
            this.DateAdded = added;
            this.Drm = drm;
            this.GameTitle = gameTitle;
            this.Discount = discount;
            this.Store = store;
            this.Url = url;
        }

        public DateTime DateAdded { get; }

        public IList<string> Drm { get; }

        public string GameTitle { get; }

        public Discount Discount { get; }

        public Store Store { get; }

        public string Url { get; }
    }
}
