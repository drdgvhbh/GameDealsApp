namespace GoodGameDeals.Presentation.Controls {
    using System.Collections.ObjectModel;

    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;
    using Windows.UI.Xaml.Media.Imaging;

    using GoodGameDeals.Models;

    using NullGuard;

    public sealed partial class GameDealControl : UserControl {
        public static readonly DependencyProperty GameTitleProperty =
            DependencyProperty.Register(
                "GameTitle",
                typeof(string),
                typeof(GameDealControl),
                new PropertyMetadata("Game Title"));

        public static readonly DependencyProperty GameSubtitleProperty =
            DependencyProperty.Register(
                "GameSubtitle",
                typeof(string),
                typeof(GameDealControl),
                new PropertyMetadata("Game Subtitle"));

        public static readonly DependencyProperty ImageProperty =
            DependencyProperty.Register(
                "Image",
                typeof(BitmapImage),
                typeof(GameDealControl),
                new PropertyMetadata(new BitmapImage()));

        public static readonly DependencyProperty DealsListProperty =
            DependencyProperty.Register(
                "DealsList",
                typeof(ObservableCollection<DealModel>),
                typeof(GameDealControl),
                new PropertyMetadata(new ObservableCollection<DealModel>()));


        private VisualState defaultState;

        public GameDealControl() {
            this.InitializeComponent();
         //   this.defaultState = this.;
            this.grid.PointerEntered += (sender, args) => {
                VisualStateManager.GoToState(this, "PointerOver", true);
            };
            this.grid.PointerExited += (sender, args) => {
                VisualStateManager.GoToState(this, "Normal", true);
            };
        }

        private void AdaptiveStatesCurrentStateChanged(
                object sender,
                VisualStateChangedEventArgs e) {
            this.UpdateForVisualState(e.NewState, e.OldState);

        }

        private void UpdateForVisualState(
            VisualState newState,
            [AllowNull]VisualState oldState = null) {
/*            if (newState == this.Narrow) {
                this.GameBarView.Visibility = Visibility.Visible;
            }

            if (newState == this.DefaultState) {
                this.GameBarView.Visibility = Visibility.Collapsed;
            }*/
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