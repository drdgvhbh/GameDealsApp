using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace GoodGameDeals.Controls {
    public sealed partial class TabHeader : UserControl {
        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register(
                "Title",
                typeof(string),
                typeof(TabHeader),
                new PropertyMetadata("Title"));

        public TabHeader() {
            this.InitializeComponent();
        }

        public string Title {
            get {
                return (string)this.GetValue(TitleProperty);
            }

            set {
                this.SetValue(TitleProperty, value);
            }
        }
    }
}
