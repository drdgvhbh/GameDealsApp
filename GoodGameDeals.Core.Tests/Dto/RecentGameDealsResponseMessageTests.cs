namespace GoodGameDeals.Core.Tests.Dto {
    using System;
    using System.Collections.Generic;

    using GoodGameDeals.Core.Dto;
    using GoodGameDeals.Core.Entities;

    using Xunit;

    public class RecentGameDealsResponseMessageTests {

        [Fact]
        public void
                RecentGameDealsResponseMessage_MessageIsNull_ThrowsArgumentNullException() {
            Assert.Throws<ArgumentNullException>(
                () => new RecentGameDealsResponseMessage(true, new List<Game>(), null));
        }

        [Fact]
        public void
            RecentGameDealsResponseMessage_GameIsNull_ThrowsArgumentNullException() {
            Assert.Throws<ArgumentNullException>(
                () => new RecentGameDealsResponseMessage(true, null, string.Empty));
        }
    }
}
