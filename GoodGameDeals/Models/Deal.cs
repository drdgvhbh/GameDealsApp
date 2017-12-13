using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodGameDeals.Models {
    using Windows.UI.Xaml.Media.Imaging;

    public class Deal {
        public string GameTitle { get; private set; }

        public string GamePrice { get; private set; }

        public string GamePriceOld { get; private set; }

        public string Discount { get; private set; }

        public BitmapImage Image { get; private set; }

/*        public Deal() : this(string.Empty) {

        }

        public Deal(string gameTitle) : this(gameTitle, "$0.00") {
        }

        public Deal(string gameTitle, string gamePrice)
                : this(gameTitle, gamePrice, "$0.00") {
        }

        public Deal(string gameTitle, string gamePrice, string gamePriceOld)
            : this(gameTitle, gamePrice, gamePriceOld, "-0%") {
        }*/

        public Deal(string gameTitle, string gamePrice, string gamePriceOld, string discount, BitmapImage image) {
            this.GameTitle = gameTitle;
            this.GamePrice = gamePrice;
            this.GamePriceOld = gamePriceOld;
            this.Discount = discount;
            this.Image = image;
        }
    }
}
