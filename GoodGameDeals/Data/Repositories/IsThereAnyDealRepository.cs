// ReSharper disable ClassNeverInstantiated.Global
namespace GoodGameDeals.Data.Repositories {
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reactive.Linq;

    using AutoMapper;

    using GoodGameDeals.Data.Entity;
    using GoodGameDeals.Data.Repositories.Stores;
    using GoodGameDeals.Domain;

    using MetroLog;

    /// <inheritdoc />
    /// <summary>
    ///     Represents a repository for retrieving data from the
    ///     <code>IsThereAnyDeal</code> api.
    /// </summary>
    public class IsThereAnyDealRepository : IIsThereAnyDealRepository {
        /// <summary>
        ///     The logger for this class.
        /// </summary>
        private static readonly ILogger Log = LogManagerFactory
            .DefaultLogManager.GetLogger<IsThereAnyDealRepository>();

        /// <summary>
        ///     The store factory.
        /// </summary>
        private readonly IsThereAnyDealStoreFactory factory;

        /// <summary>
        ///     The type mapper.
        /// </summary>
        private readonly IMapper mapper;

        /// <summary>
        ///     Initializes a new instance of the
        ///  <see cref="IsThereAnyDealRepository"/> class.
        /// </summary>
        /// <param name="factory">
        ///     The <code>IsThereAnyDeal</code> store factory.
        /// </param>
        /// <param name="mapper">
        ///     The type mapper.
        /// </param>
        public IsThereAnyDealRepository(
                IsThereAnyDealStoreFactory factory,
                IMapper mapper) {
            this.factory = factory;
            this.mapper = mapper;
        }

        /// <inheritdoc />
        /// <returns>
        ///     The recent deals from the <code>IsThereAnyDeal</code> api.
        /// </returns>
        /// <remarks>
        ///     Refer to the documentation for the
        ///     <a href="https://goo.gl/P3WUuu">Recent Deals</a>
        ///      api request.
        /// </remarks>
        public IObservable<List<Deal>> RecentDeals(
                Country country,
                int offset,
                int limit) {
            return this.factory.Create().RecentDeals(country, offset, limit)
                .Select(
                    deal => {
                        var list = deal.Data.List;
                        var deals = list.Select(
                                dealItem => this.mapper.Map<Deal>(dealItem))
                            .ToList();
                        Log.Debug(
                            "Successfully retrieved {0} recent deals.",
                            deals.Count);
                        return deals;
                    });
        }

        /// <inheritdoc />
        /// <returns>
        ///     The current deals for a particular game
        ///      from the <code>IsThereAnyDeal</code> api.
        /// </returns>
        /// <remarks>
        ///     Refer to the documentation for the
        ///     <a href="https://goo.gl/jJodbe">Get current prices</a>
        ///      api request.
        /// </remarks>
        public IObservable<List<Deal>> GameCurrentPrices(
                string plain,
                Country country = Country.Cad) {
            return this.factory.Create().CurrentPrices(plain, country).Select(
                dealList => {
                    var list = dealList.Data.Plain.List;
                    var deals = list.Select(
                        dealItem => this.mapper.Map<Deal>(dealItem)).ToList();
                    Log.Debug(
                        "Successfully retrieved {0} deal(s) for game with"
                        + " plain \"{1}\".",
                        deals.Count,
                        plain);
                    return deals;
                });
        }
    }
}
