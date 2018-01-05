namespace GoodGameDeals.Presentation.Controls {
    using System.Collections.ObjectModel;

    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;
    using Windows.UI.Xaml.Media.Imaging;

    using GoodGameDeals.Models;

    public sealed partial class GameDealBoxControl : UserControl {
        public static readonly DependencyProperty GameTitleProperty =
            DependencyProperty.Register(
                "GameTitle",
                typeof(string),
                typeof(GameDealBoxControl),
                new PropertyMetadata("Game Title"));

        public static readonly DependencyProperty GameSubtitleProperty =
            DependencyProperty.Register(
                "GameSubtitle",
                typeof(string),
                typeof(GameDealBoxControl),
                new PropertyMetadata("Game Subtitle"));

        public static readonly DependencyProperty ImageProperty =
            DependencyProperty.Register(
                "Image",
                typeof(BitmapImage),
                typeof(GameDealBoxControl),
                new PropertyMetadata(new BitmapImage()));

        public static readonly DependencyProperty DealsListProperty =
            DependencyProperty.Register(
                "DealsList",
                typeof(ObservableCollection<DealModel>),
                typeof(GameDealBoxControl),
                new PropertyMetadata(new ObservableCollection<DealModel>()));

        public GameDealBoxControl() {
            this.InitializeComponent();
        }

        public string GameTitle {
            get => (string)this.GetValue(GameTitleProperty);

            set => this.SetValue(GameTitleProperty, value);
        }

        public string GameSubtitle {
            get {
                return (string)this.GetValue(GameSubtitleProperty);
            }

            set {
                this.SetValue(GameSubtitleProperty, value);
            }
        }

        public BitmapImage Image {
            get {
                return (BitmapImage)this.GetValue(ImageProperty);
            }

            set {
                this.SetValue(ImageProperty, value);
            }
        }

        public ObservableCollection<DealModel> DealsList {
            get {
                return (ObservableCollection<DealModel>)this.GetValue(DealsListProperty);
            }

            set {
                this.SetValue(DealsListProperty, value);
            }
        }
    }
}
