namespace GoodGameDeals.Containers {
    using System;

    using GoodGameDeals.Models.ITAD;
    using GoodGameDeals.Services.HttpServices;
    using GoodGameDeals.Services.JsonServices;
    using GoodGameDeals.ViewModels;

    using Newtonsoft.Json;

    using Unity;
    using Unity.Injection;
    using Unity.Lifetime;

    using Windows.Web.Http;

    using GoodGameDeals.Models.Steam;

    public class RootContainer {
        private readonly IUnityContainer container;

        public RootContainer() {
            this.container = new UnityContainer();
            this.RegisterJsonServices();
            this.RegisterHttpServices();
            this.RegisterViewModels();
        }

        public ViewModelLocator ViewModelLocatorInstance =>
            this.container.Resolve<ViewModelLocator>();

        private void RegisterJsonServices() {
            // Serialization/Deserialization
            this.container.RegisterInstance(
                new JsonSerializerSettings {
                    MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
                    DateParseHandling = DateParseHandling.None
                },
                new ContainerControlledLifetimeManager());

            // Is There Any Deal
            this.container.RegisterType<JsonService<RecentDealsResponse>>(
                new ContainerControlledLifetimeManager());
            this.container.RegisterInstance<Func<string, RecentDealsResponse>>(
                this.container.Resolve<JsonService<RecentDealsResponse>>()
                    .FromJson);
            this.container.RegisterType<JsonService<CurrentPricesResponse>>(
                new ContainerControlledLifetimeManager());
            this.container.RegisterInstance<Func<string, CurrentPricesResponse>>(
                this.container.Resolve<JsonService<CurrentPricesResponse>>()
                    .FromJson);

            // Steam
            this.container.RegisterType<JsonService<GetAppListResponse>>(
                new ContainerControlledLifetimeManager());
            this.container.RegisterInstance<Func<string, GetAppListResponse>>(
                this.container.Resolve<JsonService<GetAppListResponse>>()
                    .FromJson);
        }

        private void RegisterHttpServices() {
            this.container.RegisterType<HttpClient>(new InjectionConstructor());
            this.container.RegisterType<IsThereAnyDealService>(
                new ContainerControlledLifetimeManager());
        }

        private void RegisterViewModels() {
            this.container.RegisterType<ViewModelLocator>(
                new ContainerControlledLifetimeManager(),
                new InjectionConstructor(
                    this.container.CreateChildContainer()));
        }
    }
}