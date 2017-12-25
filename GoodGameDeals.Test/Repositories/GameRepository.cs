namespace GoodGameDeals.Test.Repositories {
    using System;

    using GoodGameDeals.Gateways.Contracts;
    using GoodGameDeals.Gateways.Repositories;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class GameRepositoryTests {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException),
            "A userId of null was inappropriately allowed.")]
        public void
            GameRepository_SteamStoreIsNull_ThrowsArgumentNullException() {
            var isThereAnyDealStoreMock = new StubIIsThereAnyDealStore();
            var gameRepo = new GameRepository(null, isThereAnyDealStoreMock);
        }
    }
}
