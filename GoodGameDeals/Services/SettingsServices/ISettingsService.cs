namespace GoodGameDeals.Services.SettingsServices {
    using System;

    using Windows.UI.Xaml;

    public interface ISettingsService {
        /// <summary>
        ///     Gets or sets the application theme.
        /// </summary>
        ApplicationTheme AppTheme { get; set; }

        TimeSpan CacheMaxDuration { get; set; }

        bool UseShellBackButton { get; set; }
    }
}
