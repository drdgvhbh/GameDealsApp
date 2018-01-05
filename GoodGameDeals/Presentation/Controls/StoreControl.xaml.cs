namespace GoodGameDeals.Presentation.Controls {
    using System;

    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;

    public sealed partial class StoreControl : UserControl {
        public static readonly DependencyProperty StoreProperty =
            DependencyProperty.Register(
                "Store",
                typeof(string),
                typeof(StoreControl),
                new PropertyMetadata("Store"));

        public event RoutedEventHandler StoreButtonClick;

        public StoreControl() {
            this.InitializeComponent();
        }

        public string Store {
            get => (string)this.GetValue(StoreProperty);

            set => this.SetValue(StoreProperty, value);
        }

        private void StoreButton_OnClick(object sender, RoutedEventArgs e) {
            this.StoreButtonClick?.Invoke(this, e);
        }
    }
}
