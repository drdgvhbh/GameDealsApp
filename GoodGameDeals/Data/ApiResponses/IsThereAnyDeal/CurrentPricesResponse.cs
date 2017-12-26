namespace GoodGameDeals.Data.ApiResponses.IsThereAnyDeal {
    using GoodGameDeals.Data.ApiResponses.IsThereAnyDeal.Converters;

    using Newtonsoft.Json;

    public partial class CurrentPricesResponse {
        public CurrentPricesResponse() {
            this.Data = new DataC();
            this.Meta = new MetaC();
        }

        [JsonProperty("data")]
        public DataC Data { get; set; }

        [JsonProperty(".meta")]
        public MetaC Meta { get; set; }

        public class MetaC {
            [JsonProperty("currency")]
            public string Currency { get; set; }
        }


        [JsonConverter(typeof(PlainConverter))]
        public class DataC {
            public DataC() {
                this.Plain = new Plain();
            }

            [JsonProperty("plain")]
            public Plain
                Plain { get; set; }

        }

        public class Plain {
            public Plain() {
                this.List = new List[0];
                this.Urls = new Urls();;
            }

            [JsonProperty("list")]
            public List[] List { get; set; }

            [JsonProperty("urls")]
            public Urls Urls { get; set; }
        }


        public class Urls {
            [JsonProperty("game")]
            public string Game { get; set; }
        }

        public class List {
            public List() {
                this.Drm = new string[0];
                this.Shop = new Shop();
            }

            [JsonProperty("drm")]
            public string[] Drm { get; set; }

            [JsonProperty("price_cut")]
            public long PriceCut { get; set; }

            [JsonProperty("price_new")]
            public double PriceNew { get; set; }

            [JsonProperty("price_old")]
            public double PriceOld { get; set; }

            [JsonProperty("shop")]
            public Shop Shop { get; set; }

            [JsonProperty("url")]
            public string Url { get; set; }
        }

        public class Shop {
            [JsonProperty("id")]
            public string Id { get; set; }

            [JsonProperty("name")]
            public string Name { get; set; }
        }
    }
}