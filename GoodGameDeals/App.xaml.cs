namespace GoodGameDeals {
    using System.Threading.Tasks;

    using GoodGameDeals.Containers;
    using GoodGameDeals.Gateways.Contracts;

    using MetroLog;
    using MetroLog.Targets;

    using Reactive.Bindings;

    using Template10.Common;
    using Template10.Controls;

    using Unity;

    using Windows.ApplicationModel.Activation;
    using Windows.Storage;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Data;

    using MainPage = Presentation.Views.MainPage;

    /// <summary>
    /// The app.
    /// </summary>
    [Bindable]
    public sealed partial class App : BootStrapper {
        private static readonly ILogger Log = LogManagerFactory
            .DefaultLogManager.GetLogger<App>();

        private RootContainer root;

        private SettingsContainer settingsContainer;

        /// <summary>
        /// Initializes a new instance of the <see cref="App"/> class.
        /// </summary>
        public App() {
            this.InitializeComponent();
            this.settingsContainer = new SettingsContainer();

            this.SplashFactory = (e) => new Views.Splash(e);

            var settings = this.settingsContainer.ISettingsServiceInstance;
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

        public override async Task OnInitializeAsync(IActivatedEventArgs args) {
            await Task.CompletedTask;
        }

        public override async Task OnStartAsync(
                StartKind startKind,
                IActivatedEventArgs args) {
            this.root = (RootContainer)this.Resources["Root"];
            UIDispatcherScheduler.Initialize();

            await this.root.Container.Resolve<ISteamStore>().Initialize();

            // TODO: add your long-running task here
            await this.root.ResolveFileCache("SteamCache").InitializeAsync(
                ApplicationData.Current.LocalCacheFolder,
                "SteamCache");

            await this.root.ResolveFileCache("IGDBCache").InitializeAsync(
                ApplicationData.Current.LocalCacheFolder,
                "IGDBCache");

            await this.root.ResolveFileCache("SteamLogoCache").InitializeAsync(
                ApplicationData.Current.LocalCacheFolder,
                "SteamLogoCache");

            await this.root.ResolveFileCache("SteamAppIdCache").InitializeAsync(
                ApplicationData.Current.LocalCacheFolder,
                "SteamAppIdCache");

            await this.root.ResolveFileCache("IsThereAnyDealCache").InitializeAsync(
                ApplicationData.Current.TemporaryFolder,
                "IsThereAnyDealCache");

            await this.NavigationService.NavigateAsync(typeof(MainPage));
        }
    }
}
