namespace GoodGameDeals.Domain.Mappers {
    using AutoMapper;

    using GoodGameDeals.Data.ApiResponses.IsThereAnyDeal;

    public class
        RecentDealsResponseListDealConverter : ITypeConverter<
            RecentDealsResponse.Deal, Deal> {
        public Deal Convert(
                RecentDealsResponse.Deal source,
                Deal destination,
                ResolutionContext context) {
            var deal = new Deal
            {
                DealUrl =
                    new DealUrl
                    {
                        Game = source.Urls.Game,
                        Buy = source.Urls.Buy
                    },
                Added = source.Added,
                Drm = source.Drm,
                Plain = source.Plain,
                PriceCut = source.PriceCut,
                PriceNew = source.PriceNew,
                PriceOld = source.PriceOld,
                Shop =
                    new Shop { Id = source.Shop.Id, Name = source.Shop.Name },
                Title = source.Title
            };
            return deal;
        }
    }
}
