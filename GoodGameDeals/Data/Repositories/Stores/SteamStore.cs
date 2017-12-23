namespace GoodGameDeals.Data.Repositories.Stores {
    using System;
    using System.Reactive;
    using System.Reactive.Linq;
    using System.Reactive.Subjects;
    using System.Reactive.Threading.Tasks;
    using System.Reactive.Windows.Foundation;
    using System.Text;

    using GoodGameDeals.Data.Cache;
    using GoodGameDeals.Data.Entity.Responses.Steam;
    using GoodGameDeals.Services.JsonServices;

    using MetroLog;

    using Newtonsoft.Json;

    using Windows.Storage;
    using Windows.UI.Core;
    using Windows.UI.Xaml.Media.Imaging;

    using Reactive.Bindings.Extensions;

    using FileCache = GoodGameDeals.Data.Cache.FileCache;

    public class SteamStore : ISteamStore {
        private static readonly ILogger Log = LogManagerFactory
            .DefaultLogManager.GetLogger<SteamStore>();

        private readonly FileCache cache;

        private readonly ImageCache imageCache;

        private readonly InMemoryStorage<long> appIdCache;

        private Uri appListUri;

        private JsonSerializerSettings deserializationSettings;

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

        public IObservable<BitmapImage> GameLogo(string title) {
            var observable = new Subject<BitmapImage>();

            var placeHolderUri =
                "ms-appx:///Presentation/Assets/NoPreviewAvaliable.png";
            var uri = new Uri(placeHolderUri);
            var memoryItem = this.appIdCache.GetItem(title, TimeSpan.FromDays(1));
            if (memoryItem != null) {
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
                if (uri.OriginalString == placeHolderUri) {
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
