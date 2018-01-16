namespace GoodGameDeals.Gateways.Stores {
    using System;
    using System.Text;
    using System.Threading.Tasks;

    using GoodGameDeals.Gateways.Contracts;

    using Newtonsoft.Json;

    using Windows.ApplicationModel.Resources;
    using Windows.Storage;
    using Windows.Web.Http;

    using GoodGameDeals.Data.ApiResponses.IGDB;
    using GoodGameDeals.Data.ApiResponses.IsThereAnyDeal;
    using GoodGameDeals.Data.Cache;
    using GoodGameDeals.Services.JsonServices;

    using Unity.Attributes;

    public class IGDBStore : IIGDBStore {
        private readonly string apiKey;

        private readonly JsonSerializerSettings deserializationSettings;

        private readonly FileCache cache;


        public IGDBStore(
                [Dependency("IGDBCache")]FileCache cache,
                JsonSerializerSettings deserializationSettings) {
            this.cache = cache;
            this.deserializationSettings = deserializationSettings;
        }

        public async Task<SearchForGameResponse[]> SearchForGame(string searchQuery) {
            var query = new StringBuilder();
            query.AppendFormat("search={0}", searchQuery);
            query.Replace(" ", "+");
            var uriBuilder = new UriBuilder
                                 {
                                     Scheme = "https",
                                     Host =
                                         "api-2445582011268.apicast.io",
                                     Path = "games/",
                                     Query = query.ToString()
                                 };
            var file = await this.cache.GetFromCacheAsync(uriBuilder.Uri, true);
            var text = await FileIO.ReadTextAsync(file);
            var response =
                new JsonService<SearchForGameResponse[]>(
                        this.deserializationSettings)
                    .FromJson(text);
            return response;
        }

        public async Task<CoverResponse[]> CoverImage(long id) {
            var path = new StringBuilder();
            path.AppendFormat("games/{0}/", id);
            var uriBuilder = new UriBuilder
                                 {
                                     Scheme = "https",
                                     Host =
                                         "api-2445582011268.apicast.io",
                                     Path = path.ToString(),
                                     Query = "fields=cover"
                                 };
            var file = await this.cache.GetFromCacheAsync(uriBuilder.Uri, true);
            var text = await FileIO.ReadTextAsync(file);
            var response =
                new JsonService<CoverResponse[]>(
                        this.deserializationSettings)
                    .FromJson(text);
            return response;
        }
    }
}