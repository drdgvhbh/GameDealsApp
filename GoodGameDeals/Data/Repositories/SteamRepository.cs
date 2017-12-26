// ReSharper disable ClassNeverInstantiated.Global
namespace GoodGameDeals.Data.Repositories {
    using System;
    using System.Reactive.Subjects;

    using AutoMapper;

    using GoodGameDeals.Data.Repositories.Stores;

    using MetroLog;

    using Reactive.Bindings.Extensions;

    using Windows.UI.Xaml.Media.Imaging;

    using GoodGameDeals.Data.ApiResponses.Steam;

    /// <inheritdoc />
    /// <summary>
    ///     Represents a repository for retrieving data from the
    ///     <code>Steam</code> api.
    /// </summary>
    public class SteamRepository : ISteamRepository {
        /// <summary>
        ///     The logger for this class.
        /// </summary>
        private static readonly ILogger Log = LogManagerFactory
            .DefaultLogManager.GetLogger<SteamRepository>();

        /// <summary>
        ///     The store factory.
        /// </summary>
        private readonly SteamStoreFactory factory;

        /// <summary>
        ///     The type mapper.
        /// </summary>
        private readonly IMapper mapper;

        /// <summary>
        ///     Initializes a new instance of the
        ///     <see cref="SteamRepository"/> class.
        /// </summary>
        /// <param name="factory">
        ///     The <code>Steam</code> store factory.
        /// </param>
        /// <param name="mapper">
        ///     The type mapper.
        /// </param>
        public SteamRepository(SteamStoreFactory factory, IMapper mapper) {
            this.factory = factory;
            this.mapper = mapper;
        }

        /// <inheritdoc />
        public IObservable<BitmapImage> GameLogo(string title) {
            var subject = new Subject<BitmapImage>();
            this.factory.Create().ObserveOnUIDispatcher().Subscribe(
                steamStore => {
                    steamStore.GameLogo(title).ObserveOnUIDispatcher()
                        .Subscribe(
                            image => {
                                subject.OnNext(image);
                                Log.Debug(
                                    "Successfully retrieved game logo for game"
                                        + " with title \"{0}\".",
                                    title);
                            });
                });
            return subject;
        }

        /// <inheritdoc />
        public IObservable<GetAppListResponse> AppList() {
            var subject = new Subject<GetAppListResponse>();

            this.factory.Create().Subscribe(
                steamStore => {
                    steamStore.AppList().Subscribe(
                        response => {
                            subject.OnNext(response);
                            Log.Debug("Successfully retrieved Steam app list");
                        });
                });
            return subject;
        }
    }
}
