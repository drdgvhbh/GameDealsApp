namespace GoodGameDeals.Gateways.Contracts {
    using System.Threading.Tasks;

    using GoodGameDeals.Data.ApiResponses.IGDB;

    public interface IIGDBStore {
        Task<SearchForGameResponse[]> SearchForGame(string searchQuery);

        Task<CoverResponse[]> CoverImage(long id);
    }
}