namespace GoodGameDeals.Threading.Tasks {
    using System;
    using System.Threading.Tasks;

    public class ProductionTaskDelay : ITaskDelay {
        public Task Delay(TimeSpan delay) {
            return Task.Delay(delay);
        }
    }
}
