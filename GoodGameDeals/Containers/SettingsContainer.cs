namespace GoodGameDeals.Containers {
    using Windows.ApplicationModel;

    using GoodGameDeals.Services.SettingsServices;

    using Unity;

    public class SettingsContainer {
        private readonly IUnityContainer container;

        public SettingsContainer() {
            this.container = new UnityContainer();
            if (DesignMode.DesignModeEnabled) {
                this.container
                    .RegisterType<ISettingsService, DesignSettingsService>();
            }
            else {
                this.container.RegisterInstance<ISettingsService>(
                    SettingsService.Instance);
            }
        }

        public ISettingsService ISettingsServiceInstance =>
            this.container.Resolve<ISettingsService>();
    }
}
