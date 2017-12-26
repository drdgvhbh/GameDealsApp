namespace GoodGameDeals.Data.Repositories {
    using System;
    using System.Collections.Generic;

    using GoodGameDeals.Data.Localization;
    using GoodGameDeals.Domain;

    /// <summary>
    ///     Defines a repository for retrieving data from the
    ///     <code>IsThereAnyDeal</code> api.
    /// </summary>
    public interface IIsThereAnyDealRepository {
        /// <summary>
        ///     Retrieves the recent deals from the <code>IsThereAnyDeal</code> api.
        /// </summary>
        /// <param name="country">
        ///     The country to find deals in.
        /// </param>
        /// <param name="offset">
        ///     The deal entry to start retrieving deals from.
        /// </param>
        /// <param name="limit">
        ///     The maximum number of entries that should be returned.
        /// </param>
        /// <returns>
        ///     The recent deals.
        /// </returns>
        IObservable<List<Deal>> RecentDeals(
            Country country,
            int offset,
            int limit);

        /// <summary>
        ///     Retrieves the current deals for a particular game.
        /// </summary>
        /// <param name="plain">
        ///     The plain name of the game.
        /// </param>
        /// <param name="country">
        ///     The country to find deals in.
        /// </param>
        /// <returns>
        ///     The current deals for a particular game.
        /// </returns>
        IObservable<List<Deal>> GameCurrentPrices(
            string plain,
            Country country = Country.Cad);
    }
}
