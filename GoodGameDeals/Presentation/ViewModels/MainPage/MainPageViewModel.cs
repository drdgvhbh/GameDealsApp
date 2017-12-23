namespace GoodGameDeals.Presentation.ViewModels.MainPage {
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Windows.ApplicationModel;
    using Windows.UI.Xaml.Navigation;

    using GoodGameDeals.Messages;
    using GoodGameDeals.Views;

    using MetroLog;

    using Template10.Mvvm;
    using Template10.Services.NavigationService;
    using Template10.Services.SerializationService;

    public class MainPageViewModel : ViewModelBase {
        /// <summary>
        ///     The log.
        /// </summary>
        private static readonly ILogger Log = LogManagerFactory
            .DefaultLogManager.GetLogger<MainPageViewModel>();

        private AccessToken accessToken;

        public GameDealsViewModel GameDealsViewModel { get; }

        public MainPageViewModel(GameDealsViewModel gameDealsViewModel) {
            if (DesignMode.DesignModeEnabled) {
            }
            this.GameDealsViewModel = gameDealsViewModel;
            this.accessToken = new AccessToken();
        }

        public void GotoAbout() =>
            this.NavigationService.Navigate(typeof(SettingsPage), 2);

        public void GotoPrivacy() =>
            this.NavigationService.Navigate(typeof(SettingsPage), 1);

        public void GotoSettings() =>
            this.NavigationService.Navigate(typeof(SettingsPage), 0);

/*        /// <summary>
        ///     The nav to details page.
        /// </summary>
        public void NavToDetailsPage() =>
            this.NavigationService.Navigate(typeof(DetailPage), this.Value);*/

        public override async Task OnNavigatedFromAsync(
            IDictionary<string, object> suspensionState,
            bool suspending) {
            if (suspending) {
/*                suspensionState[nameof(this.Value)] = this.Value;*/
            }

            await Task.CompletedTask;
        }

        public override async Task OnNavigatedToAsync(
            object parameter,
            NavigationMode mode,
            IDictionary<string, object> suspensionState) {
            if (suspensionState.Any()) {
/*                this.Value = suspensionState[nameof(this.Value)]?.ToString();*/
            }

            if (parameter is string param) {
                SerializationService.Json.TryDeserialize(
                    param,
                    out this.accessToken);
                Log.Info("Access Token set to {0}", this.accessToken);
            }

            await this.GameDealsViewModel.OnNavigatedToAsync(
                parameter,
                mode,
                suspensionState);
            await Task.CompletedTask;
        }

        public override async Task OnNavigatingFromAsync(
            NavigatingEventArgs args) {
            args.Cancel = false;
            await Task.CompletedTask;
        }

    }
}