namespace GoodGameDeals.Core.Entities {
    public class Store {
        public Store(string id, string Name) {
            this.Id = id;
            this.Name = Name;
        }

        public string Id { get; }

        public string Name { get; }
    }
}
