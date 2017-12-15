namespace GoodGameDeals.ViewModels.MainPage {
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Threading.Tasks;

    using GoodGameDeals.Messages;
    using GoodGameDeals.Models;
    using GoodGameDeals.Models.ITAD;
    using GoodGameDeals.Models.Steam;
    using GoodGameDeals.Views;

    using MetroLog;

    using Template10.Mvvm;
    using Template10.Services.NavigationService;
    using Template10.Services.SerializationService;

    using Windows.ApplicationModel;
    using Windows.ApplicationModel.Resources;
    using Windows.UI.Xaml.Media.Imaging;
    using Windows.UI.Xaml.Navigation;

    public class MainPageViewModel : ViewModelBase {
        /// <summary>
        ///     The log.
        /// </summary>
        private static readonly ILogger Log = LogManagerFactory
            .DefaultLogManager.GetLogger<MainPageViewModel>();

        private string _Value = "Gas";

        private AccessToken accessToken;

        public GameDealsViewModel GameDealsViewModel { get; }

        public MainPageViewModel() {
            if (DesignMode.DesignModeEnabled) {
                this.Value = "Designtime value";
            }
            this.GameDealsViewModel = new GameDealsViewModel();
            this.accessToken = new AccessToken();
        }

        public string Value {
            get {
                return this._Value;
            }

            set {
                this.Set(ref this._Value, value);
                this.RaisePropertyChanged();
            }
        }

        public void GotoAbout() =>
            this.NavigationService.Navigate(typeof(SettingsPage), 2);

        public void GotoPrivacy() =>
            this.NavigationService.Navigate(typeof(SettingsPage), 1);

        public void GotoSettings() =>
            this.NavigationService.Navigate(typeof(SettingsPage), 0);

        /// <summary>
        ///     The nav to details page.
        /// </summary>
        public void NavToDetailsPage() =>
            this.NavigationService.Navigate(typeof(DetailPage), this.Value);

        public override async Task OnNavigatedFromAsync(
            IDictionary<string, object> suspensionState,
            bool suspending) {
            if (suspending) {
                suspensionState[nameof(this.Value)] = this.Value;
            }

            await Task.CompletedTask;
        }

        public override async Task OnNavigatedToAsync(
            object parameter,
            NavigationMode mode,
            IDictionary<string, object> suspensionState) {
            if (suspensionState.Any()) {
                this.Value = suspensionState[nameof(this.Value)]?.ToString();
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