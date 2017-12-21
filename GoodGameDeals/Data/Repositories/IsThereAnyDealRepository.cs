using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodGameDeals.Data.Repositories {
    using System.Reactive.Linq;

    using AutoMapper;

    using GoodGameDeals.Data.Entity;
    using GoodGameDeals.Data.Repositories.Stores;
    using GoodGameDeals.Domain;

    public class IsThereAnyDealRepository : IIsThereAnyDealRepository {
        private IIsThereAnyDealStore store;

        private IMapper mapper;

        public IsThereAnyDealRepository(IIsThereAnyDealStore store, IMapper mapper) {
            this.store = store;
            this.mapper = mapper;
        }

        public IObservable<List<Deal>> RecentDeals(
                Country country = Country.Cad,
                int offset = 0,
                int limit = 50) {
            return this.store.RecentDeals(country, offset, limit)
                .Select(deal => {
                    var list = deal.Data.List;
                    var deals = list.Select(dealItem => this.mapper.Map<Deal>(dealItem)).ToList();
                    return deals;
                });
        }
    }
}
