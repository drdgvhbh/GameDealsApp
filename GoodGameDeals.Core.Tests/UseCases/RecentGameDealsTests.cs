namespace GoodGameDeals.Core.Tests.UseCases {
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using GoodGameDeals.Core.Contracts.Repositories;
    using GoodGameDeals.Core.Dto;
    using GoodGameDeals.Core.Entities;
    using GoodGameDeals.Core.UseCases;

    using Xunit;

    public class RecentGameDealsTests {
        private readonly Random random;

        public RecentGameDealsTests() {
            this.random = new Random();
        }

        [Fact]
        public async Task
                GetGamesByMostRecentDeals_AnyInput_GamesIsNotNull() {
            var randQuantity = this.random.Next(0, 100);
            var randOffset = this.random.Next(0, 100);
            var mock = this.GetMock(randQuantity, randOffset);
            var request = new RecentGameDealsRequestMessage(
                randQuantity,
                randOffset);
            var interactor = new RequestRecentGameDealsInteractor(mock);
            var result = await interactor.Handle(request);
            Assert.NotNull(result.Games);
            await Task.CompletedTask;
        }


        [Fact]
        public async Task
            GetGamesByMostRecentDeals_AnyInput_ResponseIsNotNull() {
            var randQuantity = this.random.Next(0, 100);
            var randOffset = this.random.Next(0, 100);
            var mock = this.GetMock(randQuantity, randOffset);
            var request = new RecentGameDealsRequestMessage(
                randQuantity,
                randOffset);
            var interactor = new RequestRecentGameDealsInteractor(mock);
            var result = await interactor.Handle(request);
            Assert.NotNull(result);
            await Task.CompletedTask;
        }

        [Theory]
        [InlineData(1)]
        [InlineData(12)]
        [InlineData(100)]
        public async Task
                GetGamesByMostRecentDeals_ListSize_MatchesQuantityInput(
                int quantity) {
            var mock = this.GetMock(quantity);
            var request = new RecentGameDealsRequestMessage(quantity);
            var interactor = new RequestRecentGameDealsInteractor(mock);
            var result = await interactor.Handle(request);
            Assert.True(result.Games.Count == quantity);
            await Task.CompletedTask;
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(-89566843)]
        public async Task
                GetGamesByMostRecentDeals_OffsetIsNegative_ThrowsArgumentOutOfRangeException(
                int offset) {
            var randQuantity = this.random.Next(0, 100);
            var mock = this.GetMock(randQuantity, offset);
            var request =
                new RecentGameDealsRequestMessage(randQuantity, offset);
            var interactor = new RequestRecentGameDealsInteractor(mock);
            await Assert.ThrowsAsync<ArgumentOutOfRangeException>(
                async () => await interactor.Handle(request));
            await Task.CompletedTask;
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(-48274328)]
        public async Task
                GetGamesByMostRecentDeals_QuantityIsNegative_ThrowsArgumentOutOfRangeException(
                int quantity) {
            var mock = this.GetMock(quantity);
            var request = new RecentGameDealsRequestMessage(quantity);
            var interactor = new RequestRecentGameDealsInteractor(mock);
            await Assert.ThrowsAsync<ArgumentOutOfRangeException>(
                async () => await interactor.Handle(request));
            await Task.CompletedTask;
        }

        private StubIGamesRepository GetMock(int quantity = 0, int offset = 0) {
            var gamesRepoStub = new StubIGamesRepository();
            var list = new List<Game>();
            for (var i = 0; i < quantity; i++) {
                list.Add(
                    new Game(string.Empty, string.Empty, 0, new List<Deal>()));
            }

            gamesRepoStub.GetGamesByMostRecentDeals(
                (count, numOffset) => Task.FromResult((IEnumerable<Game>)list));
            return gamesRepoStub;
        }
    }
}