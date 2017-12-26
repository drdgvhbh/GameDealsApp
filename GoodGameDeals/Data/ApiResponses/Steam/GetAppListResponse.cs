namespace GoodGameDeals.Data.ApiResponses.Steam {
    using Newtonsoft.Json;

    public class GetAppListResponse {
        [JsonProperty("applist")]
        public Applist AppList { get; set; }

        public class Applist {
            [JsonProperty("apps")]
            public App[] Apps { get; set; }
        }

        public class App {
            [JsonProperty("appid")]
            public long Appid { get; set; }

            [JsonProperty("name")]
            public string Name { get; set; }
        }
    }

}
