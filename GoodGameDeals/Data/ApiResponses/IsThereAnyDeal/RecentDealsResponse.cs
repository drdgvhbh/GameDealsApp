namespace GoodGameDeals.Data.ApiResponses.IsThereAnyDeal {
    using Newtonsoft.Json;



    public class RecentDealsResponse {
        public RecentDealsResponse() {
            this.Data = new DataC();
            this.Meta = new MetaData();
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
            public DataC() {
                this.DealsList = new Deal[0];
                this.ITADWebsite = new ITADWebsite();
            }

            [JsonProperty("count")]
            public long MaxNumDeals { get; set; }

            [JsonProperty("list")]
            public Deal[] DealsList { get; set; }

            [JsonProperty("urls")]
            internal ITADWebsite ITADWebsite { get; set; }

        }

        internal class ITADWebsite {
            public ITADWebsite() {
            }

            [JsonProperty("deals")]
            public string Site { get; set; }
        }

        public class Deal {
            public Deal() {
                this.Drm = new string[0];
                this.Shop = new Shop();
                this.Urls = new DealUrl();
            }

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