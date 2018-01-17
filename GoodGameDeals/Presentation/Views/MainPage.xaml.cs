namespace GoodGameDeals.Presentation.Views {
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;
    using Windows.UI.Xaml.Navigation;

    using GoodGameDeals.Presentation.ViewModels;

    public sealed partial class MainPage : Page {
        private ItemsWrapGrid itemsWrapGrid;

        public MainPage() {
            this.InitializeComponent();
            this.NavigationCacheMode = NavigationCacheMode.Enabled;
        }

        private void AdaptiveStatesCurrentStateChanged(
                object sender,
                VisualStateChangedEventArgs e) {
            this.UpdateForVisualState(e.NewState, e.OldState);

        }

        private void UpdateForVisualState(
                VisualState newState,
                VisualState oldState = null) {
            if (newState == this.NarrowState) {
                this.GameBarView.Visibility = Visibility.Visible;
            }

            if (newState == this.DefaultState || newState == this.WideState) {
                this.GameBarView.Visibility = Visibility.Collapsed;
            }

            if (newState == this.WideState) {
                this.itemsWrapGrid.MaximumRowsOrColumns = 3;
            }

            if (newState == this.DefaultState) {
                this.itemsWrapGrid.MaximumRowsOrColumns = 1;
            }
        }

        private void GameListView_OnItemClick(object sender, ItemClickEventArgs e) {
            (this.DataContext as MainPageViewModel)?.OnDealButtonPressed(sender, e);
        }

        private void ItemsWrapGrid_OnLoaded(object sender, RoutedEventArgs e) {
            this.itemsWrapGrid = sender as ItemsWrapGrid;
        }
    }
}