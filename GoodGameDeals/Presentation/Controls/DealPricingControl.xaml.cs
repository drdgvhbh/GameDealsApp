namespace GoodGameDeals.Presentation.Controls {
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;

    public sealed partial class DealPricingControl : UserControl {
        public static readonly DependencyProperty DiscountProperty =
            DependencyProperty.Register(
                "Discount",
                typeof(string),
                typeof(DealPricingControl),
                new PropertyMetadata("-0%"));

        public static readonly DependencyProperty GamePriceOldProperty =
            DependencyProperty.Register(
                "GamePriceOld",
                typeof(string),
                typeof(DealPricingControl),
                new PropertyMetadata("$0.00"));

        public static readonly DependencyProperty GamePriceProperty =
            DependencyProperty.Register(
                "GamePrice",
                typeof(string),
                typeof(DealPricingControl),
                new PropertyMetadata("$0.00"));

        public DealPricingControl() {
            this.InitializeComponent();
        }

        public string GamePrice {
            get {
                return (string)this.GetValue(GamePriceProperty);
            }

            set {
                this.SetValue(GamePriceProperty, value);
            }
        }

        public string GamePriceOld {
            get {
                return (string)this.GetValue(GamePriceOldProperty);
            }

            set {
                this.SetValue(GamePriceOldProperty, value);
            }
        }

        public string Discount {
            get {
                return (string)this.GetValue(DiscountProperty);
            }

            set {
                this.SetValue(DiscountProperty, value);
            }
        }
    }
}