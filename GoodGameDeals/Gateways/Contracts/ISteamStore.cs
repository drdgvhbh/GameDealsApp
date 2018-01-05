namespace GoodGameDeals.Gateways.Contracts {
    using System;
    using System.Threading.Tasks;

    using Windows.UI.Xaml.Media.Imaging;

    using GoodGameDeals.Data.ApiResponses.Steam;

    public interface ISteamStore {
        Task<Uri> GameLogo(string title);

        Task<GetAppListResponse> AppList();

        long GetAppId(string title);

        Task Initialize();
    }
}
