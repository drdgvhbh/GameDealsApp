namespace GoodGameDeals.Models.Steam {
    using Newtonsoft.Json;

    public partial class GetAppListResponse {
        [JsonProperty("applist")]
        public Applist Applist { get; set; }
    }

    public partial class Applist {
        [JsonProperty("apps")]
        public App[] Apps { get; set; }
    }

    public partial class App {
        [JsonProperty("appid")]
        public long Appid { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
