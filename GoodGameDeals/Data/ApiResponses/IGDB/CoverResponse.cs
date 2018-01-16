namespace GoodGameDeals.Data.ApiResponses.IGDB {
    using Newtonsoft.Json;

    public class CoverResponse {
        [JsonProperty("cover")]
        public Cover CoverDetail { get; set; }

        [JsonProperty("id")]
        public long Id { get; set; }

        public class Cover {
            [JsonProperty("cloudinary_id")]
            public string CloudinaryId { get; set; }

            [JsonProperty("height")]
            public long Height { get; set; }

            [JsonProperty("url")]
            public string Url { get; set; }

            [JsonProperty("width")]
            public long Width { get; set; }
        }
    }
}