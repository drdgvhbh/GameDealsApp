namespace GoodGameDeals.Data.Repositories.Stores {
    using System;
    using System.Collections.Generic;

    using GoodGameDeals.Data.Entity;
    using GoodGameDeals.Data.Entity.Responses.IsThereAnyDeal;

    public interface IIsThereAnyDealStore {
        IObservable<RecentDealsResponse> RecentDeals(
            Country country = Country.Cad,
            int offset = 0,
            int limit = 50);
    }
}