namespace GoodGameDeals.Presentation.Controls {
    using System.Collections.ObjectModel;

    using GoodGameDeals.Models;

    using Template10.Utils;

    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;
    using Windows.UI.Xaml.Media.Imaging;

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

        public static readonly DependencyProperty GamePriceOldProperty =
            DependencyProperty.Register(
                "GamePriceOld",
                typeof(string),
                typeof(GameDealBoxControl),
                new PropertyMetadata("$0.00"));

        public static readonly DependencyProperty GamePriceProperty =
            DependencyProperty.Register(
                "GamePrice",
                typeof(string),
                typeof(GameDealBoxControl),
                new PropertyMetadata("$0.00"));

        public static readonly DependencyProperty DealsListProperty =
            DependencyProperty.Register(
                "DealsList",
                typeof(ObservableCollection<DealModel>),
                typeof(GameDealBoxControl),
                new PropertyMetadata(new ObservableCollection<DealModel>()));

        public GameDealBoxControl() {
            this.InitializeComponent();
            this.RegisterPropertyChangedCallback(
                DealsListProperty,
                (sender, dp) => {
                    var propArray = (ObservableCollection<DealModel>)this.GetValue(dp);
                    if (propArray.Count > 0) {
                        var prop = propArray[0];
                        this.GamePrice = prop.GamePrice.ToString("C");
                        this.GamePriceOld = prop.GamePriceOld.ToString("C");
                    }


                    });
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

        public string GamePrice {
            get {
                return (string)this.GetValue(GamePriceProperty);
            }

            set {
                this.SetValue(GamePriceProperty, value);
            }
        }

        public string GamePriceOld {
            get {
                return (string)this.GetValue(GamePriceOldProperty);
            }

            set {
                this.SetValue(GamePriceOldProperty, value);
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

/*        public event RoutedEventHandler StoreButtonClick;*/

        private void StoreControl_OnStoreButtonClick(object sender, RoutedEventArgs e) {
            this.DeactivateText();

            if (!(sender is StoreControl storeControl)) {
                return;
            }

            storeControl.IsActive = true;
            var dealModel = storeControl?.DataContext as DealModel;
            this.GamePrice = dealModel?.GamePrice.ToString("C");
            this.GamePriceOld = dealModel?.GamePriceOld.ToString("C");
/*            this.StoreButtonClick?.Invoke(this, e);*/
        }

        private void DeactivateText() {
            var itemCollection = this.Stores.Children[0].AllChildren();
            if (itemCollection != null) {
                foreach (var storeObject in itemCollection) {
                    if (storeObject is StoreControl store) {
                        store.IsActive = false;
                    }
                }
            }
        }
    }
}
