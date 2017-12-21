// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236
namespace GoodGameDeals.Controls {
    using System;
    using System.Collections.ObjectModel;

    using Windows.UI;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;
    using Windows.UI.Xaml.Media;
    using Windows.UI.Xaml.Media.Imaging;

    using GoodGameDeals.Models;

    public sealed partial class GameDealControl : UserControl {
        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
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

        public GameDealControl() {
            this.InitializeComponent();
            this.grid.PointerEntered += (sender, args) => {
                VisualStateManager.GoToState(this, "PointerOver", true);
            };
            this.grid.PointerExited += (sender, args) => {
                VisualStateManager.GoToState(this, "Normal", true);
            };
        }

        public string GameTitle {
            get {
                return (string)this.GetValue(GameTitleProperty);
            }

            set {
                this.SetValue(GameTitleProperty, value);
            }
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