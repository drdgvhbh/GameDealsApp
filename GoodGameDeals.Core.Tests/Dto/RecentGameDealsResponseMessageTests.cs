namespace GoodGameDeals.Core.Tests.Dto {
    using System;
    using System.Collections.Generic;

    using GoodGameDeals.Core.Dto;
    using GoodGameDeals.Core.Entities;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class RecentGameDealsResponseMessageTests {

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void
                RecentGameDealsResponseMessage_MessageIsNull_ThrowsArgumentNullException() {
            new RecentGameDealsResponseMessage(true, new List<Game>(), null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void
            RecentGameDealsResponseMessage_GameIsNull_ThrowsArgumentNullException() {
            new RecentGameDealsResponseMessage(true, null, string.Empty);
        }
    }
}
