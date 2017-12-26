namespace GoodGameDeals.Data.ApiResponses.IsThereAnyDeal.Converters {
    using System;
    using System.Linq;
    using System.Reflection;

    using GoodGameDeals.Data.ApiResponses.IsThereAnyDeal;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    public class PlainConverter : JsonConverter {
        public override bool CanConvert(Type objectType) =>
            objectType == typeof(CurrentPricesResponse.DataC);

        public override object ReadJson(
            JsonReader reader,
            Type objectType,
            object existingValue,
            JsonSerializer serializer) {
            var instance = TypeExtensions
                .GetConstructor(objectType, Type.EmptyTypes).Invoke(null);
            var props = objectType.GetProperties();

            var jo = JObject.Load(reader);
            foreach (var jp in jo.Properties()) {
                var prop = props.FirstOrDefault();
                prop?.SetValue(
                    instance,
                    jp.Value.ToObject(prop.PropertyType, serializer));
            }

            return instance;
        }

        public override void WriteJson(
                JsonWriter writer,
                object value,
                JsonSerializer serializer) {
            throw new NotImplementedException();
        }
    }
}