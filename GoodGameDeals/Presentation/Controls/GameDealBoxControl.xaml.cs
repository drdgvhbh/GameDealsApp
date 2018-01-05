namespace GoodGameDeals.Presentation.Controls {
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;

    public sealed partial class GameDealBoxControl : UserControl {
        public static readonly DependencyProperty GameTitleProperty =
            DependencyProperty.Register(
                "GameTitle",
                typeof(string),
                typeof(GameDealBoxControl),
                new PropertyMetadata("Game Title"));

        public GameDealBoxControl() {
            this.InitializeComponent();
        }

        public string GameTitle {
            get => (string)this.GetValue(GameTitleProperty);

            set => this.SetValue(GameTitleProperty, value);
        }
    }
}
