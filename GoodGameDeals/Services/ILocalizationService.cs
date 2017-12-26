namespace GoodGameDeals.Services {
    using System.Globalization;

    public interface ILocalizationService {
        CultureInfo CurrentCulture { get; set; }

        CultureInfo CurrentUiCulture { get; set; }

        RegionInfo CurrentRegion { get; set; }
    }
}
