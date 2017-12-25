namespace GoodGameDeals.Gateways.Repositories {
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using GoodGameDeals.Core.Contracts.Repositories;
    using GoodGameDeals.Core.Entities;
    using GoodGameDeals.Gateways.Stores;

    using IIsThereAnyDealStore = GoodGameDeals.Gateways.Contracts.IIsThereAnyDealStore;

    public class GameRepository : IGamesRepository {
        private ISteamStore steamStore;

        private IIsThereAnyDealStore dealStore;

        public GameRepository(
                ISteamStore steamStore,
                IIsThereAnyDealStore dealStore) {
            this.dealStore = dealStore
                             ?? throw new ArgumentNullException(
                                 nameof(dealStore));
            this.steamStore = steamStore
                              ?? throw new ArgumentNullException(
                                  nameof(steamStore));
        }

        public Task<IEnumerable<Game>> GetGamesByMostRecentDeals(int quantity, int offset) {
            return null;
        }
    }
}
