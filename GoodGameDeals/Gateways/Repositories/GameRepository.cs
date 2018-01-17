namespace GoodGameDeals.Gateways.Repositories {
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reactive.Concurrency;
    using System.Reactive.Linq;
    using System.Reactive.Subjects;
    using System.Threading;
    using System.Threading.Tasks;

    using GoodGameDeals.Core.Contracts.Repositories;
    using GoodGameDeals.Core.Entities;
    using GoodGameDeals.Data.ApiResponses.IsThereAnyDeal;
    using GoodGameDeals.Gateways.Contracts;
    using GoodGameDeals.Threading;

    using MetroLog;

    using Windows.ApplicationModel.Core;
    using Windows.UI.Core;
    using Windows.UI.Xaml.Media.Imaging;

    using Reactive.Bindings.Extensions;

    using IIsThereAnyDealStore = GoodGameDeals.Gateways.Contracts.IIsThereAnyDealStore;

    public class GameRepository : IGamesRepository {
        /// <summary>
        ///     The logger for this class.
        /// </summary>
        private static readonly ILogger Log = LogManagerFactory
            .DefaultLogManager.GetLogger<GameRepository>();

        private readonly ISteamStore steamStore;

        private readonly IIsThereAnyDealStore isThereAnyDealStore;

        private readonly ITaskDelay taskDelay;

        private readonly IIGDBStore igdbStore;

        public GameRepository(
            ISteamStore steamStore,
            IIsThereAnyDealStore dealStore,
            ITaskDelay taskDelay) {
            this.taskDelay = taskDelay;
            this.isThereAnyDealStore = dealStore;
            this.steamStore = steamStore;
        }

        public GameRepository(
                ISteamStore steamStore,
                IIsThereAnyDealStore dealStore,
                IIGDBStore igdbStore,
                ITaskDelay taskDelay) {
            this.taskDelay = taskDelay;
            this.igdbStore = igdbStore;
            this.isThereAnyDealStore = dealStore;
            this.steamStore = steamStore;
        }

        public async Task<IEnumerable<Game>> GetGamesByMostRecentDeals(
                int quantity) {
            return await this.GetGamesByMostRecentDeals(quantity, 0);
        }

        public async Task<IEnumerable<Game>> GetGamesByMostRecentDeals(
                int quantity, int offset) {
            return await this.GetGamesByMostRecentDeals(
                       quantity,
                       offset,
                       TimeSpan.FromSeconds(20));
        }

        public async Task<IEnumerable<Game>> GetGamesByMostRecentDeals(
            int quantity, int offset, TimeSpan timeout) {
            return await this.GetGamesByMostRecentDeals(
                       quantity,
                       offset,
                       timeout,
                       TimeSpan.FromMilliseconds(100));
        }

        /// <summary>
        ///     Gets an enumerable collection of games based on the most
        ///     recent game deals.
        /// </summary>
        /// <param name="quantity">
        ///     The number of games to retrieve.
        /// </param>
        /// <param name="offset">
        ///     The position on the recent deals list to start retrieving games.
        /// </param>
        /// <returns>
        ///     The enumerable collection of games based on the most
        ///     recent game deals.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        ///     The <code>quantity</code> or <code>offset</code> is negative.
        /// </exception>
        public async Task<IEnumerable<Game>> GetGamesByMostRecentDeals(
                int quantity,
                int offset,
                TimeSpan timeout,
                TimeSpan delay) {
            if (quantity < 0) {
                throw new ArgumentOutOfRangeException(
                    nameof(quantity),
                    nameof(quantity) + " must be positive.");
            }

            if (offset < 0) {
                throw new ArgumentOutOfRangeException(
                    nameof(offset),
                    nameof(offset) + " must be positive.");
            }

            RecentDealsResponse recentDeals;
            try {
                recentDeals =
                    await this.isThereAnyDealStore.RecentDeals(
                        quantity,
                        offset);
            }
            catch (NullReferenceException e) {
                const string Message =
                    nameof(this.isThereAnyDealStore.RecentDeals)
                    + " returned null.";
                Log.Error(Message, e);
                throw new NullReferenceException(Message);
            }

            var derp = await this.igdbStore.SearchForGame("Europa Universalis 4");
            var games = new ConcurrentBag<Game>();

            foreach (var deal in recentDeals.Data.DealsList) {
                try {
                    var gamePrices =
                        await this.isThereAnyDealStore
                            .CurrentPrices(deal.Plain);
                    var deals = new List<Deal>();
                    foreach (var priceDeal in gamePrices.Data.Plain.List) {
                        if (priceDeal.PriceNew < priceDeal.PriceOld) {
                            deals.Add(
                                new Deal(
                                    DateTime.MinValue
                                    + TimeSpan.FromMilliseconds(deal.Added),
                                    priceDeal.Drm.ToList(),
                                    deal.Title,
                                    new Discount(
                                        (float)priceDeal.PriceNew,
                                        (float)priceDeal.PriceOld),
                                    new Store(
                                        priceDeal.Shop.Id,
                                        priceDeal.Shop.Name),
                                    priceDeal.Url));
                        }
                    }

                    var imageId = await this.igdbStore.SearchForGame(deal.Title);
                    if (imageId.Length == 0) {
                        continue;
                    }

                    var imageResponse =
                        await this.igdbStore.CoverImage(imageId[0].Id);
                    if (imageResponse.Length == 0) {
                        continue;
                    }

                    var imageUrl = "ms-appx:///Presentation/Assets/NoPreviewAvaliable.png";
                    for (var i = 0; i < imageResponse.Length; i++) {
                        if (imageResponse[i].CoverDetail != null
                                && imageResponse[i].CoverDetail.Url != null) {
                            imageUrl = imageResponse[i].CoverDetail.Url;
                            imageUrl = imageUrl.Replace("thumb", "cover_big");
                            Uri result;
                            if (!Uri.IsWellFormedUriString(
                                    imageUrl,
                                    UriKind.Absolute)) {
                                imageUrl = "https:" + imageUrl;
                            }

                        }
                    }

                    games.Add(
                        new Game(
                            deal.Plain,
                            deal.Title,
                            new Uri(imageUrl),
                            deals));


                }
                catch (Exception e) {
                    Log.Error(e.Message, e);
                    Environment.FailFast(e.Message, e);
                }
            }

            var time = new TimeSpan();
            while (games.Count != quantity
                   && time.Duration().TotalMilliseconds <= timeout.TotalMilliseconds) {
                await Task.Delay(delay);
                time = time.Add(delay);
            }

            if (games.Count != quantity) {
                Log.Warn("Unable to retrieve the requested quantity of games.");
            }
            return games;
        }

        private void HandleError(Task task) {

            if (!task.IsFaulted) {
                throw new ArgumentException("Task is not faulted.");
            }
            foreach (var e in task.Exception.InnerExceptions) {
                    Log.Error(e.Message, e);
                    throw e;
            }
        }
    }
}
