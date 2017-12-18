namespace GoodGameDeals.ViewModels.MainPage {
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;

    using Windows.ApplicationModel.Resources;

    using GoodGameDeals.Models;

    using Windows.UI.Xaml.Navigation;

    using GoodGameDeals.Collections.ObjectModel;
    using GoodGameDeals.Models.ITAD;
    using GoodGameDeals.Services.HttpServices;

    using Template10.Mvvm;
    using Microsoft.Toolkit.Uwp.UI;

    public class GameDealsViewModel : ViewModelBase {
        private readonly IDictionary<string, long> appIdDictionary;

        private bool isFirstLoad;

        private string itadApiKey;

        private IsThereAnyDealService itadService;

        private SteamService steamService;

        private ObservableCollection<Game> gamesList;

        public GameDealsViewModel(
                IsThereAnyDealService itadService,
                SteamService steamService) {
            this.isFirstLoad = true;
            this.itadService = itadService;
            this.steamService = steamService;
            this.gamesList = new ObservableCollection<Game>();
            this.GamesCollectionView = new AdvancedCollectionView(this.gamesList, true);
            this.appIdDictionary = new Dictionary<string, long>();
            this.gameCache = new HashSet<RecentDealsResponse.List>();
            this.itadApiKey = ResourceLoader.GetForCurrentView("apiKeys")
                .GetString("ITAD");

            this.GamesCollectionView.SortDescriptions.Add(
                new SortDescription("Id", SortDirection.Descending));
        }

        public HashSet<RecentDealsResponse.List> gameCache;

        public AdvancedCollectionView GamesCollectionView { get; }



        public override async Task OnNavigatedToAsync(
                object parameter,
                NavigationMode mode,
                IDictionary<string, object> state) {
            if (mode == NavigationMode.New || mode == NavigationMode.Refresh) {
                await this.OnFirstLoad();
                await this.PopulateGamesList();
            }
            else {
                this.GamesCollectionView.Refresh();
            }
            await Task.CompletedTask;
        }

        public async Task PopulateGamesList() {
/*            using (this.GamesCollectionView.DeferRefresh()) {*/
                this.gamesList.Clear();
                var recentDeals = await this.itadService.RecentDeals();
                foreach (var game in recentDeals.Data.List) {
                    await this.AddGame(game);
                }

                await Task.CompletedTask;
/*            }*/
        }

        public async Task AddGame(RecentDealsResponse.List game) {
            if (this.gameCache.Contains(game)) {
                return;
            }

            this.gameCache.Add(game);
            long id = -1;
            try {
                id = this.appIdDictionary[game.Title];
            }
            catch (KeyNotFoundException) {
            }
            var image = await this.steamService.GameLogo(id);
            var deals =
                await this.itadService.CurrentPrices(game.Plain);
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
            var subtitle = gameHeader.Length == 2
                               ? gameHeader[1].Trim()
                               : string.Empty;
            this.gamesList.Add(
                new Game(
                    game.Added,
                    gameHeader[0].Trim(),
                    subtitle,
                    image,
                    dealsCollection));

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