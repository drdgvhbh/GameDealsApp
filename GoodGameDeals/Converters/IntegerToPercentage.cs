using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodGameDeals.Converters {
    using System.Globalization;

    using Windows.UI.Xaml.Data;


    public class IntegerToPercentage : IValueConverter {
        public object Convert(
                object value,
                Type targetType,
                object parameter,
                string language) {
            var text = ((long)value / 100.0).ToString("P0", CultureInfo.CurrentCulture);
            if (parameter is string b && bool.Parse(b)) {
                text = "-" + text;
            }
            return text;
        }

        public object ConvertBack(
                object value,
                Type targetType,
                object parameter,
                string language) {
            throw new NotImplementedException();
        }
    }
}
