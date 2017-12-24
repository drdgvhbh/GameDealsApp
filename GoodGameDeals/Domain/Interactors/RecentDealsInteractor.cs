namespace GoodGameDeals.Domain.Interactors {
    using System;
    using System.Collections.Generic;

    using GoodGameDeals.Data.Entity;
    using GoodGameDeals.Data.Repositories;

    public class RecentDealsInteractor : AbstractInteractor<List<Deal>, RecentDealsInteractor.Params> {
        private readonly IIsThereAnyDealRepository repository;

        public RecentDealsInteractor(IIsThereAnyDealRepository repository) {
            this.repository = repository;
        }

        public override IObservable<List<Deal>> UseCaseObservable(Params parameters) {
            return this.repository.RecentDeals(parameters.Country, parameters.Offset, parameters.Limit);
        }

        public class Params {
            internal readonly Country Country;

            internal readonly int Offset;

            internal readonly int Limit;

            public Params(
                Country country = Country.Cad,
                int limit = 25,
                int offset = 0) {
                this.Country = country;
                this.Limit = limit;
                this.Offset = offset;
            }
        }
    }
}
