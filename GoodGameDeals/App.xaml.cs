using Windows.UI.Xaml;
using System.Threading.Tasks;
using GoodGameDeals.Services.SettingsServices;
using Windows.ApplicationModel.Activation;
using Template10.Controls;
using Template10.Common;
using System;
using System.Linq;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Controls;

namespace GoodGameDeals {
    using GoodGameDeals.Containers;
    using GoodGameDeals.Services.SettingsServices;

    using MetroLog;
    using MetroLog.Targets;

    /// <summary>
    /// The app.
    /// </summary>
    [Bindable]
    public sealed partial class App : BootStrapper {
        /// <summary>
        /// Initializes a new instance of the <see cref="App"/> class.
        /// </summary>
        public App() {
            this.InitializeComponent();
            this.SplashFactory = (e) => new Views.Splash(e);

            var settings = SettingsService.Instance;
            this.RequestedTheme = settings.AppTheme;
            this.CacheMaxDuration = settings.CacheMaxDuration;
            this.ShowShellBackButton = settings.UseShellBackButton;

#if DEBUG
            LogManagerFactory.DefaultConfiguration.AddTarget(
                LogLevel.Trace,
                LogLevel.Fatal,
                new StreamingFileTarget());
#endif
        }

        public override UIElement CreateRootElement(IActivatedEventArgs e) {
            var service = NavigationServiceFactory(BackButton.Attach, ExistingContent.Exclude);
            return new ModalDialog {
                DisableBackButtonWhenModal = true,
                Content = new Views.Shell(service),
                ModalContent = new Views.Busy(),
            };
        }

        public override async Task OnStartAsync(StartKind startKind, IActivatedEventArgs args) {
            var derp = (RootContainer)this.Resources["Root"];

            // TODO: add your long-running task here
            await NavigationService.NavigateAsync(typeof(Views.MainPage));
        }
    }
}
