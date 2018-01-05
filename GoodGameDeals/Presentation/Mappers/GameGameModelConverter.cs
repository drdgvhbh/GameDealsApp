namespace GoodGameDeals.Presentation.Mappers {
    using System.Collections.ObjectModel;
    using System.Text.RegularExpressions;

    using Windows.UI.Xaml.Media.Imaging;

    using AutoMapper;

    using GoodGameDeals.Core.Entities;
    using GoodGameDeals.Models;

    using NullGuard;

    public class GameGameModelConverter : ITypeConverter<Game, GameModel> {
        public GameModel Convert(
                Game source,
                [AllowNull]GameModel destination,
                ResolutionContext context) {
            var regex = new Regex(@":|-");
            var gameHeader = regex.Split(source.GameTitle, 2);
            var subtitle = gameHeader.Length == 2
                               ? gameHeader[1].Trim()
                               : string.Empty;
            var deals = new ObservableCollection<DealModel>();
            var counter = 0;
            foreach (var deal in source.Deals) {
                if (counter > 2) {
                    break;
                }
                deals.Add(
                    new DealModel(
                        deal.Url,
                        (long)(deal.Discount.PriceDiscountPercentage * 100),
                        deal.Discount.PriceOld,
                        deal.Discount.PriceNew,
                        deal.Store.Name));
                counter++;
            }
            return new GameModel(
                    source.Deals[0].DateAdded,
                    gameHeader[0].Trim(),
                    subtitle,
                    new BitmapImage(source.GameLogo),
                    deals);
        }
    }
}
