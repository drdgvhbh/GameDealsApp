namespace GoodGameDeals.Domain.Interactors {
    using System;
    using System.Collections.Generic;

    using GoodGameDeals.Data.Repositories;

    public class RecentDealsInteractor : AbstractInteractor<List<Deal>, object> {
        private readonly IIsThereAnyDealRepository repository;

        public RecentDealsInteractor(IIsThereAnyDealRepository repository) {
            this.repository = repository;
        }

        public override IObservable<List<Deal>> UseCaseObservable(object parameters) {
            return this.repository.RecentDeals();
        }
    }
}
