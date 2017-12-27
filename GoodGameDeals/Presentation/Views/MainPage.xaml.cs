namespace GoodGameDeals.Presentation.Views {
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;
    using Windows.UI.Xaml.Navigation;

    using NullGuard;

    public sealed partial class MainPage : Page {
        public MainPage() {
            this.InitializeComponent();
            this.NavigationCacheMode = NavigationCacheMode.Enabled;
        }

        private void AdaptiveStatesCurrentStateChanged(
                object sender,
                VisualStateChangedEventArgs e) {
            System.Diagnostics.Debug.WriteLine("Hello World!");

        }

    }
}