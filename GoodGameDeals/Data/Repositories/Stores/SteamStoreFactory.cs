// ReSharper disable ClassNeverInstantiated.Global
namespace GoodGameDeals.Data.Repositories.Stores {
    using System;
    using System.Reactive.Subjects;

    using GoodGameDeals.Data.Cache;

    using MetroLog;

    using Newtonsoft.Json;

    using Unity.Attributes;

    using FileCache = Cache.FileCache;

    /// <summary>
    ///     Represents a factory for creating stores to retrieve data from the
    ///     <code>Steam</code> api.
    /// </summary>
    public class SteamStoreFactory {
        /// <summary>
        ///     The logger for this class.
        /// </summary>
        private static readonly ILogger Log = LogManagerFactory
            .DefaultLogManager.GetLogger<SteamStoreFactory>();

        /// <summary>
        ///     The cache to store files.
        /// </summary>
        private readonly FileCache cache;

        /// <summary>
        ///     The cache to store images.
        /// </summary>
        private readonly ImageCache imageCache;

        /// <summary>
        ///     The cache to store steam app ids.
        /// </summary>
        private readonly InMemoryStorage<long> appIdCache;

        /// <summary>
        ///     The settings to deserialize JSON.
        /// </summary>
        private readonly JsonSerializerSettings deserializationSettings;

        /// <summary>
        ///     Initializes a new instance of the <see cref="SteamStoreFactory" /> class.
        /// </summary>
        /// <param name="appIdCache">
        ///     The app id cache.
        /// </param>
        /// <param name="imageCache">
        ///     The image cache.
        /// </param>
        /// <param name="cache">
        ///     The cache.
        /// </param>
        /// <param name="deserializationSettings">
        ///     The JSON deserialization settings.
        /// </param>
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

        /// <summary>
        ///     Creates a store for retrieving <code>Steam</code> api data.
        /// </summary>
        /// <returns>
        ///      A store for retrieving <code>Steam</code> api data.
        /// </returns>
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
