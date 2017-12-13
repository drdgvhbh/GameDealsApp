

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236
namespace GoodGameDeals.Controls {
    using System;

    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;
    using Windows.UI.Xaml.Media.Imaging;

    public sealed partial class GameDealControl : UserControl {
        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty GameTitleProperty =
            DependencyProperty.Register(
                "GameTitle",
                typeof(string),
                typeof(GameDealControl),
                new PropertyMetadata("Game Title"));

        public static readonly DependencyProperty GamePriceProperty =
            DependencyProperty.Register(
                "GamePrice",
                typeof(string),
                typeof(GameDealControl),
                new PropertyMetadata("$0.00"));

        public static readonly DependencyProperty GamePriceOldProperty =
            DependencyProperty.Register(
                "GamePriceOld",
                typeof(string),
                typeof(GameDealControl),
                new PropertyMetadata("$0.00"));

        public static readonly DependencyProperty DiscountProperty =
            DependencyProperty.Register(
                "Discount",
                typeof(string),
                typeof(GameDealControl),
                new PropertyMetadata("-0%"));

        public static readonly DependencyProperty ImageProperty =
            DependencyProperty.Register(
                "Image",
                typeof(BitmapImage),
                typeof(GameDealControl),
                new PropertyMetadata(new BitmapImage(new Uri("ms-appx:///Assets/NoPreviewAvaliable.png"))));

        public GameDealControl() {
            this.InitializeComponent();
        }

        public string GameTitle {
            get {
                return (string)this.GetValue(GameTitleProperty);
            }

            set {
                this.SetValue(GameTitleProperty, value);
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

        public string Discount {
            get {
                return (string)this.GetValue(DiscountProperty);
            }

            set {
                this.SetValue(DiscountProperty, value);
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

    }
}