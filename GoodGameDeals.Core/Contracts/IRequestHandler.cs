namespace GoodGameDeals.Core.Contracts {
    using System;
    using System.Threading.Tasks;

    public interface IRequestHandler<in TRequest, TResponse> {
        Task<TResponse> Handle(TRequest request);
    }
}