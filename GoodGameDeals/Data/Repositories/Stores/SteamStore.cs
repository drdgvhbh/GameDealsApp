namespace GoodGameDeals.Data.Repositories.Stores {
    using System;
    using System.Reactive;
    using System.Reactive.Subjects;
    using System.Reactive.Windows.Foundation;
    using System.Text;

    using GoodGameDeals.Data.Cache;
    using GoodGameDeals.Data.Entity.Responses.Steam;
    using GoodGameDeals.Services.JsonServices;

    using MetroLog;

    using Newtonsoft.Json;

    using Reactive.Bindings.Extensions;

    using Windows.Storage;
    using Windows.UI.Core;
    using Windows.UI.Xaml.Media.Imaging;

    using FileCache = GoodGameDeals.Data.Cache.FileCache;

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
        private readonly FileCache cache;

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
                InMemoryStorage<long> appIdCache,
                ImageCache imageCache,
                FileCache cache,
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
        public IObservable<BitmapImage> GameLogo(string title) {
            var observable = new Subject<BitmapImage>();

            const string PlaceHolderUri =
                "ms-appx:///Presentation/Assets/NoPreviewAvaliable.png";
            var uri = new Uri(PlaceHolderUri);
            var memoryItem = this.appIdCache.GetItem(title, TimeSpan.FromDays(1));
            if (memoryItem != null) {
                // Item is not in the cache go find it online
                var appId = memoryItem.Item;
                var sb = new StringBuilder();
                sb.AppendFormat("steam/apps/{0}/header.jpg", appId);
                var uriBuilder = new UriBuilder {
                    Scheme = "http",
                    Host = "cdn.akamai.steamstatic.com",
                    Path = sb.ToString()
                };
                uri = uriBuilder.Uri;
            }

            try {
                var dispatcher = CoreWindow.GetForCurrentThread().Dispatcher;
                if (uri.OriginalString == PlaceHolderUri) {
                    dispatcher
                        .RunAsync(CoreDispatcherPriority.Normal, () => { })
                        .ToObservable().Subscribe(
                            _ => {
                                observable.OnNext(new BitmapImage(uri));
                            });
                    return observable;
                }

            }
            catch (NullReferenceException e) {
                Log.Error("BitmapImages must be created on the UI thread", e);
                throw;
            }

            this.imageCache.GetFromCacheAsync(uri, true)
                .AsAsyncOperation().ToObservable().ObserveOnUIDispatcher().Subscribe(
                    image => {
                        observable.OnNext(image);
                    });

            return observable;
        }

        public IObservable<GetAppListResponse> AppList() {
            var observable = new Subject<GetAppListResponse>();
            this.cache.GetFromCacheAsync(this.appListUri, true)
                .AsAsyncOperation().ToObservable().Subscribe(
                    file => {
                        FileIO.ReadTextAsync(file)
                            .ToObservable().Subscribe(
                                text => {
                                    var response =
                                        new JsonService<GetAppListResponse>(
                                                this.deserializationSettings)
                                            .FromJson(text);
                                    this.FillAppIdCache(response);
                                    observable.OnNext(response);
                                });
                    });
            return observable;
        }

        public IObservable<Unit> Initialize() {
            var subject = new Subject<Unit>();
            this.AppList().Subscribe(
                response => {
                    subject.OnNext(new Unit());
                });
/*            var zippedSequence = Observable.When(
                one.And(two)
                    .And(three)
                    .Then((first, second, third) =>
                        new {
                            One = first,
                            Two = second,
                            Three = third
                        })
            );
            zippedSequence.Subscribe(
                Console.WriteLine,
                () => Console.WriteLine("Completed"));*/


            return subject;
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
