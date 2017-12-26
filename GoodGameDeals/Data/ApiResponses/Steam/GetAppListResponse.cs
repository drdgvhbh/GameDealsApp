namespace GoodGameDeals.Data.ApiResponses.Steam {
    using Newtonsoft.Json;

    using NullGuard;

    public class GetAppListResponse {
        public GetAppListResponse() {
            this.AppList = new Applist();
        }

        [JsonProperty("applist")]
        public Applist AppList { get; set; }

        public class Applist {
            public Applist() {
                this.Apps = new App[0];
            }

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
