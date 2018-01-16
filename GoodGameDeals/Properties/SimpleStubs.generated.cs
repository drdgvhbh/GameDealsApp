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

namespace GoodGameDeals.Threading
{
    using System;
    using System.Threading.Tasks;

    [CompilerGenerated]
    public class StubITaskDelay : ITaskDelay
    {
        private readonly StubContainer<StubITaskDelay> _stubs = new StubContainer<StubITaskDelay>();

        public MockBehavior MockBehavior { get; set; }

        global::System.Threading.Tasks.Task global::GoodGameDeals.Threading.ITaskDelay.Delay(global::System.TimeSpan delay)
        {
            Delay_TimeSpan_Delegate del;
            if (MockBehavior == MockBehavior.Strict)
            {
                del = _stubs.GetMethodStub<Delay_TimeSpan_Delegate>("Delay");
            }
            else
            {
                if (!_stubs.TryGetMethodStub<Delay_TimeSpan_Delegate>("Delay", out del))
                {
                    return Task.FromResult(true);
                }
            }

            return del.Invoke(delay);
        }

        public delegate global::System.Threading.Tasks.Task Delay_TimeSpan_Delegate(global::System.TimeSpan delay);

        public StubITaskDelay Delay(Delay_TimeSpan_Delegate del, int count = Times.Forever, bool overwrite = false)
        {
            _stubs.SetMethodStub(del, count, overwrite);
            return this;
        }

        public StubITaskDelay(MockBehavior mockBehavior = MockBehavior.Loose)
        {
            MockBehavior = mockBehavior;
        }
    }
}