namespace GoodGameDeals.Data.Repositories.Stores {
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Reactive;
    using System.Reactive.Linq;
    using System.Reactive.Subjects;
    using System.Reactive.Threading.Tasks;
    using System.Reactive.Windows.Foundation;
    using System.Text;

    using Windows.ApplicationModel.Resources;
    using Windows.Storage;
    using Windows.Web.Http;

    using GoodGameDeals.Data.ApiResponses.IsThereAnyDeal;
    using GoodGameDeals.Data.Cache;
    using GoodGameDeals.Data.Localization;
    using GoodGameDeals.Services.JsonServices;

    using Newtonsoft.Json;

    public class IsThereAnyDealStore : IIsThereAnyDealStore {
        private const int RecentDealsLimit = 50;

        private readonly string apiKey;

        private FileCache cache;

        private JsonSerializerSettings deserializationSettings;

        public IsThereAnyDealStore(
                FileCache cache,
                JsonSerializerSettings deserializationSettings) {
            this.cache = cache;
            this.deserializationSettings = deserializationSettings;
            this.apiKey = ResourceLoader.GetForViewIndependentUse("apiKeys")
                .GetString("ITAD");
        }

        public IObservable<CurrentPricesResponse> CurrentPrices(
                string plain,
                Country country = Country.Cad) {
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
            this.cache.GetFromCacheAsync(uriBuilder.Uri, true)
                .AsAsyncOperation().ToObservable().Subscribe(
                    file => {
                        FileIO.ReadTextAsync(file).ToObservable()
                            .Subscribe(
                                text => {
                                    var response =
                                        new JsonService<CurrentPricesResponse>(
                                                this.deserializationSettings)
                                            .FromJson(text);
                                    observableResponse.OnNext(response);
                                });
                    });
            return observableResponse;
        }

        public IObservable<RecentDealsResponse> RecentDeals(
                Country country = Country.Cad,
                int offset = 0,
                int limit = RecentDealsLimit) {
            if (limit > RecentDealsLimit) {
                limit = RecentDealsLimit;
            }

            var query = new StringBuilder();
            query.AppendFormat(
                "key={0}&country={1}&offset={2}&limit={3}",
                this.apiKey,
                country.ToString().ToUpper(),
                offset,
                limit);
            var uriBuilder = new UriBuilder {
                Scheme = "https",
                Host = "api.isthereanydeal.com",
                Path = "v01/deals/list/ca",
                Query = query.ToString()
            };

            var observableResponse = new Subject<RecentDealsResponse>();
            this.cache.GetFromCacheAsync(uriBuilder.Uri, true)
                .AsAsyncOperation().ToObservable().Subscribe(
                    file => {
                        FileIO.ReadTextAsync(file).ToObservable()
                            .Subscribe(
                                text => {
                                    var response =
                                        new JsonService<RecentDealsResponse>(
                                                this.deserializationSettings)
                                            .FromJson(text);
                                    observableResponse.OnNext(response);
                                });
                    });
            return observableResponse;
        }

        public IObservable<Unit> Initialize() {
            var subject = new Subject<Unit>();
/*            var one = this.cache.GetFromCacheAsync()*/
    /*        this    */

            return subject;
        }
    }
}
