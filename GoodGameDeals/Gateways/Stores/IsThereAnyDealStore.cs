namespace GoodGameDeals.Gateways.Stores {
    using System;
    using System.Reactive.Subjects;
    using System.Runtime.CompilerServices;
    using System.Text;
    using System.Threading.Tasks;

    using Windows.ApplicationModel.Resources;
    using Windows.Storage;

    using GoodGameDeals.Data.ApiResponses.IsThereAnyDeal;
    using GoodGameDeals.Gateways.Contracts;
    using GoodGameDeals.Services.JsonServices;

    using Newtonsoft.Json;

    using Unity.Attributes;

    public class IsThereAnyDealStore : IIsThereAnyDealStore {
         private const int RecentDealsLimit = 50;

        private readonly string apiKey;

        private Data.Cache.FileCache cache;

        private JsonSerializerSettings deserializationSettings;

        public IsThereAnyDealStore(
                [Unity.Attributes.Dependency("IsThereAnyDealCache")]Data.Cache.FileCache cache,
                JsonSerializerSettings deserializationSettings) {
            this.cache = cache;
            this.deserializationSettings = deserializationSettings;
            this.apiKey = ResourceLoader.GetForViewIndependentUse("apiKeys")
                .GetString("ITAD");
        }

        public async Task<CurrentPricesResponse> CurrentPrices(
                string plain) {
            var query = new StringBuilder();
            query.AppendFormat(
                "key={0}&plains={1}&country=CAD",
                this.apiKey,
                plain);
            var uriBuilder = new UriBuilder {
                Scheme = "https",
                Host = "api.isthereanydeal.com",
                Path = "v01/game/prices/ca",
                Query = query.ToString()
            };

            var observableResponse = new Subject<CurrentPricesResponse>();
            var file = await this.cache.GetFromCacheAsync(uriBuilder.Uri, true);
            var text = await FileIO.ReadTextAsync(file);
            var response =
                    new JsonService<CurrentPricesResponse>(
                            this.deserializationSettings)
                        .FromJson(text);
            return response;
        }

        public async Task<RecentDealsResponse> RecentDeals(
                int limit = RecentDealsLimit,
                int offset = 0) {
            if (limit > RecentDealsLimit) {
                limit = RecentDealsLimit;
            }

            var query = new StringBuilder();
            query.AppendFormat(
                "key={0}&country={1}&offset={2}&limit={3}",
                this.apiKey,
                "CA",
                offset,
                limit);
            var uriBuilder = new UriBuilder {
                Scheme = "https",
                Host = "api.isthereanydeal.com",
                Path = "v01/deals/list/ca",
                Query = query.ToString()
            };

            var observableResponse = new Subject<RecentDealsResponse>();
            var file = await this.cache.GetFromCacheAsync(uriBuilder.Uri, true);
            var text = await FileIO.ReadTextAsync(file);
            var response =
                new JsonService<RecentDealsResponse>(
                    this.deserializationSettings).FromJson(text);
            return response;
        }

        public async Task Initialize() {
        }
    }
}
