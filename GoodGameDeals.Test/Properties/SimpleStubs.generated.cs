using  System;
using  System.Runtime.CompilerServices;
using  Etg.SimpleStubs;

namespace GoodGameDeals.Core.Contracts
{
    [CompilerGenerated]
    public class StubIRequest : IRequest
    {
        private readonly StubContainer<StubIRequest> _stubs = new StubContainer<StubIRequest>();

        public MockBehavior MockBehavior { get; set; }

        public StubIRequest(MockBehavior mockBehavior = MockBehavior.Loose)
        {
            MockBehavior = mockBehavior;
        }
    }
}

namespace GoodGameDeals.Core.Contracts
{
    using System;
    using System.Threading.Tasks;

    [CompilerGenerated]
    public class StubIRequestHandler<TRequest, TResponse> : IRequestHandler<TRequest, TResponse>
    {
        private readonly StubContainer<StubIRequestHandler<TRequest, TResponse>> _stubs = new StubContainer<StubIRequestHandler<TRequest, TResponse>>();

        public MockBehavior MockBehavior { get; set; }

        global::System.Threading.Tasks.Task<TResponse> global::GoodGameDeals.Core.Contracts.IRequestHandler<TRequest, TResponse>.Handle(TRequest request)
        {
            Handle_TRequest_Delegate del;
            if (MockBehavior == MockBehavior.Strict)
            {
                del = _stubs.GetMethodStub<Handle_TRequest_Delegate>("Handle");
            }
            else
            {
                if (!_stubs.TryGetMethodStub<Handle_TRequest_Delegate>("Handle", out del))
                {
                    return Task.FromResult(default(TResponse));
                }
            }

            return del.Invoke(request);
        }

        public delegate global::System.Threading.Tasks.Task<TResponse> Handle_TRequest_Delegate(TRequest request);

        public StubIRequestHandler<TRequest, TResponse> Handle(Handle_TRequest_Delegate del, int count = Times.Forever, bool overwrite = false)
        {
            _stubs.SetMethodStub(del, count, overwrite);
            return this;
        }

        public StubIRequestHandler(MockBehavior mockBehavior = MockBehavior.Loose)
        {
            MockBehavior = mockBehavior;
        }
    }
}

namespace GoodGameDeals.Core.Contracts.Repositories
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using GoodGameDeals.Core.Entities;

    [CompilerGenerated]
    public class StubIGamesRepository : IGamesRepository
    {
        private readonly StubContainer<StubIGamesRepository> _stubs = new StubContainer<StubIGamesRepository>();

        public MockBehavior MockBehavior { get; set; }

        global::System.Threading.Tasks.Task<global::System.Collections.Generic.IEnumerable<global::GoodGameDeals.Core.Entities.Game>> global::GoodGameDeals.Core.Contracts.Repositories.IGamesRepository.GetGamesByMostRecentDeals(int quantity, int offset)
        {
            GetGamesByMostRecentDeals_Int32_Int32_Delegate del;
            if (MockBehavior == MockBehavior.Strict)
            {
                del = _stubs.GetMethodStub<GetGamesByMostRecentDeals_Int32_Int32_Delegate>("GetGamesByMostRecentDeals");
            }
            else
            {
                if (!_stubs.TryGetMethodStub<GetGamesByMostRecentDeals_Int32_Int32_Delegate>("GetGamesByMostRecentDeals", out del))
                {
                    return Task.FromResult(default(global::System.Collections.Generic.IEnumerable<global::GoodGameDeals.Core.Entities.Game>));
                }
            }

            return del.Invoke(quantity, offset);
        }

        public delegate global::System.Threading.Tasks.Task<global::System.Collections.Generic.IEnumerable<global::GoodGameDeals.Core.Entities.Game>> GetGamesByMostRecentDeals_Int32_Int32_Delegate(int quantity, int offset);

        public StubIGamesRepository GetGamesByMostRecentDeals(GetGamesByMostRecentDeals_Int32_Int32_Delegate del, int count = Times.Forever, bool overwrite = false)
        {
            _stubs.SetMethodStub(del, count, overwrite);
            return this;
        }

        public StubIGamesRepository(MockBehavior mockBehavior = MockBehavior.Loose)
        {
            MockBehavior = mockBehavior;
        }
    }
}

namespace GoodGameDeals.Data.Repositories
{
    using System;
    using System.Collections.Generic;

    using GoodGameDeals.Data.Entity;
    using GoodGameDeals.Domain;

    [CompilerGenerated]
    public class StubIIsThereAnyDealRepository : IIsThereAnyDealRepository
    {
        private readonly StubContainer<StubIIsThereAnyDealRepository> _stubs = new StubContainer<StubIIsThereAnyDealRepository>();

        public MockBehavior MockBehavior { get; set; }

        global::System.IObservable<global::System.Collections.Generic.List<global::GoodGameDeals.Domain.Deal>> global::GoodGameDeals.Data.Repositories.IIsThereAnyDealRepository.RecentDeals(global::GoodGameDeals.Data.Entity.Country country, int offset, int limit)
        {
            RecentDeals_Country_Int32_Int32_Delegate del;
            if (MockBehavior == MockBehavior.Strict)
            {
                del = _stubs.GetMethodStub<RecentDeals_Country_Int32_Int32_Delegate>("RecentDeals");
            }
            else
            {
                if (!_stubs.TryGetMethodStub<RecentDeals_Country_Int32_Int32_Delegate>("RecentDeals", out del))
                {
                    return default(global::System.IObservable<global::System.Collections.Generic.List<global::GoodGameDeals.Domain.Deal>>);
                }
            }

            return del.Invoke(country, offset, limit);
        }

        public delegate global::System.IObservable<global::System.Collections.Generic.List<global::GoodGameDeals.Domain.Deal>> RecentDeals_Country_Int32_Int32_Delegate(global::GoodGameDeals.Data.Entity.Country country, int offset, int limit);

        public StubIIsThereAnyDealRepository RecentDeals(RecentDeals_Country_Int32_Int32_Delegate del, int count = Times.Forever, bool overwrite = false)
        {
            _stubs.SetMethodStub(del, count, overwrite);
            return this;
        }

        global::System.IObservable<global::System.Collections.Generic.List<global::GoodGameDeals.Domain.Deal>> global::GoodGameDeals.Data.Repositories.IIsThereAnyDealRepository.GameCurrentPrices(string plain, global::GoodGameDeals.Data.Entity.Country country)
        {
            GameCurrentPrices_String_CountryCountryCad_Delegate del;
            if (MockBehavior == MockBehavior.Strict)
            {
                del = _stubs.GetMethodStub<GameCurrentPrices_String_CountryCountryCad_Delegate>("GameCurrentPrices");
            }
            else
            {
                if (!_stubs.TryGetMethodStub<GameCurrentPrices_String_CountryCountryCad_Delegate>("GameCurrentPrices", out del))
                {
                    return default(global::System.IObservable<global::System.Collections.Generic.List<global::GoodGameDeals.Domain.Deal>>);
                }
            }

            return del.Invoke(plain, country);
        }

        public delegate global::System.IObservable<global::System.Collections.Generic.List<global::GoodGameDeals.Domain.Deal>> GameCurrentPrices_String_CountryCountryCad_Delegate(string plain, global::GoodGameDeals.Data.Entity.Country country);

        public StubIIsThereAnyDealRepository GameCurrentPrices(GameCurrentPrices_String_CountryCountryCad_Delegate del, int count = Times.Forever, bool overwrite = false)
        {
            _stubs.SetMethodStub(del, count, overwrite);
            return this;
        }

        public StubIIsThereAnyDealRepository(MockBehavior mockBehavior = MockBehavior.Loose)
        {
            MockBehavior = mockBehavior;
        }
    }
}

namespace GoodGameDeals.Data.Repositories
{
    using System;

    using GoodGameDeals.Data.Entity.Responses.Steam;

    using Windows.UI.Xaml.Media.Imaging;

    [CompilerGenerated]
    public class StubISteamRepository : ISteamRepository
    {
        private readonly StubContainer<StubISteamRepository> _stubs = new StubContainer<StubISteamRepository>();

        public MockBehavior MockBehavior { get; set; }

        global::System.IObservable<global::Windows.UI.Xaml.Media.Imaging.BitmapImage> global::GoodGameDeals.Data.Repositories.ISteamRepository.GameLogo(string title)
        {
            GameLogo_String_Delegate del;
            if (MockBehavior == MockBehavior.Strict)
            {
                del = _stubs.GetMethodStub<GameLogo_String_Delegate>("GameLogo");
            }
            else
            {
                if (!_stubs.TryGetMethodStub<GameLogo_String_Delegate>("GameLogo", out del))
                {
                    return default(global::System.IObservable<global::Windows.UI.Xaml.Media.Imaging.BitmapImage>);
                }
            }

            return del.Invoke(title);
        }

        public delegate global::System.IObservable<global::Windows.UI.Xaml.Media.Imaging.BitmapImage> GameLogo_String_Delegate(string title);

        public StubISteamRepository GameLogo(GameLogo_String_Delegate del, int count = Times.Forever, bool overwrite = false)
        {
            _stubs.SetMethodStub(del, count, overwrite);
            return this;
        }

        global::System.IObservable<global::GoodGameDeals.Data.Entity.Responses.Steam.GetAppListResponse> global::GoodGameDeals.Data.Repositories.ISteamRepository.AppList()
        {
            AppList_Delegate del;
            if (MockBehavior == MockBehavior.Strict)
            {
                del = _stubs.GetMethodStub<AppList_Delegate>("AppList");
            }
            else
            {
                if (!_stubs.TryGetMethodStub<AppList_Delegate>("AppList", out del))
                {
                    return default(global::System.IObservable<global::GoodGameDeals.Data.Entity.Responses.Steam.GetAppListResponse>);
                }
            }

            return del.Invoke();
        }

        public delegate global::System.IObservable<global::GoodGameDeals.Data.Entity.Responses.Steam.GetAppListResponse> AppList_Delegate();

        public StubISteamRepository AppList(AppList_Delegate del, int count = Times.Forever, bool overwrite = false)
        {
            _stubs.SetMethodStub(del, count, overwrite);
            return this;
        }

        public StubISteamRepository(MockBehavior mockBehavior = MockBehavior.Loose)
        {
            MockBehavior = mockBehavior;
        }
    }
}

namespace GoodGameDeals.Data.Repositories.Stores
{
    using System;
    using System.Reactive;

    using Windows.UI.Xaml.Media.Imaging;

    using GoodGameDeals.Data.Entity.Responses.Steam;

    [CompilerGenerated]
    public class StubISteamStore : ISteamStore
    {
        private readonly StubContainer<StubISteamStore> _stubs = new StubContainer<StubISteamStore>();

        public MockBehavior MockBehavior { get; set; }

        global::System.IObservable<global::Windows.UI.Xaml.Media.Imaging.BitmapImage> global::GoodGameDeals.Data.Repositories.Stores.ISteamStore.GameLogo(string title)
        {
            GameLogo_String_Delegate del;
            if (MockBehavior == MockBehavior.Strict)
            {
                del = _stubs.GetMethodStub<GameLogo_String_Delegate>("GameLogo");
            }
            else
            {
                if (!_stubs.TryGetMethodStub<GameLogo_String_Delegate>("GameLogo", out del))
                {
                    return default(global::System.IObservable<global::Windows.UI.Xaml.Media.Imaging.BitmapImage>);
                }
            }

            return del.Invoke(title);
        }

        public delegate global::System.IObservable<global::Windows.UI.Xaml.Media.Imaging.BitmapImage> GameLogo_String_Delegate(string title);

        public StubISteamStore GameLogo(GameLogo_String_Delegate del, int count = Times.Forever, bool overwrite = false)
        {
            _stubs.SetMethodStub(del, count, overwrite);
            return this;
        }

        global::System.IObservable<global::GoodGameDeals.Data.Entity.Responses.Steam.GetAppListResponse> global::GoodGameDeals.Data.Repositories.Stores.ISteamStore.AppList()
        {
            AppList_Delegate del;
            if (MockBehavior == MockBehavior.Strict)
            {
                del = _stubs.GetMethodStub<AppList_Delegate>("AppList");
            }
            else
            {
                if (!_stubs.TryGetMethodStub<AppList_Delegate>("AppList", out del))
                {
                    return default(global::System.IObservable<global::GoodGameDeals.Data.Entity.Responses.Steam.GetAppListResponse>);
                }
            }

            return del.Invoke();
        }

        public delegate global::System.IObservable<global::GoodGameDeals.Data.Entity.Responses.Steam.GetAppListResponse> AppList_Delegate();

        public StubISteamStore AppList(AppList_Delegate del, int count = Times.Forever, bool overwrite = false)
        {
            _stubs.SetMethodStub(del, count, overwrite);
            return this;
        }

        global::System.IObservable<global::System.Reactive.Unit> global::GoodGameDeals.Data.Repositories.Stores.ISteamStore.Initialize()
        {
            Initialize_Delegate del;
            if (MockBehavior == MockBehavior.Strict)
            {
                del = _stubs.GetMethodStub<Initialize_Delegate>("Initialize");
            }
            else
            {
                if (!_stubs.TryGetMethodStub<Initialize_Delegate>("Initialize", out del))
                {
                    return default(global::System.IObservable<global::System.Reactive.Unit>);
                }
            }

            return del.Invoke();
        }

        public delegate global::System.IObservable<global::System.Reactive.Unit> Initialize_Delegate();

        public StubISteamStore Initialize(Initialize_Delegate del, int count = Times.Forever, bool overwrite = false)
        {
            _stubs.SetMethodStub(del, count, overwrite);
            return this;
        }

        public StubISteamStore(MockBehavior mockBehavior = MockBehavior.Loose)
        {
            MockBehavior = mockBehavior;
        }
    }
}

namespace GoodGameDeals.Data.Repositories.Stores
{
    using System;
    using System.Collections.Generic;
    using System.Reactive;

    using GoodGameDeals.Data.Entity;
    using GoodGameDeals.Data.Entity.Responses.IsThereAnyDeal;

    [CompilerGenerated]
    public class StubIIsThereAnyDealStore : IIsThereAnyDealStore
    {
        private readonly StubContainer<StubIIsThereAnyDealStore> _stubs = new StubContainer<StubIIsThereAnyDealStore>();

        public MockBehavior MockBehavior { get; set; }

        global::System.IObservable<global::GoodGameDeals.Data.Entity.Responses.IsThereAnyDeal.RecentDealsResponse> global::GoodGameDeals.Data.Repositories.Stores.IIsThereAnyDealStore.RecentDeals(global::GoodGameDeals.Data.Entity.Country country, int offset, int limit)
        {
            RecentDeals_Country_Int32_Int32_Delegate del;
            if (MockBehavior == MockBehavior.Strict)
            {
                del = _stubs.GetMethodStub<RecentDeals_Country_Int32_Int32_Delegate>("RecentDeals");
            }
            else
            {
                if (!_stubs.TryGetMethodStub<RecentDeals_Country_Int32_Int32_Delegate>("RecentDeals", out del))
                {
                    return default(global::System.IObservable<global::GoodGameDeals.Data.Entity.Responses.IsThereAnyDeal.RecentDealsResponse>);
                }
            }

            return del.Invoke(country, offset, limit);
        }

        public delegate global::System.IObservable<global::GoodGameDeals.Data.Entity.Responses.IsThereAnyDeal.RecentDealsResponse> RecentDeals_Country_Int32_Int32_Delegate(global::GoodGameDeals.Data.Entity.Country country, int offset, int limit);

        public StubIIsThereAnyDealStore RecentDeals(RecentDeals_Country_Int32_Int32_Delegate del, int count = Times.Forever, bool overwrite = false)
        {
            _stubs.SetMethodStub(del, count, overwrite);
            return this;
        }

        global::System.IObservable<global::GoodGameDeals.Data.Entity.Responses.IsThereAnyDeal.CurrentPricesResponse> global::GoodGameDeals.Data.Repositories.Stores.IIsThereAnyDealStore.CurrentPrices(string plain, global::GoodGameDeals.Data.Entity.Country country)
        {
            CurrentPrices_String_CountryCountryCad_Delegate del;
            if (MockBehavior == MockBehavior.Strict)
            {
                del = _stubs.GetMethodStub<CurrentPrices_String_CountryCountryCad_Delegate>("CurrentPrices");
            }
            else
            {
                if (!_stubs.TryGetMethodStub<CurrentPrices_String_CountryCountryCad_Delegate>("CurrentPrices", out del))
                {
                    return default(global::System.IObservable<global::GoodGameDeals.Data.Entity.Responses.IsThereAnyDeal.CurrentPricesResponse>);
                }
            }

            return del.Invoke(plain, country);
        }

        public delegate global::System.IObservable<global::GoodGameDeals.Data.Entity.Responses.IsThereAnyDeal.CurrentPricesResponse> CurrentPrices_String_CountryCountryCad_Delegate(string plain, global::GoodGameDeals.Data.Entity.Country country);

        public StubIIsThereAnyDealStore CurrentPrices(CurrentPrices_String_CountryCountryCad_Delegate del, int count = Times.Forever, bool overwrite = false)
        {
            _stubs.SetMethodStub(del, count, overwrite);
            return this;
        }

        global::System.IObservable<global::System.Reactive.Unit> global::GoodGameDeals.Data.Repositories.Stores.IIsThereAnyDealStore.Initialize()
        {
            Initialize_Delegate del;
            if (MockBehavior == MockBehavior.Strict)
            {
                del = _stubs.GetMethodStub<Initialize_Delegate>("Initialize");
            }
            else
            {
                if (!_stubs.TryGetMethodStub<Initialize_Delegate>("Initialize", out del))
                {
                    return default(global::System.IObservable<global::System.Reactive.Unit>);
                }
            }

            return del.Invoke();
        }

        public delegate global::System.IObservable<global::System.Reactive.Unit> Initialize_Delegate();

        public StubIIsThereAnyDealStore Initialize(Initialize_Delegate del, int count = Times.Forever, bool overwrite = false)
        {
            _stubs.SetMethodStub(del, count, overwrite);
            return this;
        }

        public StubIIsThereAnyDealStore(MockBehavior mockBehavior = MockBehavior.Loose)
        {
            MockBehavior = mockBehavior;
        }
    }
}

namespace GoodGameDeals.Gateways.Contracts
{
    [CompilerGenerated]
    public class StubIIsThereAnyDealStore : IIsThereAnyDealStore
    {
        private readonly StubContainer<StubIIsThereAnyDealStore> _stubs = new StubContainer<StubIIsThereAnyDealStore>();

        public MockBehavior MockBehavior { get; set; }

        public StubIIsThereAnyDealStore(MockBehavior mockBehavior = MockBehavior.Loose)
        {
            MockBehavior = mockBehavior;
        }
    }
}

namespace GoodGameDeals.Gateways.Stores
{
    [CompilerGenerated]
    public class StubISteamStore : ISteamStore
    {
        private readonly StubContainer<StubISteamStore> _stubs = new StubContainer<StubISteamStore>();

        public MockBehavior MockBehavior { get; set; }

        public StubISteamStore(MockBehavior mockBehavior = MockBehavior.Loose)
        {
            MockBehavior = mockBehavior;
        }
    }
}

namespace GoodGameDeals.Services.SettingsServices
{
    using System;

    using Windows.UI.Xaml;

    [CompilerGenerated]
    public class StubISettingsService : ISettingsService
    {
        private readonly StubContainer<StubISettingsService> _stubs = new StubContainer<StubISettingsService>();

        public MockBehavior MockBehavior { get; set; }

        global::Windows.UI.Xaml.ApplicationTheme global::GoodGameDeals.Services.SettingsServices.ISettingsService.AppTheme
        {
            get
            {
                {
                    AppTheme_Get_Delegate del;
                    if (MockBehavior == MockBehavior.Strict)
                    {
                        del = _stubs.GetMethodStub<AppTheme_Get_Delegate>("get_AppTheme");
                    }
                    else
                    {
                        if (!_stubs.TryGetMethodStub<AppTheme_Get_Delegate>("get_AppTheme", out del))
                        {
                            return default(global::Windows.UI.Xaml.ApplicationTheme);
                        }
                    }

                    return del.Invoke();
                }
            }

            set
            {
                {
                    AppTheme_Set_Delegate del;
                    if (MockBehavior == MockBehavior.Strict)
                    {
                        del = _stubs.GetMethodStub<AppTheme_Set_Delegate>("set_AppTheme");
                    }
                    else
                    {
                        if (!_stubs.TryGetMethodStub<AppTheme_Set_Delegate>("set_AppTheme", out del))
                        {
                            return;
                        }
                    }

                    del.Invoke(value);
                }
            }
        }

        global::System.TimeSpan global::GoodGameDeals.Services.SettingsServices.ISettingsService.CacheMaxDuration
        {
            get
            {
                {
                    CacheMaxDuration_Get_Delegate del;
                    if (MockBehavior == MockBehavior.Strict)
                    {
                        del = _stubs.GetMethodStub<CacheMaxDuration_Get_Delegate>("get_CacheMaxDuration");
                    }
                    else
                    {
                        if (!_stubs.TryGetMethodStub<CacheMaxDuration_Get_Delegate>("get_CacheMaxDuration", out del))
                        {
                            return default(global::System.TimeSpan);
                        }
                    }

                    return del.Invoke();
                }
            }

            set
            {
                {
                    CacheMaxDuration_Set_Delegate del;
                    if (MockBehavior == MockBehavior.Strict)
                    {
                        del = _stubs.GetMethodStub<CacheMaxDuration_Set_Delegate>("set_CacheMaxDuration");
                    }
                    else
                    {
                        if (!_stubs.TryGetMethodStub<CacheMaxDuration_Set_Delegate>("set_CacheMaxDuration", out del))
                        {
                            return;
                        }
                    }

                    del.Invoke(value);
                }
            }
        }

        bool global::GoodGameDeals.Services.SettingsServices.ISettingsService.UseShellBackButton
        {
            get
            {
                {
                    UseShellBackButton_Get_Delegate del;
                    if (MockBehavior == MockBehavior.Strict)
                    {
                        del = _stubs.GetMethodStub<UseShellBackButton_Get_Delegate>("get_UseShellBackButton");
                    }
                    else
                    {
                        if (!_stubs.TryGetMethodStub<UseShellBackButton_Get_Delegate>("get_UseShellBackButton", out del))
                        {
                            return default(bool);
                        }
                    }

                    return del.Invoke();
                }
            }

            set
            {
                {
                    UseShellBackButton_Set_Delegate del;
                    if (MockBehavior == MockBehavior.Strict)
                    {
                        del = _stubs.GetMethodStub<UseShellBackButton_Set_Delegate>("set_UseShellBackButton");
                    }
                    else
                    {
                        if (!_stubs.TryGetMethodStub<UseShellBackButton_Set_Delegate>("set_UseShellBackButton", out del))
                        {
                            return;
                        }
                    }

                    del.Invoke(value);
                }
            }
        }

        public delegate global::Windows.UI.Xaml.ApplicationTheme AppTheme_Get_Delegate();

        public StubISettingsService AppTheme_Get(AppTheme_Get_Delegate del, int count = Times.Forever, bool overwrite = false)
        {
            _stubs.SetMethodStub(del, count, overwrite);
            return this;
        }

        public delegate void AppTheme_Set_Delegate(global::Windows.UI.Xaml.ApplicationTheme value);

        public StubISettingsService AppTheme_Set(AppTheme_Set_Delegate del, int count = Times.Forever, bool overwrite = false)
        {
            _stubs.SetMethodStub(del, count, overwrite);
            return this;
        }

        public delegate global::System.TimeSpan CacheMaxDuration_Get_Delegate();

        public StubISettingsService CacheMaxDuration_Get(CacheMaxDuration_Get_Delegate del, int count = Times.Forever, bool overwrite = false)
        {
            _stubs.SetMethodStub(del, count, overwrite);
            return this;
        }

        public delegate void CacheMaxDuration_Set_Delegate(global::System.TimeSpan value);

        public StubISettingsService CacheMaxDuration_Set(CacheMaxDuration_Set_Delegate del, int count = Times.Forever, bool overwrite = false)
        {
            _stubs.SetMethodStub(del, count, overwrite);
            return this;
        }

        public delegate bool UseShellBackButton_Get_Delegate();

        public StubISettingsService UseShellBackButton_Get(UseShellBackButton_Get_Delegate del, int count = Times.Forever, bool overwrite = false)
        {
            _stubs.SetMethodStub(del, count, overwrite);
            return this;
        }

        public delegate void UseShellBackButton_Set_Delegate(bool value);

        public StubISettingsService UseShellBackButton_Set(UseShellBackButton_Set_Delegate del, int count = Times.Forever, bool overwrite = false)
        {
            _stubs.SetMethodStub(del, count, overwrite);
            return this;
        }

        public StubISettingsService(MockBehavior mockBehavior = MockBehavior.Loose)
        {
            MockBehavior = mockBehavior;
        }
    }
}