namespace GoodGameDeals.Domain {
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    using Windows.UI.Xaml.Media.Imaging;

    using GoodGameDeals.Models;

    public class Game {
        public Game(
            long id,
            string gameTitle,
            BitmapImage image = null,
            List<Deal> dealsList = null) {
            this.Id = id;
            this.GameTitle = gameTitle;
            this.GameImage = image;
            this.DealsList = dealsList;
        }

        public long Id { get; }

        /// <summary>
        ///     Gets the deals list.
        /// </summary>
        public List<Deal> DealsList { get; set; }

        /// <summary>
        ///     Gets the game image.
        /// </summary>
        public BitmapImage GameImage { get; set; }

        /// <summary>
        ///     Gets the game title.
        /// </summary>
        public string GameTitle { get; }
    }
}
