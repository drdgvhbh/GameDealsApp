namespace GoodGameDeals.Gateways.Stores {
    using System.Threading.Tasks;

    using GoodGameDeals.Gateways.Contracts;

    using Windows.UI.Xaml.Media.Imaging;

    using GoodGameDeals.Data.ApiResponses.Steam;

    public class SteamStore : ISteamStore {
        public Task<BitmapImage> GameLogo(string title) {
            return null;
        }

        public Task<GetAppListResponse> AppList() {
            return null;
        }

        public long GetAppId(string title) {
            return 0;
        }
    }
}
