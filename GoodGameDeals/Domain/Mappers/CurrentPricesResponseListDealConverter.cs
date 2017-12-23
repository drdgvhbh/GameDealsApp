namespace GoodGameDeals.Domain.Mappers {
    using System;

    using AutoMapper;

    using GoodGameDeals.Data.Entity.Responses.IsThereAnyDeal;

    public class
        CurrentPricesResponseListDealConverter : ITypeConverter<
            CurrentPricesResponse.List, Deal> {
        public Deal Convert(
                CurrentPricesResponse.List source,
                Deal destination,
                ResolutionContext context) {
            var deal = new Deal()
            {
                DealUrl = new DealUrl()
                {
                    Game = string.Empty,
                    Buy = source.Url
                },
                Added = DateTime.MinValue.Millisecond,
                Drm = source.Drm,
                Plain = string.Empty,
                PriceCut = source.PriceCut,
                PriceNew = source.PriceNew,
                PriceOld = source.PriceOld,
                Shop = new Shop()
                {
                    Id = source.Shop.Id,
                    Name = source.Shop.Name
                },
                Title = string.Empty
            };
            return deal;
        }
    }
}