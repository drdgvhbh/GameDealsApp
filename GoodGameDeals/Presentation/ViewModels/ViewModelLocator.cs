// ReSharper disable ClassNeverInstantiated.Global
namespace GoodGameDeals.Presentation.ViewModels {
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    using GoodGameDeals.Models;

    using Unity;
    using Unity.Lifetime;

    using Windows.ApplicationModel;
    using Windows.Globalization.DateTimeFormatting;
    using Windows.UI.Xaml.Media.Imaging;

    using Bogus;

    using Template10.Utils;

    using Unity.Injection;

    /// <summary>
    ///     The view model locator.
    /// </summary>
    public class ViewModelLocator : IDisposable {
        private readonly IUnityContainer container;

        private readonly Randomizer random;

        private readonly Faker faker;

        public ViewModelLocator(IUnityContainer container) {
            this.container = container;
            this.random = new Randomizer();
            this.faker = new Faker();

            this.container.RegisterType<ObservableCollection<GameModel>>(
                new ContainerControlledLifetimeManager(),
                new InjectionConstructor());

            if (DesignMode.DesignModeEnabled) {
                var list
                    = this.container.Resolve<ObservableCollection<GameModel>>();
                for (var i = 0; i < 8; i++) {
                    var fakeGame = new GameModel(
                        this.faker.Date.Past(),
                        this.faker.Name.JobTitle(),
                        this.faker.Finance.EthereumAddress(),
                        new BitmapImage(new Uri(/*this.faker.Image.Nature(460, 215, true)*/"ms-appx:///Presentation/Assets/NoPreviewAvaliable.png")),
                        this.FakeDealModels(3));
                    list.Add(fakeGame);
                }
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
            var minPrice = this.random.Double(0, 100);
            var maxPrice = this.random.Double(minPrice, 100);
            var fakeDeal = new DealModel(this.faker.Internet.UrlWithPath(), 60, maxPrice, minPrice, this.faker.Company.CompanyName());
            return fakeDeal;
        }
    }
}