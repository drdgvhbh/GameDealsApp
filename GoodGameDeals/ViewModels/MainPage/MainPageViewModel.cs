namespace GoodGameDeals.ViewModels.MainPage {
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Threading.Tasks;

    using GoodGameDeals.Messages;
    using GoodGameDeals.Models;
    using GoodGameDeals.Models.ITAD;
    using GoodGameDeals.Models.Steam;
    using GoodGameDeals.Views;

    using MetroLog;

    using Template10.Mvvm;
    using Template10.Services.NavigationService;
    using Template10.Services.SerializationService;

    using Windows.ApplicationModel;
    using Windows.ApplicationModel.Resources;
    using Windows.UI.Xaml.Media.Imaging;
    using Windows.UI.Xaml.Navigation;

    public class MainPageViewModel : ViewModelBase {
        /// <summary>
        ///     The log.
        /// </summary>
        private static readonly ILogger Log = LogManagerFactory
            .DefaultLogManager.GetLogger<MainPageViewModel>();

        private string _Value = "Gas";

        private AccessToken accessToken;

        public GameDealsViewModel GameDealsViewModel { get; }

        public MainPageViewModel() {
            if (DesignMode.DesignModeEnabled) {
                this.Value = "Designtime value";
            }
            this.GameDealsViewModel = new GameDealsViewModel();
            this.accessToken = new AccessToken();
        }

        public string Value {
            get {
                return this._Value;
            }

            set {
                this.Set(ref this._Value, value);
                this.RaisePropertyChanged();
            }
        }

        public void GotoAbout() =>
            this.NavigationService.Navigate(typeof(SettingsPage), 2);

        public void GotoPrivacy() =>
            this.NavigationService.Navigate(typeof(SettingsPage), 1);

        public void GotoSettings() =>
            this.NavigationService.Navigate(typeof(SettingsPage), 0);

        /// <summary>
        ///     The nav to details page.
        /// </summary>
        public void NavToDetailsPage() =>
            this.NavigationService.Navigate(typeof(DetailPage), this.Value);

        public override async Task OnNavigatedFromAsync(
            IDictionary<string, object> suspensionState,
            bool suspending) {
            if (suspending) {
                suspensionState[nameof(this.Value)] = this.Value;
            }

            await Task.CompletedTask;
        }

        public override async Task OnNavigatedToAsync(
            object parameter,
            NavigationMode mode,
            IDictionary<string, object> suspensionState) {
            if (suspensionState.Any()) {
                this.Value = suspensionState[nameof(this.Value)]?.ToString();
            }

            if (parameter is string param) {
                SerializationService.Json.TryDeserialize(
                    param,
                    out this.accessToken);
                Log.Info("Access Token set to {0}", this.accessToken);
            }

            await this.GameDealsViewModel.OnNavigatedToAsync(
                parameter,
                mode,
                suspensionState);
/*            this.DealsList.Clear();
            await this.PopulateDealsList();*/
            await Task.CompletedTask;
        }

        public override async Task OnNavigatingFromAsync(
            NavigatingEventArgs args) {
            args.Cancel = false;
            await Task.CompletedTask;
        }

   /*     public async Task PopulateDealsList() {
            try {
                var resources = ResourceLoader.GetForCurrentView("apiKeys");
                var itadKey = resources.GetString("ITAD");
                var uriBuilder = new UriBuilder
                {
                    Scheme = "https",
                    Host = "api.isthereanydeal.com",
                    Path = "v01/deals/list/ca",
                    Query = "key=" + itadKey + "&country=CAD" + "&offset=0"
                            + "&limit=2000"
                };
                var client = new Windows.Web.Http.HttpClient();
                var response = await client.GetAsync(uriBuilder.Uri);
                var recentDeals =
                    RecentDealsResponse.FromJson(response.Content.ToString());
                uriBuilder = new UriBuilder
                {
                    Scheme = "https",
                    Host = "api.steampowered.com",
                    Path = "ISteamApps/GetAppList/v0002"
                };
                response = await client.GetAsync(uriBuilder.Uri);
                var steamApps =
                    GetAppListResponse.FromJson(response.Content.ToString());
                Dictionary<string, long> appIdMap =
                    new Dictionary<string, long>();
                foreach (var app in steamApps.Applist.Apps) {
                    if (!appIdMap.ContainsKey(app.Name)) {
                        appIdMap.Add(app.Name, app.Appid);
                    }
                }
                foreach (var game in recentDeals.Data.List) {
                    var image = new BitmapImage(
                        new Uri("ms-appx:///Assets/NoPreviewAvaliable.png"));
                    try {
                        var imagePath =
                            "steam/apps/" + appIdMap[game.Title]
                            + "/header.jpg";
                        uriBuilder = new UriBuilder
                        {
                            Scheme = "http",
                            Host = "cdn.akamai.steamstatic.com",
                            Path = imagePath
                        };
                        image = new BitmapImage(
                            new Uri(uriBuilder.ToString(), UriKind.Absolute));

                    }
                    catch (KeyNotFoundException e) {
                        Log.Debug(e.Message);
                    }


                    this.DealsList.Add(
                        new Deal(
                            game.Title,
                            "$" + game.PriceNew,
                            "$" + game.PriceOld,
                            "-" + game.PriceCut + "%",
                            image));
                }

            } catch (System.Runtime.InteropServices.COMException e) {
                Log.Debug("No Internet");
            }
        }*/
    }
}