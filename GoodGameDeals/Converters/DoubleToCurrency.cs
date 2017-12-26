namespace GoodGameDeals.Converters {
    using System;
    using System.Diagnostics;
    using System.Globalization;

    using Windows.UI.Xaml.Data;

    using NullGuard;

    [Bindable]
    public class DoubleToCurrency : IValueConverter {
        public object Convert(
                object value,
                [AllowNull]Type targetType,
                [AllowNull]object parameter,
                [AllowNull]string language) {
            return ((double)value).ToString("C", CultureInfo.CurrentCulture);
        }

        public object ConvertBack(
                object value,
                [AllowNull]Type targetType,
                [AllowNull]object parameter,
                [AllowNull]string language) {
            return 100;
        }
    }
}
