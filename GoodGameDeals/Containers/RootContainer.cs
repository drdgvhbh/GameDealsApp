namespace GoodGameDeals.Containers {
    using System;

    using Windows.UI.Xaml.Data;

    using AutoMapper;

    using GoodGameDeals.Models;
    using GoodGameDeals.Presentation.Mappers;
    using GoodGameDeals.Presentation.ViewModels;

    using Newtonsoft.Json;

    using Unity;
    using Unity.Injection;
    using Unity.Lifetime;

    using Windows.Web.Http;

    using GoodGameDeals.Core.Contracts.Repositories;
    using GoodGameDeals.Core.UseCases;
    using GoodGameDeals.Data.ApiResponses.IsThereAnyDeal;
    using GoodGameDeals.Gateways.Contracts;
    using GoodGameDeals.Gateways.Repositories;
    using GoodGameDeals.Gateways.Stores;
    using GoodGameDeals.Threading;
    using GoodGameDeals.Threading.Tasks;

    using Microsoft.Toolkit.Uwp.UI;

    using FileCache = GoodGameDeals.Data.Cache.FileCache;
    using Game = GoodGameDeals.Core.Entities.Game;

    [Bindable]
    public class RootContainer : AbstractContainerInstaller {
        public RootContainer() {
            this.Container.RegisterType<ITaskDelay, ProductionTaskDelay>(
                new ContainerControlledLifetimeManager());
            this.RegisterJsonServices();
            this.RegisterStores();
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
                .CacheDuration = TimeSpan.FromDays(10);

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
                new InjectionConstructor());
            this.Container
                .Resolve<ImageCache>("SteamLogoCache")
                .CacheDuration = TimeSpan.FromHours(6);

            this.Container.RegisterType<Microsoft.Toolkit.Uwp.UI.InMemoryStorage<long>>(
                "SteamAppIdCache",
                new ContainerControlledLifetimeManager());
            this.Container.Resolve<Microsoft.Toolkit.Uwp.UI.InMemoryStorage<long>>("SteamAppIdCache")
                .MaxItemCount = int.MaxValue;
        }

        private void RegisterJsonServices() {
            // Serialization/Deserialization
            this.Container.RegisterInstance(
                new JsonSerializerSettings {
                    MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
                    DateParseHandling = DateParseHandling.None,
                    ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor
                },
                new ContainerControlledLifetimeManager());
        }

        protected override void RegisterInteractors() {
            this.Container.RegisterType<RequestRecentGameDealsInteractor>(
                new ContainerControlledLifetimeManager());
        }

        protected override void RegisterRepositories() {
            this.Container.RegisterType<IGamesRepository, GameRepository>(
                new ContainerControlledLifetimeManager());
        }

        protected override void RegisterMappings() {
            var config = new MapperConfiguration(
                cfg => {
                    cfg.CreateMap<Game, GameModel>()
                        .ConvertUsing(new GameGameModelConverter());
                });
            var mapper = new Mapper(config);
            this.Container.RegisterInstance<IMapper>(mapper);
        }

        protected override void RegisterFactories() {
        }

        private void RegisterStores() {
            this.Container
                .RegisterType<IIsThereAnyDealStore, IsThereAnyDealStore>(
                    new ContainerControlledLifetimeManager());
            this.Container
                .RegisterType<ISteamStore, SteamStore>(
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