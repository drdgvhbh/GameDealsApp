// ReSharper disable ClassNeverInstantiated.Global
namespace GoodGameDeals.Presentation.ViewModels {
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    using GoodGameDeals.Models;

    using Unity;
    using Unity.Lifetime;

    using Windows.ApplicationModel;
    using Windows.UI.Xaml.Media.Imaging;

    using Template10.Utils;

    using Unity.Injection;

    /// <summary>
    ///     The view model locator.
    /// </summary>
    public class ViewModelLocator : IDisposable {
        private readonly IUnityContainer container;

        public ViewModelLocator(IUnityContainer container) {
            this.container = container;

            this.container.RegisterType<ObservableCollection<GameModel>>(
                new ContainerControlledLifetimeManager(),
                new InjectionConstructor());

            if (DesignMode.DesignModeEnabled) {
                var list
                    = this.container.Resolve<ObservableCollection<GameModel>>();

/*                for (var i = 0; i < 8; i++) {
                    list.Add(
                        new GameModel(
                            0,
                            Company.Name(),
                            Company.BS(),
                            new BitmapImage(),
                            this.FakeDealModels(3)));
                }*/
            }

            this.container.RegisterType<MainPageViewModel>(
                new ContainerControlledLifetimeManager());
        }

        /// <summary>
        ///     The main page view model instance.
        /// </summary>
        public MainPageViewModel MainPageViewModelInstance =>
            this.container.Resolve<MainPageViewModel>();

        public void Dispose() {
            this.container?.Dispose();
        }

        private ObservableCollection<DealModel> FakeDealModels(int count) {
            var collection = new ObservableCollection<DealModel>();
            for (var i = 0; i < count; i++) {
                collection.Add(this.FakeDealModel());
            }
            return collection;
        }

        private DealModel FakeDealModel() {
            throw new NotImplementedException();
/*            var fakeDeal
            var random = new Random();
            var minPrice = random.NextDouble() * (100.0 - 0.01) + 0.01;
            var maxPrice = random.NextDouble() * (100.0 - minPrice) + minPrice;
            var discount = (int)(1 - minPrice / maxPrice);
            return new DealModel(
                Internet.DomainName(),
                discount,
                maxPrice,
                minPrice,
                Company.Name());*/
        }
    }
}