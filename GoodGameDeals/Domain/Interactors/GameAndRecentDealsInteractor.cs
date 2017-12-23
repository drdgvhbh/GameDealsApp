namespace GoodGameDeals.Domain.Interactors {
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Reactive.Linq;
    using System.Reactive.Subjects;

    using GoodGameDeals.Data.Entity;
    using GoodGameDeals.Data.Repositories;

    using Reactive.Bindings;

    public class GameAndRecentDealsInteractor : AbstractInteractor<Game,
            GameAndRecentDealsInteractor.Params> {
        private readonly IIsThereAnyDealRepository dealRepository;

        private readonly ISteamRepository steamRepository;

        public GameAndRecentDealsInteractor(
                IIsThereAnyDealRepository dealRepository,
                ISteamRepository steamRepository) {
            this.dealRepository = dealRepository;
            this.steamRepository = steamRepository;
        }

        public override IObservable<Game> UseCaseObservable(Params parameters) {
            var subject = new Subject<Game>();
            this.dealRepository.RecentDeals(
                    parameters.country,
                    parameters.offset,
                    parameters.limit).Subscribe(
                deals => {
                    foreach (var deal in deals) {
                        var game = new Game(deal.Added, deal.Title);
                        var canReturn = new ReactiveProperty<int> { Value = 2 };
                        this.dealRepository.GameCurrentPrices(
                            deal.Plain,
                            parameters.country).Subscribe(
                            gameDeals => {
                                foreach (var gameDeal in gameDeals) {
                                    gameDeal.Plain = deal.Plain;
                                    gameDeal.Title = deal.Title;
                                }
                                game.DealsList = gameDeals;
                                canReturn.Value--;
                            });
                        this.steamRepository
                            .GameLogo(deal.Title).Subscribe(
                                image => {
                                    game.GameImage = image;
                                    canReturn.Value--;
                                });
                        canReturn.Where(x => x == 0).Subscribe(
                            _ => {
                                subject.OnNext(game);
                            });
                    }
                });

            return subject;
        }

        public class Params {
            internal readonly Country country;

            internal readonly int offset;

            internal readonly int limit;

            public Params(
                    Country country = Country.Cad,
                    int limit = 50,
                    int offset = 0) {
                this.country = country;
                this.limit = 50;
                this.offset = 0;
            }
        }
    }
}
