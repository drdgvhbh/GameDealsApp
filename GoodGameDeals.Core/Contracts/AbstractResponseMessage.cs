namespace GoodGameDeals.Core.Contracts {
    using System;

    public abstract class AbstractResponseMessage {
        protected AbstractResponseMessage(bool isSuccessful, string message) {
            this.IsSuccessful = isSuccessful;
            this.Message = message ?? throw new ArgumentNullException(nameof(message));
        }

        protected AbstractResponseMessage(bool isSuccessful)
            : this(isSuccessful, string.Empty) {
        }

        public bool IsSuccessful { get; }

        public string Message { get; }
    }
}