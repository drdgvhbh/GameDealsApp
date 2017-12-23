namespace GoodGameDeals.ViewModels {
    using System;

    using GoodGameDeals.Presentation.ViewModels.MainPage;

    using Unity;
    using Unity.Injection;
    using Unity.Lifetime;

    using Windows.ApplicationModel;
    using Windows.ApplicationModel.Resources;
    using Windows.Web.Http;

    /// <summary>
    ///     The view model locator.
    /// </summary>
    public class ViewModelLocator : IDisposable {
        private IUnityContainer container;

        /// <summary>
        ///     Initializes a new instance of the <see cref="ViewModelLocator" /> class.
        /// </summary>
        public ViewModelLocator(IUnityContainer container) {
            this.container = container;
            if (DesignMode.DesignModeEnabled) {
                // TODO Create design time view services and models;
            }

            this.container.RegisterType<GameDealsViewModel>(new ContainerControlledLifetimeManager());

            this.container.RegisterType<MainPageViewModel>(new ContainerControlledLifetimeManager());
        }

        /// <summary>
        ///     The main page view model instance.
        /// </summary>
        public MainPageViewModel MainPageViewModelInstance =>
            this.container.Resolve<MainPageViewModel>();

        public void Dispose() {
            this.container?.Dispose();
        }
    }
}