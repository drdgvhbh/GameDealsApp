namespace GoodGameDeals.Core.Entities {
    using System.Collections.Generic;

    public class Game {
        public Game(string id, string gameTitle, long gameLogoId, IList<Deal> deals) {
            this.Id = id;
            this.GameTitle = gameTitle;
            this.GameLogoId = gameLogoId;
            this.Deals = deals;

        }

        public Game(string id, string gameTitle, long gameLogoId) : this(
            id,
            gameTitle,
            gameLogoId,
            new List<Deal>()) {
        }

        public string Id { get; }

        public string GameTitle { get; }

        public long GameLogoId { get; }

        public IList<Deal> Deals { get; }
    }
}
