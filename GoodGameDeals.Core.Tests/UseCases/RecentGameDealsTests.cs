namespace GoodGameDeals.Core.Tests.UseCases {
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Windows.ApplicationModel.Core;
    using Windows.UI.Core;
    using Windows.UI.Xaml.Media.Imaging;

    using GoodGameDeals.Core.Contracts.Repositories;
    using GoodGameDeals.Core.Dto;
    using GoodGameDeals.Core.Entities;
    using GoodGameDeals.Core.UseCases;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class RecentGameDealsTests {
        private readonly Random random;

        public RecentGameDealsTests() {
            this.random = new Random();
        }

        [TestMethod]
        public async Task
                GetGamesByMostRecentDeals_AnyValidInput_GamesIsNotNull() {
            var randQuantity = this.random.Next(0, 100);
            var randOffset = this.random.Next(0, 100);
            var mock = await this.GetMock(randQuantity, randOffset);
            var request = new RecentGameDealsRequestMessage(
                randQuantity,
                randOffset);
            var interactor = new RequestRecentGameDealsInteractor(mock);
            var result = await interactor.Handle(request);
            Assert.IsNotNull(result.Games);
            await Task.CompletedTask;
        }


        [TestMethod]
        public async Task
            GetGamesByMostRecentDeals_AnyValidInput_ResponseIsNotNull() {
            var randQuantity = this.random.Next(0, 100);
            var randOffset = this.random.Next(0, 100);
            var mock = await this.GetMock(randQuantity, randOffset);
            var request = new RecentGameDealsRequestMessage(
                randQuantity,
                randOffset);
            var interactor = new RequestRecentGameDealsInteractor(mock);
            var result = await interactor.Handle(request);
            Assert.IsNotNull(result);
            await Task.CompletedTask;
        }

        [DataTestMethod]
        [DataRow(1)]
        [DataRow(12)]
        [DataRow(100)]
        public async Task
                GetGamesByMostRecentDeals_ListSize_LessThanOrEqualQuantityInput(
                int quantity) {
            var mock = await this.GetMock(quantity);
            var request = new RecentGameDealsRequestMessage(quantity, this.random.Next());
            var interactor = new RequestRecentGameDealsInteractor(mock);
            var result = await interactor.Handle(request);
            Assert.IsTrue(result.Games.Count <= quantity);
            await Task.CompletedTask;
        }

        [DataTestMethod]
        [DataRow(-1)]
        [DataRow(-89566843)]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public async Task
                GetGamesByMostRecentDeals_OffsetIsNegative_ThrowsArgumentOutOfRangeException(
                int offset) {
            var randQuantity = this.random.Next(0, 100);
            var mock = await this.GetMock(randQuantity, offset);
            var request =
                new RecentGameDealsRequestMessage(randQuantity, offset);
            var interactor = new RequestRecentGameDealsInteractor(mock);
            await interactor.Handle(request);
            await Task.CompletedTask;
        }

        [DataTestMethod]
        [DataRow(-1)]
        [DataRow(-48274328)]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public async Task
                GetGamesByMostRecentDeals_QuantityIsNegative_ThrowsArgumentOutOfRangeException(
                int quantity) {
            var mock = await this.GetMock(quantity);
            var request = new RecentGameDealsRequestMessage(quantity);
            var interactor = new RequestRecentGameDealsInteractor(mock);
            await interactor.Handle(request);
            await Task.CompletedTask;
        }

        private async Task<StubIGamesRepository> GetMock(int quantity = 0, int offset = 0) {
            var gamesRepoStub = new StubIGamesRepository();
            var list = new List<Game>();
            for (var i = 0; i < quantity; i++) {
                var dispatcher = CoreApplication.CreateNewView().Dispatcher;
                 await dispatcher.RunAsync(
                    CoreDispatcherPriority.High,
                    () =>
                        {
                            list.Add(
                                new Game(
                                    string.Empty,
                                    string.Empty,
                                    new Uri(string.Empty),
                                    new List<Deal>()));
                        });
            }

            gamesRepoStub.GetGamesByMostRecentDeals(
                async (count, numOffset) =>
                    await Task.FromResult((IEnumerable<Game>)list));
            return gamesRepoStub;
        }
    }
}