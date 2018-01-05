namespace GoodGameDeals.Test.Repositories {
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Threading.Tasks;

    using Windows.ApplicationModel.Core;
    using Windows.UI.Core;
    using Windows.UI.Xaml.Media.Imaging;

    using GoodGameDeals.Core.Entities;
    using GoodGameDeals.Data.ApiResponses.IsThereAnyDeal;
    using GoodGameDeals.Data.ApiResponses.Steam;
    using GoodGameDeals.Gateways.Contracts;
    using GoodGameDeals.Gateways.Repositories;
    using GoodGameDeals.Gateways.Stores;
    using GoodGameDeals.Threading;
    using GoodGameDeals.Threading.Tasks;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class GameRepositoryTests {
        private readonly Random random;

        private ITaskDelay taskDelay;

        public GameRepositoryTests() {
            this.random = new Random();
            this.taskDelay = new TestTaskDelay(TimeSpan.Zero);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void
            GameRepository_SteamStoreIsNull_ThrowsArgumentNullException() {
            var isThereAnyDealStoreMock = new StubIIsThereAnyDealStore();
            var gameRepo = new GameRepository(
                null,
                isThereAnyDealStoreMock,
                this.taskDelay);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void
            GameRepository_IsThereAnyDealStoreIsNull_ThrowsArgumentNullException() {
            var steamStoreMock = new StubISteamStore();
            var gameRepo = new GameRepository(
                steamStoreMock,
                null,
                this.taskDelay);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public async Task
                GetGamesByMostRecentDeals_QuantityIsNegative_ThrowsOutOfRangeException() {
            var gameRepo = await this.GetFullMock();
            await gameRepo.GetGamesByMostRecentDeals(-1, this.random.Next());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public async Task
                GetGamesByMostRecentDeals_OffsetIsNegative_ThrowsOutOfRangeException() {
            var gameRepo = await this.GetFullMock();
            await gameRepo.GetGamesByMostRecentDeals(this.random.Next(), -1);
        }

        [TestMethod]
        public async Task
            GetGamesByMostRecentDeals_AnyValidInput_ReturnValueIsNotNull() {
            var gameRepo = await this.GetFullMock();
            var returnVal = await gameRepo.GetGamesByMostRecentDeals(
                                this.random.Next(),
                                this.random.Next());
            Assert.IsNotNull(returnVal);
        }

        [TestMethod]
        public async Task
            GetGamesByMostRecentDeals_ResponseTimeGreaterThanTimeout_GameListCountLessThanQuantity() {
            var gameRepo = await this.GetFullMock(new TestTaskDelay(TimeSpan.FromMilliseconds(1)));
            var timer = new Stopwatch();
            var timeOut = TimeSpan.FromSeconds(1);
            var quantity = this.random.Next();
            timer.Start();
            var returnVal = await gameRepo.GetGamesByMostRecentDeals(
                                this.random.Next(),
                                this.random.Next(),
                                timeOut,
                                TimeSpan.FromMilliseconds(100));
            timer.Stop();
            if (timer.Elapsed > timeOut) {
                var counter = 0;
                var enumerator = returnVal.GetEnumerator();
                while (enumerator.MoveNext()) {
                    counter++;
                }
                enumerator.Dispose();
                Assert.IsTrue(counter < quantity);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public async Task
            GetGamesByMostRecentDeals_IsThereAnyDealStoreRecentDealsIsNull_ThrowsNullReferenceException() {
            var steamStoreMock = this.GetFullSteamStoreMock();
            var isThereAnyDealStoreMock = new StubIIsThereAnyDealStore();
            isThereAnyDealStoreMock.RecentDeals(
                async (quantity, offset) =>
                    await Task.FromResult<RecentDealsResponse>(null));
            isThereAnyDealStoreMock.CurrentPrices(
                async plain =>
                    await Task.FromResult(new CurrentPricesResponse()));

            var gameRepo = new GameRepository(
                steamStoreMock,
                isThereAnyDealStoreMock,
                this.taskDelay);
            var returnVal = await gameRepo.GetGamesByMostRecentDeals(
                                this.random.Next(),
                                this.random.Next());
        }

        private async Task<GameRepository> GetFullMock(ITaskDelay delay) {
            var steamStoreMock = this.GetFullSteamStoreMock();
            var isThereAnyDealStoreMock = this.GetFullIsThereAnyDealStoreMock();
            var gameRepo = new GameRepository(
                steamStoreMock,
                isThereAnyDealStoreMock,
                delay);
            return gameRepo;
        }

        private async Task<GameRepository> GetFullMock() {
            return await this.GetFullMock(this.taskDelay);
        }


        private StubIIsThereAnyDealStore
                GetFullIsThereAnyDealStoreMock() {
            var isThereAnyDealStoreMock = new StubIIsThereAnyDealStore();
            isThereAnyDealStoreMock.RecentDeals(
                async (quantity, offset) =>
                    await Task.FromResult(new RecentDealsResponse()));
            isThereAnyDealStoreMock.CurrentPrices(
                async plain =>
                    await Task.FromResult(new CurrentPricesResponse()));
            return isThereAnyDealStoreMock;
        }

        private StubISteamStore GetFullSteamStoreMock() {
            var steamStoreMock = new StubISteamStore();
            steamStoreMock.GetAppId(
                key => 0);
            steamStoreMock.AppList(
                async () => await Task.FromResult(new GetAppListResponse()));
            var dispatcher = CoreApplication.CreateNewView().Dispatcher;
            /*            BitmapImage image = null;
                        await dispatcher.RunAsync(
                            CoreDispatcherPriority.High,
                            () => { image = new BitmapImage(); });*/
            steamStoreMock.GameLogo(async title => new Uri(string.Empty));
            return steamStoreMock;
        }
    }
}
