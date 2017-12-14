// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236
namespace GoodGameDeals.Controls {
    using System;
    using System.Collections.ObjectModel;

    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;
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

        public static readonly DependencyProperty ImageProperty =
            DependencyProperty.Register(
                "Image",
                typeof(BitmapImage),
                typeof(GameDealControl),
                new PropertyMetadata(new BitmapImage(new Uri("ms-appx:///Assets/NoPreviewAvaliable.png"))));

        public static readonly DependencyProperty DealsListProperty =
            DependencyProperty.Register(
                "DealsList",
                typeof(ObservableCollection<Deal>),
                typeof(GameDealControl),
                new PropertyMetadata(new ObservableCollection<Deal>()));

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

        public BitmapImage Image {
            get {
                return (BitmapImage)this.GetValue(ImageProperty);
            }

            set {
                this.SetValue(ImageProperty, value);
            }
        }

        public ObservableCollection<Deal> DealsList {
            get {
                return (ObservableCollection<Deal>)this.GetValue(DealsListProperty);
            }

            set {
                this.SetValue(DealsListProperty, value);
            }
        }

    }
}