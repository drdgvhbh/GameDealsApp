namespace GoodGameDeals.Core.Entities {
    using System;
    using System.Collections.Generic;

    using Windows.UI.Xaml.Media.Imaging;

    public class Game {
        public Game(string id, string gameTitle, Uri gameLogo, IList<Deal> deals) {
            this.Id = id;
            this.GameTitle = gameTitle;
            this.GameLogo = gameLogo;
            this.Deals = deals;

        }

        public Game(string id, string gameTitle, Uri gameLogo) : this(
            id,
            gameTitle,
            gameLogo,
            new List<Deal>()) {
        }

        public string Id { get; }

        public string GameTitle { get; }

        public Uri GameLogo { get; }

        public IList<Deal> Deals { get; }
    }
}
