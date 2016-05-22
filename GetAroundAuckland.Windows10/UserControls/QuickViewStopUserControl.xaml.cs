using Services.NavigationService;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Microsoft.Practices.Unity;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Core;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using GetAroundAuckland.Windows10.Models;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace GetAroundAuckland.Windows10.UserControls
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class QuickViewStopUserControl : Page
    {
        private INavigationService _nav;

        public static readonly DependencyProperty StopQuickViewProperty =
            DependencyProperty.Register("Stop", typeof(Stop), typeof(QuickViewStopUserControl), null);

        public static readonly DependencyProperty SequenceQuickViewProperty =
            DependencyProperty.Register("Sequence", typeof(int?), typeof(QuickViewStopUserControl), null);

        public Stop Stop
        {
            get { return this.GetValue(StopQuickViewProperty) as Stop; }
            set { this.SetValueDp(StopQuickViewProperty, value); }
        }

        public int? Sequence
        {
            get { return this.GetValue(SequenceQuickViewProperty) as int?; }
            set { this.SetValueDp(SequenceQuickViewProperty, value); }
        }

        public EventHandler BackToMapButtonTapped;

        public QuickViewStopUserControl(Stop stop, int sequence)
        {
            this.InitializeComponent();
            _nav = App.Container.Resolve<NavigationService>();

            (this.Content as FrameworkElement).DataContext = this;

            PopupGrid.Width = Window.Current.Bounds.Width;
            PopupGrid.Height = Window.Current.Bounds.Height;

            Stop = stop;
            Sequence = sequence;
            Window.Current.SizeChanged += Current_SizeChanged;
        }

        private void Current_SizeChanged(object sender, WindowSizeChangedEventArgs e)
        {
            PopupGrid.Width = Window.Current.Bounds.Width;
            PopupGrid.Height = Window.Current.Bounds.Height;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void SetValueDp(DependencyProperty property, object value, [CallerMemberName]string propertyName = null)
        {
            SetValue(property, value);
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        private void BackToMapButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
            BackToMapButtonTapped.Invoke(sender, EventArgs.Empty);
        }

        private void MoreDetailsButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
            BackToMapButtonTapped.Invoke(sender, EventArgs.Empty);
            _nav.Navigate(App.Experiences.Stop.ToString(), Stop.Id);
        }
    }
}
