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

    using Windows.UI.Xaml.Navigation;
    using Windows.Web.Http;

    using GoodGameDeals.Collections.ObjectModel;
    using GoodGameDeals.Models.ITAD;
    using GoodGameDeals.Services.HttpServices;

    using Microsoft.EntityFrameworkCore.ChangeTracking;

    using Template10.Common;
    using Template10.Controls;
    using Template10.Mvvm;

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

        private IsThereAnyDealService itadService;

        private SteamService steamService;

        public GameDealsViewModel(
                IsThereAnyDealService itadService,
                SteamService steamService) {
            this.isFirstLoad = true;
            this.itadService = itadService;
            this.steamService = steamService;
            this.GamesList = new ObservableCollection<Game>();
            this.appIdDictionary = new Dictionary<string, long>();
            this.client = new HttpClient();
            this.gameCache = new HashSet<RecentDealsResponse.List>();
            this.itadApiKey = ResourceLoader.GetForCurrentView("apiKeys")
                .GetString("ITAD");
            ObservableLinkedList<int> derp = new ObservableLinkedList<int>();
        }

        public HashSet<RecentDealsResponse.List> gameCache;

        public ObservableCollection<Game> GamesList { get; }

        public override async Task OnNavigatedToAsync(
                object parameter,
                NavigationMode mode,
                IDictionary<string, object> state) {
            if (mode == NavigationMode.New || mode == NavigationMode.Refresh) {
                await this.OnFirstLoad();
            }
            await this.PopulateGamesList();
            await Task.CompletedTask;
        }

        public async Task PopulateGamesList() {
            var recentDeals = await this.itadService.RecentDeals();
            foreach (var game in recentDeals.Data.List) {
                if (this.gameCache.Contains(game)) {
                    continue;
                }

                this.gameCache.Add(game);
                long id = -1;
                try {
                    id = this.appIdDictionary[game.Title];
                }
                catch (KeyNotFoundException) {
                }
                var image = await
                    this.steamService.GameLogo(id);
                var deals = await this.itadService.CurrentPrices(game.Plain);
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
            var steamApps = await this.steamService.AppList();

            foreach (var app in steamApps.Applist.Apps) {
                if (!this.appIdDictionary.ContainsKey(app.Name)) {
                    this.appIdDictionary.Add(app.Name, app.Appid);
                }
            }

            this.isFirstLoad = false;
            await Task.CompletedTask;
        }
    }
}