namespace GoodGameDeals.ViewModels.MainPage {
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Reactive.Windows.Foundation;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;

    using Windows.ApplicationModel.Resources;
    using Windows.UI.Xaml.Media.Imaging;

    using GoodGameDeals.Models;
    using GoodGameDeals.Models.Steam;

    using MetroLog;

    using Template10.Mvvm;

    using Windows.UI.Xaml.Navigation;
    using Windows.Web.Http;

    using GoodGameDeals.Models.ITAD;

    public class GameDealsViewModel : ViewModelBase {
        /// <summary>
        ///     The logger for this class.
        /// </summary>
        private static readonly ILogger Log = LogManagerFactory
            .DefaultLogManager.GetLogger<GameDealsViewModel>();

        private readonly IDictionary<string, long> appIdDictionary;

        private bool isFirstLoad;

        private HttpClient client;

        private string itadApiKey;

        public GameDealsViewModel() {
            this.isFirstLoad = true;
            this.GamesList = new ObservableCollection<Game>();
            this.appIdDictionary = new Dictionary<string, long>();
            this.client = new HttpClient();
            this.itadApiKey = ResourceLoader.GetForCurrentView("apiKeys")
                .GetString("ITAD");
        }

        public ObservableCollection<Game> GamesList { get; }

        public override async Task OnNavigatedToAsync(
                object parameter,
                NavigationMode mode,
                IDictionary<string, object> state) {
            await this.OnFirstLoad();
            await this.PopulateGamesList();
            await Task.CompletedTask;
        }

        public async Task PopulateGamesList() {
            this.GamesList.Clear();
            var uriBuilder = new UriBuilder {
                Scheme = "https",
                Host = "api.isthereanydeal.com",
                Path = "v01/deals/list/ca",
                Query = "key=" + this.itadApiKey + "&country=CAD" + "&offset=0"
                        + "&limit=50"
            };
            var response = await this.client.GetAsync(uriBuilder.Uri);
            var recentDeals =
                RecentDealsResponse.FromJson(response.Content.ToString());
            foreach (var game in recentDeals.Data.List) {
                var image = this.GetGameBanner(game.Title);

                // Get all the deals associated with this game
                var query = new StringBuilder();
                query.AppendFormat(
                    "key={0}&plains={1}&country=CAD",
                    this.itadApiKey,
                    game.Plain);
                uriBuilder = new UriBuilder {
                    Scheme = "https",
                    Host = "api.isthereanydeal.com",
                    Path = "v01/game/prices/ca",
                    Query = query.ToString()
                };
                response = await this.client.GetAsync(uriBuilder.Uri);
                var deals =
                    CurrentPricesResponse.FromJson(response.Content.ToString());
                var dealsCollection = new ObservableCollection<Deal>();

                // Only take the top 3 deals
                var counter = 1;
                foreach (var deal in deals.Data.Plain.List) {
                    if (counter > 3) {
                        break;
                    }
                    if (deal.PriceCut <= 0) {
                        continue;
                    }
                    dealsCollection.Add(
                        new Deal(
                            deal.Url,
                            deal.PriceCut,
                            deal.PriceOld,
                            deal.PriceNew,
                            deal.Shop.Name));
                    counter++;
                }

                var regex = new Regex(@":|-");
                var gameHeader = regex.Split(game.Title, 2);
                this.GamesList.Add(
                    new Game(
                        gameHeader[0].Trim(),
                        gameHeader.Length == 2 ? gameHeader[1].Trim() : string.Empty,
                        image,
                        dealsCollection));
            }

            await Task.CompletedTask;
        }

        private BitmapImage GetGameBanner(string gameName) {
            var image = new BitmapImage(
                new Uri("ms-appx:///Assets/NoPreviewAvaliable.png"));
            try {
                var imagePath =
                    "steam/apps/" + this.appIdDictionary[gameName]
                    + "/header.jpg";
                var uriBuilder = new UriBuilder {
                    Scheme = "http",
                    Host = "cdn.akamai.steamstatic.com",
                    Path = imagePath
                };
                image = new BitmapImage(
                    new Uri(uriBuilder.ToString(), UriKind.Absolute));
            }
            catch (KeyNotFoundException) {
                Log.Info(
                    "Image not found for game \"{0}\"",
                    gameName);
            }

            return image;
        }

        /// <summary>
        ///     Initializes this <see cref="GameDealsViewModel" /> when it is
        ///     first navigated to.
        /// </summary>
        /// <returns>
        ///     The completed <see cref="Task" />.
        /// </returns>
        private async Task OnFirstLoad() {
            if (!this.isFirstLoad) {
                await Task.CompletedTask;
            }

            var uriBuilder = new UriBuilder
            {
                Scheme = "https",
                Host = "api.steampowered.com",
                Path = "ISteamApps/GetAppList/v0002"
            };
            var response = await client.GetAsync(uriBuilder.Uri);
            var steamApps =
                GetAppListResponse.FromJson(response.Content.ToString());
            foreach (var app in steamApps.Applist.Apps) {
                if (!this.appIdDictionary.ContainsKey(app.Name)) {
                    this.appIdDictionary.Add(app.Name, app.Appid);
                }
            }

            this.isFirstLoad = false;
            Log.Info("[Success] First Load");
            await Task.CompletedTask;
        }
    }
}