namespace GoodGameDeals.Data.Repositories.Stores {
    using System;
    using System.Collections.Generic;
    using System.Reactive;

    using GoodGameDeals.Data.Entity;
    using GoodGameDeals.Data.Entity.Responses.IsThereAnyDeal;

    public interface IIsThereAnyDealStore {
        IObservable<RecentDealsResponse> RecentDeals(
            Country country = Country.Cad,
            int offset = 0,
            int limit = 50);

        IObservable<CurrentPricesResponse> CurrentPrices(
            string plain,
            Country country = Country.Cad);

        IObservable<Unit> Initialize();
    }
}