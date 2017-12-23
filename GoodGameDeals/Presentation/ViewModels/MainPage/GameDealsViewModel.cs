namespace GoodGameDeals.Presentation.ViewModels.MainPage {
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Reactive;
    using System.Reactive.Linq;
    using System.Reactive.Subjects;
    using System.Threading.Tasks;

    using Windows.UI.Xaml.Navigation;

    using AutoMapper;

    using GoodGameDeals.Domain.Interactors;
    using GoodGameDeals.Models;

    using Microsoft.Toolkit.Uwp.UI;

    using Template10.Mvvm;

    public class GameDealsViewModel : ViewModelBase {
        private readonly IDictionary<string, long> appIdDictionary;

        private bool isFirstLoad;

        private readonly ObservableCollection<GameModel> gamesList;

        private readonly GameAndRecentDealsInteractor gameAndRecentDealsInteractor;

        private IMapper mapper;

        public GameDealsViewModel(
                IMapper mapper,
                GameAndRecentDealsInteractor gameAndRecentDealsInteractor) {
            this.isFirstLoad = true;

            this.gamesList = new ObservableCollection<GameModel>();
            this.GamesCollectionView = new AdvancedCollectionView(this.gamesList, true);
            this.gameAndRecentDealsInteractor = gameAndRecentDealsInteractor;
            this.mapper = mapper;

            this.GamesCollectionView.SortDescriptions.Add(
                new SortDescription("Id", SortDirection.Descending));
        }

        public AdvancedCollectionView GamesCollectionView { get; }

        public override async Task OnNavigatedToAsync(
                object parameter,
                NavigationMode mode,
                IDictionary<string, object> state) {
            if (mode == NavigationMode.New || mode == NavigationMode.Refresh) {
                await this.PopulateGamesList().FirstAsync();
            }
            else {
                this.GamesCollectionView.Refresh();
            }
            await Task.CompletedTask;
        }

        public IObservable<Unit> PopulateGamesList() {
            var subject = new Subject<Unit>();
            this.GamesCollectionView.Clear();
            this.gameAndRecentDealsInteractor
                .UseCaseObservable(new GameAndRecentDealsInteractor.Params())
                .Subscribe(
                    game => {
                        var squid = game;
                        System.Diagnostics.Debug.WriteLine(squid.GameTitle);
                        this.gamesList.Add(this.mapper.Map<GameModel>(game));
                    },
                    () => {
                        subject.OnNext(new Unit());
                    });
            return subject;
        }

    }
}