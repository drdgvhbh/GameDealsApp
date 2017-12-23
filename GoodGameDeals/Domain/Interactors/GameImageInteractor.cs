using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodGameDeals.Domain.Interactors {
    using Windows.UI.Xaml.Media.Imaging;

    public class GameImageInteractor : AbstractInteractor<BitmapImage, object> {
        public override IObservable<BitmapImage> UseCaseObservable(
            object parameters) {
            throw new NotImplementedException();
        }
    }
}
