namespace GoodGameDeals.Data.ApiResponses.IGDB {
    using Newtonsoft.Json;

    public class SearchForGameResponse {
        [JsonProperty("id")]
        public long Id { get; set; }
    }
}