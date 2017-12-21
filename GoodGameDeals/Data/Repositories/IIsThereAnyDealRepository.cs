using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodGameDeals.Data.Repositories {
    using GoodGameDeals.Data.Entity;
    using GoodGameDeals.Domain;

    public interface IIsThereAnyDealRepository {
        IObservable<List<Deal>> RecentDeals(
            Country country = Country.Cad,
            int offset = 0,
            int limit = 50);
    }
}
