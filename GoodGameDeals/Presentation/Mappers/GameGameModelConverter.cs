namespace GoodGameDeals.Presentation.Mappers {
    using System.Collections.ObjectModel;
    using System.Text.RegularExpressions;

    using AutoMapper;

    using GoodGameDeals.Domain;
    using GoodGameDeals.Models;
    public class GameGameModelConverter : ITypeConverter<Game, GameModel> {
        public GameModel Convert(
                Game source,
                GameModel destination,
                ResolutionContext context) {
            var regex = new Regex(@":|-");
            var gameHeader = regex.Split(source.GameTitle, 2);
            var subtitle = gameHeader.Length == 2
                               ? gameHeader[1].Trim()
                               : string.Empty;
            var deals = new ObservableCollection<DealModel>();
            var counter = 0;
            foreach (var deal in source.DealsList) {
                if (counter > 2) {
                    break;
                }
                deals.Add(
                    new DealModel(
                        deal.DealUrl.Buy,
                        deal.PriceCut,
                        deal.PriceOld,
                        deal.PriceNew,
                        deal.Shop.Name));
                counter++;
            }
            return new GameModel(
                    source.Id,
                    gameHeader[0].Trim(),
                    subtitle,
                    source.GameImage,
                    deals);
        }
    }
}
