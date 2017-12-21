namespace GoodGameDeals.Presentation.Mappers {
    using AutoMapper;

    using GoodGameDeals.Domain;
    using GoodGameDeals.Models;

    public class DealDealModelConverter : ITypeConverter<Deal, DealModel> {
        public DealModel Convert(
            Deal source,
            DealModel destination,
            ResolutionContext context) {
            return new DealModel(
                source.DealUrl.Buy,
                source.PriceCut,
                source.PriceOld,
                source.PriceNew,
                source.Shop.Name);
        }
    }
}