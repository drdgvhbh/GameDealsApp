namespace GoodGameDeals.Core.Entities {
    using System;
    using System.Collections.Generic;

    public class Deal {
        public DateTime DateAdded { get; }

        public IList<string> Drm { get; }

        public string GameTitle { get; }

        public Discount Discount { get; }

        public Store Store { get; }

        public string Url { get; }
    }
}
