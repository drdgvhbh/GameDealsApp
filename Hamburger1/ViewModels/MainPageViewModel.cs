namespace Hamburger1.ViewModels {
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Net.Http;
    using System.Threading.Tasks;

    using Hamburger1.Controls;
    using Hamburger1.Messages;
    using Hamburger1.Models;
    using Hamburger1.Views;

    using MetroLog;

    using Template10.Mvvm;
    using Template10.Services.NavigationService;
    using Template10.Services.SerializationService;

    using Windows.ApplicationModel;
    using Windows.UI.Xaml.Navigation;

    public class MainPageViewModel : ViewModelBase {
        /// <summary>
        ///     The log.
        /// </summary>
        private static readonly ILogger Log = LogManagerFactory
            .DefaultLogManager.GetLogger<MainPageViewModel>();

        private string _Value = "Gas";

        private AccessToken accessToken;

        public ObservableCollection<Deal> DealsList { get; }

        public MainPageViewModel() {
            if (DesignMode.DesignModeEnabled) {
                this.Value = "Designtime value";
            }

            this.DealsList = new ObservableCollection<Deal>();
            this.accessToken = new AccessToken();
        }

        public string Value {
            get {
                return this._Value;
            }

            set {
                this.Set(ref this._Value, value);
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
            this.DealsList.Add(new Deal("Derp"));
            await Task.CompletedTask;
        }

        public override async Task OnNavigatingFromAsync(
            NavigatingEventArgs args) {
            args.Cancel = false;
            await Task.CompletedTask;
        }

        public async Task PopulateDealsList() {
            var client = new HttpClient();
        }
    }
}