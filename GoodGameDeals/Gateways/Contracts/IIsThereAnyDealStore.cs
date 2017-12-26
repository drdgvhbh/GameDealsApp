namespace GoodGameDeals.Gateways.Contracts {
    using System;
    using System.Threading.Tasks;

    using GoodGameDeals.Data.ApiResponses.IsThereAnyDeal;
    using GoodGameDeals.Data.Localization;

    public interface IIsThereAnyDealStore {
        Task<RecentDealsResponse> RecentDeals(
            int quantity,
            int offset);

        Task<CurrentPricesResponse> CurrentPrices(
            string plain);
    }
}
