namespace GoodGameDeals.Data.Repositories.Stores {
    using Windows.Web.Http;

    using GoodGameDeals.Data.Cache;

    using Newtonsoft.Json;

    using Unity.Attributes;

    public class IsThereAnyDealStoreFactory {
        private HttpClient client;

        private readonly FileCache cache;

        private JsonSerializerSettings deserializationSettings;

        public IsThereAnyDealStoreFactory(
                [Dependency("IsThereAnyDealCache")]FileCache cache,
                JsonSerializerSettings deserializationSettings) {
            this.cache = cache;
            this.deserializationSettings = deserializationSettings;
        }

        public IIsThereAnyDealStore Create() {
/*            var cacheControl = new HttpBaseProtocolFilter();
            cacheControl.CacheControl.ReadBehavior = HttpCacheReadBehavior.MostRecent;
            var client = new HttpClient(cacheControl);*/
            return new IsThereAnyDealStore(this.cache, this.deserializationSettings);
        }
    }
}
