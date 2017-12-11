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

namespace Hamburger1.Controls {
    public sealed partial class GenericGameDealControl : UserControl {
        public GenericGameDealControl() {
            this.InitializeComponent();
        }

        public string GameTitle {
            get {
                return (string)this.GetValue(GameTitleProperty);
            }
            set {
                this.SetValue(GameTitleProperty, value);
            }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty GameTitleProperty =
            DependencyProperty.Register(
                "GameTitle",
                typeof(string),
                typeof(GenericGameDealControl),
                new PropertyMetadata(0));
    }
}
