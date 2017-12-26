namespace GoodGameDeals.Data.ApiResponses.IsThereAnyDeal {
    using Newtonsoft.Json;

    using NullGuard;

    public class RecentDealsResponse {
        public RecentDealsResponse([AllowNull]object obj) {
            this.Data = new DataC();
            this.Meta = new MetaData();
            this.Data.MaxNumDeals = 0;
            this.Data.DealsList = new Deal[0];
            this.Data.ITADWebsite = new ITADWebsite();
        }

        /// <summary>
        ///     Prevents a default instance of the
        ///     <see cref="RecentDealsResponse" /> class from being created.
        /// </summary>
        /// <remarks>
        ///     This constructor is used for deserialization.
        /// </remarks>
        [JsonConstructor]
        private RecentDealsResponse() {
        }

        [JsonProperty("data")]
        public DataC Data { get; set; }

        [JsonProperty(".meta")]
        public MetaData Meta { get; set; }


        public class MetaData  {
            [JsonProperty("currency")]
            public string Currency { get; set; }
        }

        public class DataC {
            [JsonProperty("count")]
            public long MaxNumDeals { get; set; }

            [JsonProperty("list")]
            public Deal[] DealsList { get; set; }

            [JsonProperty("urls")]
            internal ITADWebsite ITADWebsite { get; set; }

        }

        internal class ITADWebsite {
            [JsonProperty("deals")]
            public string Site { get; set; }
        }

        public class Deal {
            [JsonProperty("added")]
            public long Added { get; set; }

            [JsonProperty("drm")]
            public string[] Drm { get; set; }

            [JsonProperty("plain")]
            public string Plain { get; set; }

            [JsonProperty("price_cut")]
            public long PriceCut { get; set; }

            [JsonProperty("price_new")]
            public double PriceNew { get; set; }

            [JsonProperty("price_old")]
            public double PriceOld { get; set; }

            [JsonProperty("shop")]
            public Shop Shop { get; set; }

            [JsonProperty("title")]
            public string Title { get; set; }

            [JsonProperty("urls")]
            public DealUrl Urls { get; set; }
        }

        public class DealUrl {
            [JsonProperty("buy")]
            public string Buy { get; set; }

            [JsonProperty("game")]
            public string Game { get; set; }
        }

        public class Shop {
            [JsonProperty("id")]
            public string Id { get; set; }

            [JsonProperty("name")]
            public string Name { get; set; }
        }
    }
}