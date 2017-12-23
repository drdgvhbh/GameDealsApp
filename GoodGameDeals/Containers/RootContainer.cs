namespace GoodGameDeals.Containers {
    using System;
    using System.Collections.Generic;

    using AutoMapper;

    using GoodGameDeals.Data.Cache;
    using GoodGameDeals.Data.Entity.Responses.IsThereAnyDeal;
    using GoodGameDeals.Data.Entity.Responses.Steam;
    using GoodGameDeals.Data.Repositories;
    using GoodGameDeals.Data.Repositories.Stores;
    using GoodGameDeals.Domain;
    using GoodGameDeals.Domain.Interactors;
    using GoodGameDeals.Domain.Mappers;
    using GoodGameDeals.Models;
    using GoodGameDeals.Presentation.Mappers;
    using GoodGameDeals.Services.JsonServices;
    using GoodGameDeals.ViewModels;

    using Newtonsoft.Json;

    using Unity;
    using Unity.Injection;
    using Unity.Lifetime;

    using Windows.Web.Http;

    public class RootContainer : AbstractContainerInstaller {
        public RootContainer() {
            this.RegisterJsonServices();
        }

        public ViewModelLocator ViewModelLocatorInstance =>
            this.Container.Resolve<ViewModelLocator>();

        public FileCache ResolveFileCache(string name) {
            return this.Container.Resolve<FileCache>(name);
        }

        protected override void RegisterCaches() {
            this.Container.RegisterType<HttpClient>(new InjectionConstructor());

            this.Container.RegisterType<FileCache>(
                "IsThereAnyDealCache",
                new ContainerControlledLifetimeManager(),
                new InjectionConstructor(this.Container.Resolve<HttpClient>()));
            this.Container.Resolve<FileCache>("IsThereAnyDealCache")
                .CacheDuration = TimeSpan.FromDays(1);

            var steamClient = this.Container.Resolve<HttpClient>();
            this.Container.RegisterType<FileCache>(
                "SteamCache",
                new ContainerControlledLifetimeManager(),
                new InjectionConstructor(steamClient));
            this.Container.Resolve<FileCache>("SteamCache").CacheDuration
                = TimeSpan.FromDays(1);

            this.Container.RegisterType<ImageCache>(
                "SteamLogoCache",
                new ContainerControlledLifetimeManager(),
                new InjectionConstructor(steamClient));
            this.Container.Resolve<ImageCache>("SteamLogoCache").CacheDuration =
                TimeSpan.FromHours(6);

            this.Container.RegisterType<InMemoryStorage<long>>(
                "SteamAppIdCache",
                new ContainerControlledLifetimeManager());
            this.Container.Resolve<InMemoryStorage<long>>("SteamAppIdCache")
                .MaxItemCount = int.MaxValue;
        }

        private void RegisterJsonServices() {
            // Serialization/Deserialization
            this.Container.RegisterInstance(
                new JsonSerializerSettings {
                    MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
                    DateParseHandling = DateParseHandling.None
                },
                new ContainerControlledLifetimeManager());

            // Is There Any Deal
            this.Container.RegisterType<JsonService<RecentDealsResponse>>(
                new ContainerControlledLifetimeManager());
            this.Container.RegisterInstance<Func<string, RecentDealsResponse>>(
                this.Container.Resolve<JsonService<RecentDealsResponse>>()
                    .FromJson);
            this.Container.RegisterType<JsonService<CurrentPricesResponse>>(
                new ContainerControlledLifetimeManager());
            this.Container.RegisterInstance<Func<string, CurrentPricesResponse>>(
                this.Container.Resolve<JsonService<CurrentPricesResponse>>()
                    .FromJson);

            // Steam
            this.Container.RegisterType<JsonService<GetAppListResponse>>(
                new ContainerControlledLifetimeManager());
            this.Container.RegisterInstance<Func<string, GetAppListResponse>>(
                this.Container.Resolve<JsonService<GetAppListResponse>>()
                    .FromJson);
        }

        protected override void RegisterInteractors() {
            this.Container.RegisterType<RecentDealsInteractor>(
                new ContainerControlledLifetimeManager());
            this.Container.RegisterType<GameImageInteractor>(
                new ContainerControlledLifetimeManager());
            this.Container.RegisterType<GameAndRecentDealsInteractor>(
                new ContainerControlledLifetimeManager());
        }

        protected override void RegisterRepositories() {
            this.Container.RegisterType<ISteamRepository, SteamRepository>(
                new ContainerControlledLifetimeManager());
            this.Container
                .RegisterType<IIsThereAnyDealRepository, IsThereAnyDealRepository>(
                    new ContainerControlledLifetimeManager());
        }

        protected override void RegisterMappings() {
            var config = new MapperConfiguration(
                cfg => {
                    cfg.CreateMap<RecentDealsResponse.List, Deal>()
                        .ConvertUsing(new RecentDealsResponseListDealConverter());
                    cfg.CreateMap<Deal, DealModel>()
                        .ConvertUsing(new DealDealModelConverter());
                    cfg.CreateMap<CurrentPricesResponse.List, Deal>()
                        .ConvertUsing(new CurrentPricesResponseListDealConverter());
                    cfg.CreateMap<Game, GameModel>()
                        .ConvertUsing(new GameGameModelConverter());
                });
            var mapper = new Mapper(config);
            this.Container.RegisterInstance<IMapper>(mapper);
        }

        protected override void RegisterFactories() {
            this.Container.RegisterType<SteamStoreFactory>(
                new ContainerControlledLifetimeManager());
            this.Container.RegisterType<IsThereAnyDealStoreFactory>(
                new ContainerControlledLifetimeManager());

        }

        protected override void RegisterViewModels() {
            this.Container.RegisterType<ViewModelLocator>(
                new ContainerControlledLifetimeManager(),
                new InjectionConstructor(
                    this.Container.CreateChildContainer()));
        }
    }
}