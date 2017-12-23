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
    using System.Reactive.Linq;
    using System.Reactive.Subjects;
    using System.Reactive.Threading.Tasks;
    using System.Reactive.Windows.Foundation;
    using System.Text;

    using Windows.Storage;

    using GoodGameDeals.Containers;
    using GoodGameDeals.Data.Entity;
    using GoodGameDeals.Data.Entity.Responses.Steam;
    using GoodGameDeals.Data.Repositories.Stores;
    using GoodGameDeals.Services.SettingsServices;
    using GoodGameDeals.Views;

    using MetroLog;
    using MetroLog.Targets;

    using Reactive.Bindings;

    using Unity;

    /// <summary>
    /// The app.
    /// </summary>
    [Bindable]
    public sealed partial class App : BootStrapper {
        private static readonly ILogger Log = LogManagerFactory
            .DefaultLogManager.GetLogger<App>();

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

        public override async Task OnStartAsync(
            StartKind startKind,
            IActivatedEventArgs args) {
            UIDispatcherScheduler.Initialize();
            // TODO: add your long-running task here
            var root = (RootContainer)this.Resources["Root"];
            await root.ResolveFileCache("SteamCache").InitializeAsync(
                ApplicationData.Current.LocalCacheFolder,
                "SteamCache");

            await root.ResolveFileCache("SteamLogoCache").InitializeAsync(
                ApplicationData.Current.LocalCacheFolder,
                "SteamLogoCache");

            await root.ResolveFileCache("SteamAppIdCache").InitializeAsync(
                ApplicationData.Current.LocalCacheFolder,
                "SteamAppIdCache");

            await root.ResolveFileCache("IsThereAnyDealCache").InitializeAsync(
                ApplicationData.Current.TemporaryFolder,
                "IsThereAnyDealCache");

            await root.Container.Resolve<SteamStoreFactory>().Create().FirstAsync();
            await this.NavigationService.NavigateAsync(typeof(MainPage));
        }
    }
}
