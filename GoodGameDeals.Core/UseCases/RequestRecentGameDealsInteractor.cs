namespace GoodGameDeals.Core.UseCases {
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reactive.Linq;
    using System.Reactive.Subjects;
    using System.Threading.Tasks;

    using Windows.System.Threading;

    using GoodGameDeals.Core.Contracts;
    using GoodGameDeals.Core.Contracts.Repositories;
    using GoodGameDeals.Core.Dto;
    using GoodGameDeals.Core.Entities;

    public class RequestRecentGameDealsInteractor : IRequestHandler<
        RecentGameDealsRequestMessage, RecentGameDealsResponseMessage> {
        private readonly IGamesRepository gamesRepository;

        public RequestRecentGameDealsInteractor(
            IGamesRepository gamesRepo) {
            this.gamesRepository = gamesRepo;
        }

        public async Task<RecentGameDealsResponseMessage> Handle(
                RecentGameDealsRequestMessage request) {
            if (request.Quantity < 0) {
                throw new ArgumentOutOfRangeException(
                    nameof(request.Quantity),
                    nameof(request.Quantity) + " must be positive.");
            }
            if (request.Offset < 0) {
                throw new ArgumentOutOfRangeException(
                    nameof(request.Offset),
                    nameof(request.Offset) + " must be positive.");
            }

            var games = await Task.Run(
                () => this.gamesRepository.GetGamesByMostRecentDeals(
                    request.Quantity,
                    request.Offset));
            var list = games.ToList();
            return new RecentGameDealsResponseMessage(true, list);
        }
    }
}