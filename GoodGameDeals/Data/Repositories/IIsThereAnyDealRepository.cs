using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodGameDeals.Data.Repositories {
    using GoodGameDeals.Data.Entity;
    using GoodGameDeals.Data.Entity.Responses.IsThereAnyDeal;
    using GoodGameDeals.Domain;

    public interface IIsThereAnyDealRepository {
        IObservable<List<Deal>> RecentDeals(
            Country country = Country.Cad,
            int offset = 0,
            int limit = 50);

        IObservable<List<Deal>> CurrentPrices(
            string plain,
            Country country = Country.Cad);
    }
}
