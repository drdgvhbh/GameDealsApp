namespace GoodGameDeals.Services.JsonServices {
    using Newtonsoft.Json;

    public class JsonService<T> {
        private readonly JsonSerializerSettings settings;

        public JsonService(JsonSerializerSettings settings) {
            this.settings = settings;
        }

        public T FromJson(string json) =>
            JsonConvert.DeserializeObject<T>(json, this.settings);

        public string ToJson(T jsonObject) =>
            JsonConvert.SerializeObject(jsonObject, this.settings);
    }
}