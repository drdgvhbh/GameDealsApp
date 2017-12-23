namespace GoodGameDeals.Data.Repositories {
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reactive.Linq;
    using System.Reactive.Subjects;

    using AutoMapper;

    using GoodGameDeals.Data.Entity;
    using GoodGameDeals.Data.Entity.Responses.IsThereAnyDeal;
    using GoodGameDeals.Data.Repositories.Stores;
    using GoodGameDeals.Domain;

    public class IsThereAnyDealRepository : IIsThereAnyDealRepository {
        private IsThereAnyDealStoreFactory factory;

        private IMapper mapper;

        public IsThereAnyDealRepository(IsThereAnyDealStoreFactory factory, IMapper mapper) {
            this.factory = factory;
            this.mapper = mapper;
        }

        public IObservable<List<Deal>> RecentDeals(
                Country country = Country.Cad,
                int offset = 0,
                int limit = 50) {
            return this.factory.Create().RecentDeals(country, offset, limit)
                .Select(deal => {
                    var list = deal.Data.List;
                    var deals = list.Select(dealItem => this.mapper.Map<Deal>(dealItem)).ToList();
                    return deals;
                });
        }

        public IObservable<List<Deal>> CurrentPrices(
                string plain,
                Country country = Country.Cad) {
            var subject = new Subject<List<Deal>>();
            return this.factory.Create().CurrentPrices(plain, country)
                .Select(
                    dealList => {
                        var list = dealList.Data.Plain.List;
                        var deals = list.Select(dealItem => this.mapper.Map<Deal>(dealItem)).ToList();
                        return deals;
                    });
        }
    }
}
