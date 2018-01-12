// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace GoodGameDeals.Presentation.Controls {
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    using Windows.ApplicationModel;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;

    using GoodGameDeals.Annotations;
    using GoodGameDeals.Models;

    using Microsoft.Toolkit.Uwp.UI;

    using WinRTXamlToolkit.Controls.DataVisualization.Charting;

    public sealed partial class GameDetailControl : UserControl {
        public static readonly DependencyProperty DealsListProperty =
            DependencyProperty.Register(
                "DealsList",
                typeof(ObservableCollection<DealModel>),
                typeof(GameDetailControl),
                new PropertyMetadata(new ObservableCollection<DealModel>()));

        public static readonly DependencyProperty GameTitleProperty =
            DependencyProperty.Register(
                "GameTitle",
                typeof(string),
                typeof(GameDetailControl),
                new PropertyMetadata(string.Empty));

        public GameDetailControl() {
            this.InitializeComponent();
            if (DesignMode.DesignModeEnabled) {
                return;
            }

            this.Loading += (sender, args) =>
                {
                    var rand = new Random();
                    System.Diagnostics.Debug.WriteLine(this.DealsList.GetHashCode());
                    this.DealsList.CollectionChanged += (sender2, args2) =>
                        {
                            var records = new List<Records>();
                            foreach (var deal in DealsList) {
                                records.Add(new Records() { Name = deal.Store, Value = deal.GamePrice});
                            }
                            var oldPrice = new List<Records>();
                            foreach (var deal in DealsList) {
                                oldPrice.Add(new Records() { Name = deal.Store, Value = deal.GamePriceOld});
                            }
                            ((StackedColumnSeries)this.GameDealChart.Series[0])
                                .SeriesDefinitions[0].ItemsSource = records;
                            ((StackedColumnSeries)this.GameDealChart.Series[0])
                                .SeriesDefinitions[1].ItemsSource = oldPrice;
                            this.GameDealChart.Title = this.GameTitle;
                            var woop = this.GameDealChart.Axes;
                            //this.GameDealChart.Axes[0].
                            var squinf = this.GameDealChart.TitleStyle;
                            //squinf.Setters.
                        };
                };
        }




        public class Records : INotifyPropertyChanged {
            private double value;

            public string Name { get; set; }

            public double Value {
                get {
                    return this.value;
                }
                set {
                    if (this.value != value) {
                        this.value = value;
                        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Value)));
                    }
                }
            }

            public event PropertyChangedEventHandler PropertyChanged;

            [NotifyPropertyChangedInvocator]
            protected virtual void OnPropertyChanged(
                [CallerMemberName] string propertyName = null) {
                this.PropertyChanged?.Invoke(
                    this,
                    new PropertyChangedEventArgs(propertyName));
            }
        }

        public string GameTitle {
            get => (string)this.GetValue(GameTitleProperty);
            set => this.SetValue(GameTitleProperty, value);
        }

        public ObservableCollection<DealModel> DealsList {
            get => (ObservableCollection<DealModel>)this.GetValue(DealsListProperty);

            set => this.SetValue(DealsListProperty, value);
        }
    }
}