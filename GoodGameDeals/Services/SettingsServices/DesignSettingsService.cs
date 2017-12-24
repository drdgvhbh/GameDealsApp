namespace GoodGameDeals.Services.SettingsServices {
    using System;

    using Windows.UI.Xaml;

    public class DesignSettingsService : ISettingsService {
        public ApplicationTheme AppTheme {
            get => ApplicationTheme.Dark;
            set => throw new NotSupportedException();
        }

        public TimeSpan CacheMaxDuration { get; set; }

        public bool UseShellBackButton { get; set; }
    }
}
