namespace GoodGameDeals.Data.Repositories.Stores {
    using System;
    using System.Reactive;

    using GoodGameDeals.Data.ApiResponses.IsThereAnyDeal;
    using GoodGameDeals.Data.Localization;

    public interface IIsThereAnyDealStore {
        IObservable<RecentDealsResponse> RecentDeals(
            Country country,
            int offset,
            int limit);

        IObservable<CurrentPricesResponse> CurrentPrices(
            string plain,
            Country country = Country.Cad);

        IObservable<Unit> Initialize();
    }
}