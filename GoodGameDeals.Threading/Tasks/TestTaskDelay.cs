namespace GoodGameDeals.Threading.Tasks {
    using System;
    using System.Threading.Tasks;

    public class TestTaskDelay : ITaskDelay {
        private readonly TimeSpan delay;

        public TestTaskDelay() {
            this.delay = TimeSpan.Zero;
        }

        public TestTaskDelay(TimeSpan delay) {
            this.delay = delay;
        }

        public Task Delay(TimeSpan delay) {
            return Task.Delay(this.delay);
        }
    }
}
