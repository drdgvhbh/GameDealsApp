namespace Hamburger1.Models.Steam {
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

    public partial class GetAppListResponse {
        public static GetAppListResponse FromJson(string json) => JsonConvert.DeserializeObject<GetAppListResponse>(json, Converter.Settings);
    }

    public static class Serialize {
        public static string ToJson(this GetAppListResponse self) => JsonConvert.SerializeObject(self, Converter.Settings);
    }

    public class Converter {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
        };
    }
}
