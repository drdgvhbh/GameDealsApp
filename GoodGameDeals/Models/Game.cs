namespace GoodGameDeals.Models {
    using System.Collections.ObjectModel;

    using Windows.UI.Xaml.Media.Imaging;

    public class Game {
        public Game(
                string gameTitle,
                BitmapImage image,
                ObservableCollection<Deal> dealsList) {
            this.GameTitle = gameTitle;
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
    }
}