namespace GoodGameDeals.Data.Repositories.Stores {
    using Windows.Web.Http;

    using Newtonsoft.Json;

    public class IsThereAnyDealStoreFactory {
        private HttpClient client;

        private JsonSerializerSettings deserializationSettings;

        public IsThereAnyDealStoreFactory(
                HttpClient client,
                JsonSerializerSettings deserializationSettings) {
            this.client = client;
        }

        public IIsThereAnyDealStore create() {
/*            var cacheControl = new HttpBaseProtocolFilter();
            cacheControl.CacheControl.ReadBehavior = HttpCacheReadBehavior.MostRecent;
            var client = new HttpClient(cacheControl);*/
            return new IsThereAnyDealStore(this.client, this.deserializationSettings);
        }
    }
}
