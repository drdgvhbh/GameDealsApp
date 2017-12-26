namespace GoodGameDeals.Gateways.Contracts {
    using System.Threading.Tasks;

    using Windows.UI.Xaml.Media.Imaging;

    using GoodGameDeals.Data.ApiResponses.Steam;

    public interface ISteamStore {
        Task<BitmapImage> GameLogo(string title);

        Task<GetAppListResponse> AppList();

        long GetAppId(string title);

        Task Initialize();
    }
}
