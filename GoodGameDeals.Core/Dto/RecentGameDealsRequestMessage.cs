namespace GoodGameDeals.Core.Dto {
    using Contracts;

    public class RecentGameDealsRequestMessage : IRequest {
        public int Quantity { get; }

        public int Offset { get; }

        public RecentGameDealsRequestMessage(int quantity = 0, int offset = 0) {
            this.Quantity = quantity;
            this.Offset = offset;
        }
    }
}