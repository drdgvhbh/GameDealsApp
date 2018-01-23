namespace GoodGameDeals.Data.ApiResponses.IGDB {
    using Newtonsoft.Json;

    public class SummaryResponse {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("summary")]
        public string Summary { get; set; }
    }
}
