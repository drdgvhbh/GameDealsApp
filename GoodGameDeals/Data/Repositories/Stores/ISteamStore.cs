namespace GoodGameDeals.Data.Repositories.Stores {
    using System;
    using System.Reactive;

    using Windows.UI.Xaml.Media.Imaging;

    using GoodGameDeals.Data.Entity.Responses.Steam;

    public interface ISteamStore {
        IObservable<BitmapImage> GameLogo(string title);

        IObservable<GetAppListResponse> AppList();

        IObservable<Unit> Initialize();
    }
}
