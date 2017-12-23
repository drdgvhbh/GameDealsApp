namespace GoodGameDeals.Data.Repositories.Stores {
    using System;
    using System.Reactive.Subjects;

    using GoodGameDeals.Data.Cache;

    using MetroLog;

    using Newtonsoft.Json;

    using Unity.Attributes;

    using FileCache = Cache.FileCache;

    public class SteamStoreFactory {
        private static readonly ILogger Log = LogManagerFactory
            .DefaultLogManager.GetLogger<SteamStoreFactory>();

        private readonly FileCache cache;

        private readonly ImageCache imageCache;

        private readonly InMemoryStorage<long> appIdCache;

        private JsonSerializerSettings deserializationSettings;

        public SteamStoreFactory(
                [Dependency("SteamAppIdCache")]InMemoryStorage<long> appIdCache,
                [Dependency("SteamLogoCache")]ImageCache imageCache,
                [Dependency("SteamCache")]FileCache cache,
                JsonSerializerSettings deserializationSettings) {
            this.appIdCache = appIdCache;
            this.cache = cache;
            this.imageCache = imageCache;
            this.deserializationSettings = deserializationSettings;
        }

        public IObservable<ISteamStore> Create() {
            var subject = new Subject<ISteamStore>();
            var store = new SteamStore(
                this.appIdCache,
                this.imageCache,
                this.cache,
                this.deserializationSettings);
            store.Initialize().Subscribe(
                _ => {
                    subject.OnNext(store);
                });
            return subject;
        }
    }
}
