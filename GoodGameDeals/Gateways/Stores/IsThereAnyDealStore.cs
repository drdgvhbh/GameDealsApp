namespace GoodGameDeals.Gateways.Stores {
    using System.Threading.Tasks;

    using GoodGameDeals.Data.ApiResponses.IsThereAnyDeal;
    using GoodGameDeals.Gateways.Contracts;
    public class IsThereAnyDealStore : IIsThereAnyDealStore {
        public Task<RecentDealsResponse> RecentDeals(int quantity, int offset) {
            return null;
        }

        public Task<CurrentPricesResponse> CurrentPrices(string plain) {
            return null;
        }
    }
}
