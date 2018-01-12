namespace GoodGameDeals.Presentation.ViewModels {
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Template10.Mvvm;
    using Template10.Services.NavigationService;

    using Windows.UI.Xaml.Navigation;

    public class DetailPageViewModel : ViewModelBase {
        public DetailPageViewModel() {
            if (Windows.ApplicationModel.DesignMode.DesignModeEnabled) {
                this.Value = "Designtime value";
            }
        }

        private string _Value = "Default";
        public string Value { get { return this._Value; } set { this.Set(ref this._Value, value); } }

        public override async Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> suspensionState) {
            this.Value = (suspensionState.ContainsKey(nameof(this.Value))) ? suspensionState[nameof(this.Value)]?.ToString() : parameter?.ToString();
            await Task.CompletedTask;
        }

        public override async Task OnNavigatedFromAsync(IDictionary<string, object> suspensionState, bool suspending) {
            if (suspending) {
                suspensionState[nameof(this.Value)] = this.Value;
            }
            await Task.CompletedTask;
        }

        public override async Task OnNavigatingFromAsync(NavigatingEventArgs args) {
            args.Cancel = false;
            await Task.CompletedTask;
        }
    }
}
