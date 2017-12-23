namespace GoodGameDeals.Data.Repositories {
    using System;

    using Windows.UI.Xaml.Media.Imaging;

    using GoodGameDeals.Data.Entity.Responses.Steam;
    using GoodGameDeals.Data.Repositories.Stores;

    public interface ISteamRepository {
        IObservable<BitmapImage> GameLogo(string title);

        IObservable<GetAppListResponse> AppList();
    }
}
