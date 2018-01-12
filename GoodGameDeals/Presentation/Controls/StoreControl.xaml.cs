namespace GoodGameDeals.Presentation.Controls {
    using System;

    using Windows.UI;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;
    using Windows.UI.Xaml.Media;

    using GoodGameDeals.Models;

    public sealed partial class StoreControl : UserControl {
        private bool isActive;

        private static readonly AcrylicBrush inActiveTextBrush =
            new AcrylicBrush
                {
                    BackgroundSource = AcrylicBackgroundSource.Backdrop,
                    TintColor = Color.FromArgb(255, 99, 99, 99),
                    FallbackColor = Color.FromArgb(255, 99, 99, 99),
                    TintOpacity = 0.9
                };

        private static readonly AcrylicBrush activeTextBrush =
            new AcrylicBrush
                {
                    BackgroundSource = AcrylicBackgroundSource.Backdrop,
                    TintColor = Color.FromArgb(255, 255, 255, 255),
                    FallbackColor = Color.FromArgb(255, 255, 255, 255),
                    TintOpacity = 1.0
                };

        public static readonly DependencyProperty StoreProperty =
            DependencyProperty.Register(
                "Store",
                typeof(string),
                typeof(StoreControl),
                new PropertyMetadata("Store"));

        public event RoutedEventHandler StoreButtonClick;

        public StoreControl() {
            this.InitializeComponent();
            this.DataContextChanged += this.StoreControl_DataContextChanged;
        }

        private void StoreControl_DataContextChanged(FrameworkElement sender, DataContextChangedEventArgs args) {
            this.IsActive = ((DealModel)args.NewValue).IsActive;
        }

        public string Store {
            get => (string)this.GetValue(StoreProperty);

            set => this.SetValue(StoreProperty, value);
        }

        public bool IsActive {
            get => this.isActive;
            set {
                this.isActive = value;
                this.StoreName.Foreground =
                    this.isActive ? activeTextBrush : inActiveTextBrush;
            }
        }

        private void StoreButton_OnClick(object sender, RoutedEventArgs e) {
            this.StoreButtonClick?.Invoke(this, e);
        }
    }
}
