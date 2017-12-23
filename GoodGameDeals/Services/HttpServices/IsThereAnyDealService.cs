namespace GoodGameDeals.Services.HttpServices {
    using System;
    using System.Text;
    using System.Threading.Tasks;

    using Windows.ApplicationModel.Resources;
    using Windows.Web.Http;

    using GoodGameDeals.Data.Entity.Responses.IsThereAnyDeal;
    using GoodGameDeals.Data.Repositories.Stores;

    using MetroLog;

    public class IsThereAnyDealService {
        private static readonly ILogger Log = LogManagerFactory
            .DefaultLogManager.GetLogger<IsThereAnyDealService>();

        private const int RecentDealsLimit = 50;

        private readonly HttpClient client;

        private readonly string apiKey;

        private readonly Func<string, RecentDealsResponse> recentDealsDeserializer;

        private readonly Func<string, CurrentPricesResponse> currentPricesDeserializer;

        public IsThereAnyDealService(
/*                HttpClient client,*/
                Func<string, RecentDealsResponse> recentDealsDeserializer,
                Func<string, CurrentPricesResponse> currentPricesDeserializer) {
            this.client = new HttpClient();
            this.recentDealsDeserializer = recentDealsDeserializer;
            this.currentPricesDeserializer = currentPricesDeserializer;
            this.apiKey = ResourceLoader.GetForCurrentView("apiKeys")
                .GetString("ITAD");
        }

        public enum Country {
            Cad
        }

        public async Task<CurrentPricesResponse> CurrentPrices(
                string Plain,
                Country country = Country.Cad) {
            var query = new StringBuilder();
            query.AppendFormat(
                "key={0}&plains={1}&country=CAD",
                this.apiKey,
                Plain);
            var uriBuilder = new UriBuilder {
                Scheme = "https",
                Host = "api.isthereanydeal.com",
                Path = "v01/game/prices/ca",
                Query = query.ToString()
            };

            // Log.Debug(uriBuilder.Uri.ToString());
            var response = await this.client.GetAsync(uriBuilder.Uri);
            return this.currentPricesDeserializer(response.Content.ToString());
        }

        public async Task<RecentDealsResponse> RecentDeals(
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
            var uriBuilder = new UriBuilder
            {
                Scheme = "https",
                Host = "api.isthereanydeal.com",
                Path = "v01/deals/list/ca",
                Query = query.ToString()
            };
            var response = await this.client.GetAsync(uriBuilder.Uri);
            return this.recentDealsDeserializer(response.Content.ToString());
        }
    }
}