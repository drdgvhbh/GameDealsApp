namespace GoodGameDeals.Models {
    using System.Collections.ObjectModel;

    using Windows.UI.Xaml.Media.Imaging;

    public class Game {
        public Game(
                string gameTitle,
                string gameSubtitle,
                BitmapImage image,
                ObservableCollection<Deal> dealsList) {
            this.GameTitle = gameTitle;
            this.GameSubtitle = gameSubtitle;
            this.GameImage = image;
            this.DealsList = dealsList;
        }

        /// <summary>
        ///     Gets the deals list.
        /// </summary>
        public ObservableCollection<Deal> DealsList { get; }

        /// <summary>
        ///     Gets the game image.
        /// </summary>
        public BitmapImage GameImage { get; }

        /// <summary>
        ///     Gets the game title.
        /// </summary>
        public string GameTitle { get; }

        /// <summary>
        /// Gets the game subtitle.
        /// </summary>
        public string GameSubtitle { get; }
    }
}