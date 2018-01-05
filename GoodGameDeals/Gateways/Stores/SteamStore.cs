namespace GoodGameDeals.Gateways.Stores {
    using System;
    using System.Text;
    using System.Threading.Tasks;

    using GoodGameDeals.Data.ApiResponses.Steam;
    using GoodGameDeals.Gateways.Contracts;
    using GoodGameDeals.Services.JsonServices;

    using MetroLog;

    using Microsoft.Toolkit.Uwp.UI;

    using Newtonsoft.Json;

    using Windows.ApplicationModel.Core;
    using Windows.Storage;
    using Windows.UI.Core;
    using Windows.UI.Xaml.Media.Imaging;

    using Unity.Attributes;


    /// <summary>
    ///     Represents a store to retrieve data from the <code>Steam</code> api.
    /// </summary>
    public class SteamStore : ISteamStore {
        /// <summary>
        ///     The logger for this class.
        /// </summary>
        private static readonly ILogger Log = LogManagerFactory
            .DefaultLogManager.GetLogger<SteamStore>();

        /// <summary>
        ///     The cache to store the api response from <see cref="AppList"/>.
        /// </summary>
        private readonly Data.Cache.FileCache cache;

        /// <summary>
        ///     The image cache to store game logos.
        /// </summary>
        private readonly ImageCache imageCache;

        /// <summary>
        ///     The cache to store the id of each steam game.
        /// </summary>
        private readonly InMemoryStorage<long> appIdCache;

        /// <summary>
        ///     The <see cref="Uri"/> to retrieve Steam's appList.
        /// </summary>
        private readonly Uri appListUri;

        /// <summary>
        ///     The JSON deserialization settings.
        /// </summary>
        private readonly JsonSerializerSettings deserializationSettings;

        /// <summary>
        /// Initializes a new instance of the <see cref="SteamStore"/> class.
        /// </summary>
        /// <param name="appIdCache">
        ///     The app id cache.
        /// </param>
        /// <param name="imageCache">
        ///     The image cache.
        /// </param>
        /// <param name="cache">
        ///     The file cache.
        /// </param>
        /// <param name="deserializationSettings">
        ///     The JSON deserialization settings.
        /// </param>
        public SteamStore(
                [Dependency("SteamAppIdCache")]InMemoryStorage<long> appIdCache,
                [Dependency("SteamLogoCache")]ImageCache imageCache,
                [Dependency("SteamCache")]Data.Cache.FileCache cache,
                JsonSerializerSettings deserializationSettings) {
            this.appIdCache = appIdCache;
            this.imageCache = imageCache;
            this.cache = cache;
            this.deserializationSettings = deserializationSettings;

            var uriBuilder = new UriBuilder {
                Scheme = "https",
                Host = "api.steampowered.com",
                Path = "ISteamApps/GetAppList/v0002"
            };

            this.appListUri = uriBuilder.Uri;
        }

        /// <summary>
        ///     Retrieves the game logo for a game.
        /// </summary>
        /// <param name="title">
        ///     The title of the game.
        /// </param>
        /// <returns>
        ///     The game logo if found; otherwise, a placeholder image.
        /// </returns>
        /// <remarks>
        ///     The function checks if the image, if its uri is valid,
        ///     exists in the cache first. If the image is not found,
        ///     it is retrieved online and stored into an image cache.
        /// </remarks>
        public async Task<BitmapImage> GameLogo(string title) {
                const string PlaceHolderUri =
                    "ms-appx:///Presentation/Assets/NoPreviewAvaliable.png";
                var uri = new Uri(PlaceHolderUri);
                var memoryItem = this.appIdCache.GetItem(
                    title,
                    TimeSpan.FromDays(1));
                if (memoryItem != null) {
                    // Item is not in the cache go find it online
                    var appId = memoryItem.Item;
                    var sb = new StringBuilder();
                    sb.AppendFormat("steam/apps/{0}/header.jpg", appId);
                    var uriBuilder =
                        new UriBuilder
                            {
                                Scheme = "http",
                                Host = "cdn.akamai.steamstatic.com",
                                Path = sb.ToString()
                            };
                    uri = uriBuilder.Uri;
                }

                var dispatcher = CoreApplication.MainView.CoreWindow.Dispatcher;
                BitmapImage image = null;
                await dispatcher.RunAsync(
                    CoreDispatcherPriority.High,
                    async () => {
                            if (uri.OriginalString == PlaceHolderUri) {
                                image = new BitmapImage(uri);
                            }
                            else {
                                image = new BitmapImage(uri);
                                // TODO: Create Cache that actually works
                     /*           image = await this.imageCache.GetFromCacheAsync(
                                            uri);*/
/*                                if (image == null) {
                                    image = await this.imageCache.GetFromCacheAsync(
                                                uri);
                                }*/
                            }
                        });
                return image;
        }

        public async Task<GetAppListResponse> AppList() {
            var file = await this.cache.GetFromCacheAsync(this.appListUri, true);
            var text = await FileIO.ReadTextAsync(file);
            var response =
                new JsonService<GetAppListResponse>(
                    this.deserializationSettings).FromJson(text);
            this.FillAppIdCache(response);
            return response;
        }

        public long GetAppId(string title) {
            throw new NotImplementedException();
        }

        public async Task Initialize() {
            await this.AppList();
        }

        private void FillAppIdCache(GetAppListResponse response) {
            foreach (var item in response.AppList.Apps) {
                if (this.appIdCache.GetItem(
                        item.Name, TimeSpan.FromDays(1)) == null) {
                    this.appIdCache.SetItem(new InMemoryStorageItem<long>(
                        item.Name,
                        DateTime.Now,
                        item.Appid));
                }
            }
        }
    }
}