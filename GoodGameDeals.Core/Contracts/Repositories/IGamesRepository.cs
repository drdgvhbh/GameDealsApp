namespace GoodGameDeals.Core.Contracts.Repositories {
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using GoodGameDeals.Core.Entities;

    public interface IGamesRepository {
        Task<IEnumerable<Game>> GetGamesByMostRecentDeals(int quantity, int offset);
    }
}
