namespace GoodGameDeals.Containers {
    using System;

    using GoodGameDeals.Services.HttpServices;
    using GoodGameDeals.Services.JsonServices;
    using GoodGameDeals.ViewModels;

    using Newtonsoft.Json;

    using Unity;
    using Unity.Injection;
    using Unity.Lifetime;

    using Windows.Web.Http;

    using AutoMapper;

    using GoodGameDeals.Data.Entity.Responses.IsThereAnyDeal;
    using GoodGameDeals.Data.Repositories;
    using GoodGameDeals.Data.Repositories.Stores;
    using GoodGameDeals.Domain;
    using GoodGameDeals.Domain.Interactors;
    using GoodGameDeals.Domain.Mappers;
    using GoodGameDeals.Models;
    using GoodGameDeals.Models.Steam;
    using GoodGameDeals.Presentation.Mappers;

    public class RootContainer {
        private readonly IUnityContainer container;

        public RootContainer() {
            this.container = new UnityContainer();
            this.RegisterJsonServices();
            this.RegisterHttpServices();
            this.RegisterViewModels();
            this.RegisterMappings();
            this.RegisterFactories();
            this.RegisterRepositories();
            this.RegisterStores();
            this.RegisterInteractors();
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

        private void RegisterInteractors() {
            this.container.RegisterType<RecentDealsInteractor>(
                new ContainerControlledLifetimeManager());
        }

        private void RegisterStores() {
            this.container
                .RegisterType<IIsThereAnyDealStore, IsThereAnyDealStore>(
                    new ContainerControlledLifetimeManager());
        }

        private void RegisterRepositories() {
            this.container
                .RegisterType<IIsThereAnyDealRepository, IsThereAnyDealRepository>(
                    new ContainerControlledLifetimeManager());

        }

        private void RegisterMappings() {
            var config = new MapperConfiguration(
                cfg => {
                    cfg.CreateMap<RecentDealsResponse.List, Deal>()
                        .ConvertUsing(new RecentDealsResponseListDealConverter());
                    cfg.CreateMap<Deal, DealModel>()
                        .ConvertUsing(new DealDealModelConverter());
                });
            var mapper = new Mapper(config);
            this.container.RegisterInstance<IMapper>(mapper);
        }

        private void RegisterFactories() {
            this.container.RegisterType<IsThereAnyDealStoreFactory>(
                new ContainerControlledLifetimeManager());

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