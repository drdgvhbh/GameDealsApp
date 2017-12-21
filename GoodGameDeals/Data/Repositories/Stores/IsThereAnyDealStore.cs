namespace GoodGameDeals.Data.Repositories.Stores {
    using System;
    using System.Collections.Generic;
    using System.Reactive.Linq;
    using System.Reactive.Subjects;
    using System.Reactive.Windows.Foundation;
    using System.Text;

    using Windows.ApplicationModel.Resources;
    using Windows.Web.Http;

    using GoodGameDeals.Data.Entity;
    using GoodGameDeals.Data.Entity.Responses.IsThereAnyDeal;
    using GoodGameDeals.Services.JsonServices;

    using Newtonsoft.Json;

    public class IsThereAnyDealStore : IIsThereAnyDealStore {
        private HttpClient client;

        private const int RecentDealsLimit = 50;

        private readonly string apiKey;

        private JsonSerializerSettings deserializationSettings;

        public IsThereAnyDealStore(
                HttpClient client,
                JsonSerializerSettings deserializationSettings) {
            this.client = client;
            this.deserializationSettings = deserializationSettings;
            this.apiKey = ResourceLoader.GetForViewIndependentUse("apiKeys")
                .GetString("ITAD");
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
            this.client.GetAsync(uriBuilder.Uri).ToObservable().Subscribe();
            var observableResponse = new Subject<RecentDealsResponse>();
            this.client.GetAsync(uriBuilder.Uri).ToObservable()
                .Subscribe(
                    response => {
                        var recentDeals =
                            new JsonService<RecentDealsResponse>(
                                this.deserializationSettings).FromJson(
                                response.Content.ToString());
                        observableResponse.OnNext(recentDeals);
                    });
            return observableResponse;

        }
    }
}
