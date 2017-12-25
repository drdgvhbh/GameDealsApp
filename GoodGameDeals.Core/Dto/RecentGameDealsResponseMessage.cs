namespace GoodGameDeals.Core.Dto {
    using System;
    using System.Collections.Generic;

    using GoodGameDeals.Core.Contracts;
    using GoodGameDeals.Core.Entities;

    public class RecentGameDealsResponseMessage : AbstractResponseMessage {
        public RecentGameDealsResponseMessage(
                bool isSuccessful,
                string message)
                    : this(isSuccessful, new List<Game>(), message) {
        }

        public RecentGameDealsResponseMessage(
                bool isSuccessful,
                List<Game> games)
                    : this(isSuccessful, games, string.Empty) {
        }

        public RecentGameDealsResponseMessage(
                bool isSuccessful,
                List<Game> games,
                string message)
                    : base(isSuccessful, message) {
            this.Games = games ?? throw new ArgumentNullException(nameof(message));
        }

        public List<Game> Games { get; }
    }
}