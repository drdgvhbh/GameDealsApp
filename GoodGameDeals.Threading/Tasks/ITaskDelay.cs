namespace GoodGameDeals.Threading {
    using System;
    using System.Threading.Tasks;

    public interface ITaskDelay {
        Task Delay(TimeSpan delay);
    }
}
