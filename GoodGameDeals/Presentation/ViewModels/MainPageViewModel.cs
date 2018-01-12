namespace GoodGameDeals.Presentation.ViewModels {
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows.Input;

    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;

    using AutoMapper;

    using GoodGameDeals.Core.Dto;
    using GoodGameDeals.Core.UseCases;
    using GoodGameDeals.Messages;
    using GoodGameDeals.Models;
    using GoodGameDeals.Views;

    using MetroLog;

    using Microsoft.Toolkit.Uwp.UI;

    using Template10.Mvvm;
    using Template10.Services.NavigationService;
    using Template10.Services.SerializationService;

    using Windows.UI.Xaml.Navigation;

    public class MainPageViewModel : ViewModelBase {
        /// <summary>
        ///     The log.
        /// </summary>
        private static readonly ILogger Log = LogManagerFactory
            .DefaultLogManager.GetLogger<MainPageViewModel>();

        private AccessToken accessToken;

        private bool isFirstLoad;

        private string selectedGameTitle;

        private readonly RequestRecentGameDealsInteractor recentGameDealsInteractor;

        private readonly IMapper mapper;

        public MainPageViewModel(
                IMapper mapper,
                ObservableCollection<GameModel> gamesList,
                RequestRecentGameDealsInteractor recentGameDealsInteractor) {
            this.isFirstLoad = true;

            this.GamesCollectionView =
                new AdvancedCollectionView(gamesList, true);

            var dealsList = gamesList.Count > 0
                                ? gamesList[0].DealsList
                                : new ObservableCollection<DealModel>();
            this.SelectedDealsCollectionView = new AdvancedCollectionView(
                dealsList,
                true);
            this.recentGameDealsInteractor = recentGameDealsInteractor;
            this.mapper = mapper;

            this.GamesCollectionView.SortDescriptions.Add(
                new SortDescription("Id", SortDirection.Descending));
            System.Diagnostics.Debug.WriteLine("TRASH");
            System.Diagnostics.Debug.WriteLine(this.SelectedDealsCollectionView.Source.GetHashCode());
        }

        public AdvancedCollectionView GamesCollectionView { get; }

        public AdvancedCollectionView SelectedDealsCollectionView { get; }

        public string SelectedGameName {
            get {
                return this.selectedGameTitle;
            }
            private set {
                this.Set(ref this.selectedGameTitle, value);
                //    this.Set
            }
        }

        public void GotoAbout() =>
            this.NavigationService.Navigate(typeof(SettingsPage), 2);

        public void GotoPrivacy() =>
            this.NavigationService.Navigate(typeof(SettingsPage), 1);

        public void GotoSettings() =>
            this.NavigationService.Navigate(typeof(SettingsPage), 0);

        public void OnDealButtonPressed(object sender, RoutedEventArgs e) {
            Log.Debug("Hello world!");
            var gameModel = (e as ItemClickEventArgs)?.ClickedItem as GameModel;
            this.SelectedGameName = gameModel?.GameTitle;
            this.SelectedDealsCollectionView.Clear();
            System.Diagnostics.Debug.WriteLine(this.SelectedDealsCollectionView.Source.GetHashCode());
            if (gameModel?.DealsList != null) {
                foreach (var deal in gameModel?.DealsList) {
                    this.SelectedDealsCollectionView.Add(deal);
                }
            }
        }

        /*        /// <summary>
                ///     The nav to details page.
                /// </summary>
                public void NavToDetailsPage() =>
                    this.NavigationService.Navigate(typeof(DetailPage), this.Value);*/

        public override async Task OnNavigatedFromAsync(
                IDictionary<string, object> suspensionState,
                bool suspending) {
            if (suspending) {
/*                suspensionState[nameof(this.Value)] = this.Value;*/
            }

            await Task.CompletedTask;
        }

        public override async Task OnNavigatedToAsync(
            object parameter,
            NavigationMode mode,
            IDictionary<string, object> suspensionState) {
            if (suspensionState.Any()) {
/*                this.Value = suspensionState[nameof(this.Value)]?.ToString();*/
            }

            if (parameter is string param) {
                SerializationService.Json.TryDeserialize(
                    param,
                    out this.accessToken);
                Log.Info("Access Token set to {0}", this.accessToken);
            }

            if (mode == NavigationMode.New || mode == NavigationMode.Refresh) {
                await this.PopulateGamesList();
            }
            else {
                this.GamesCollectionView.Refresh();
            }
            await Task.CompletedTask;
        }

        public override async Task OnNavigatingFromAsync(
                NavigatingEventArgs args) {
            args.Cancel = false;
            await Task.CompletedTask;
        }

        public async Task PopulateGamesList() {
            this.GamesCollectionView.Clear();
            var response =
                    await this.recentGameDealsInteractor.Handle(
                        new RecentGameDealsRequestMessage(25));
            foreach (var game in response.Games) {
                this.GamesCollectionView.Add(this.mapper.Map<GameModel>(game));
            }
        }

        private void AddGameToView(GameModel game) {
            try {
                this.GamesCollectionView.Add(game);
            }
            catch (InvalidOperationException e) {
                Log.Warn(
                    "Encountered an error adding an element to the Game "
                        + "Collection View",
                    e);
                this.GamesCollectionView.Refresh();
            }
        }

    }
}