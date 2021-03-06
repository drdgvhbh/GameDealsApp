﻿namespace GoodGameDeals.Converters {
    using System;
    using System.Diagnostics;
    using System.Globalization;

    using Windows.UI.Xaml.Data;

    [Bindable]
    public class DoubleToCurrency : IValueConverter {
        public object Convert(
                object value,
                Type targetType,
                object parameter,
                string language) {
            return ((double)value).ToString("C", CultureInfo.CurrentCulture);
        }

        public object ConvertBack(
                object value,
                Type targetType,
                object parameter,
                string language) {
            return 100;
        }
    }
}
