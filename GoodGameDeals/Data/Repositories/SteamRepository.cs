namespace GoodGameDeals.Data.Repositories {
    using System;
    using System.Reactive;
    using System.Reactive.Linq;
    using System.Reactive.Subjects;

    using Windows.UI.Xaml.Media.Imaging;

    using AutoMapper;

    using GoodGameDeals.Data.Entity.Responses.Steam;
    using GoodGameDeals.Data.Repositories.Stores;

    using Reactive.Bindings.Extensions;

    public class SteamRepository : ISteamRepository {
        private SteamStoreFactory factory;

        private IMapper mapper;

        public SteamRepository(SteamStoreFactory factory, IMapper mapper) {
            this.factory = factory;
            this.mapper = mapper;
        }

        public IObservable<BitmapImage> GameLogo(string title) {
            var subject = new Subject<BitmapImage>();
            this.factory.Create().ObserveOnUIDispatcher().Subscribe(
                steamStore => {
                    steamStore.GameLogo(title).ObserveOnUIDispatcher().Subscribe(
                        image => {
                            subject.OnNext(image);
                        });
                });
            return subject;
        }

        public IObservable<GetAppListResponse> AppList() {
            var subject = new Subject<GetAppListResponse>();

            var store = this.factory.Create().Subscribe(
                steamStore => {
                    steamStore.AppList().Subscribe(
                        response => {
                            subject.OnNext(response);
                        });
                });
            return subject;
        }
    }
}
