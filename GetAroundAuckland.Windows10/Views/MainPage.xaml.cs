using GetAroundAuckland.Windows10.Controls;
using GetAroundAuckland.Windows10.Interfaces;
using GetAroundAuckland.Windows10.Models;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace GetAroundAuckland.Windows10.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private PerfectListView _routesListView;
        private PerfectListView _stopsListView;

        private IMainPageViewModel _vm;
        public MainPage()
        {
            this.InitializeComponent();

            _vm = (IMainPageViewModel)DataContext;
            NavigationCacheMode = Windows.UI.Xaml.Navigation.NavigationCacheMode.Enabled;
        }

        private void RouteAutoSuggestBox_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            if (args.Reason == AutoSuggestionBoxTextChangeReason.UserInput)
            {
                var text = sender.Text;
                sender.ItemsSource =_vm.FilterRoutes(text);                
            }
        }

        private void RouteAutoSuggestBox_QuerySubmitted(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
        {
            if (args.ChosenSuggestion != null)
            {
                var route = args.ChosenSuggestion as Route;
                SelectRoute(sender, route);
            }
            else
            {
                var matchingRoutes = _vm.FilterRoutes(args.QueryText);

                if (matchingRoutes.Count() >= 1)
                {
                    var route = matchingRoutes.FirstOrDefault();
                    SelectRoute(sender, route);
                }
            }
        }

        private void RouteAutoSuggestBox_SuggestionChosen(AutoSuggestBox sender, AutoSuggestBoxSuggestionChosenEventArgs args)
        {
            var route = args.SelectedItem as Route;
            SelectRoute(sender, route);

            if (_routesListView.Items.Any())
            {
                _routesListView.ScrollIntoView(_routesListView.Items.Last());
                _routesListView.ScrollIntoView(route);
            }
        }

        private void SelectRoute(AutoSuggestBox sender, Route route)
        {
            sender.Text = string.Format("{0} {1} - {2}", route.AgencyId, route.ShortName, route.LongName);
        }

        private void RoutesListView_Loaded(object sender, RoutedEventArgs e)
        {
            _routesListView = (PerfectListView)sender;
        }

        private void StopAutoSuggestBox_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            if (args.Reason == AutoSuggestionBoxTextChangeReason.UserInput)
            {
                var text = sender.Text;
                sender.ItemsSource = _vm.FilterStops(text);
            }
        }

        private void StopAutoSuggestBox_QuerySubmitted(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
        {
            if (args.ChosenSuggestion != null)
            {
                var stop = args.ChosenSuggestion as Stop;
                SelectStop(sender, stop);
            }
            else
            {
                var matchingStops = _vm.FilterStops(args.QueryText);

                if (matchingStops.Count() >= 1)
                {
                    var stop = matchingStops.FirstOrDefault();
                    SelectStop(sender, stop);
                }
            }        
        }

        private void StopAutoSuggestBox_SuggestionChosen(AutoSuggestBox sender, AutoSuggestBoxSuggestionChosenEventArgs args)
        {
            var stop = args.SelectedItem as Stop;
            SelectStop(sender, stop);

            if (_stopsListView.Items.Any())
            {
                _stopsListView.ScrollIntoView(_stopsListView.Items.Last());
                _stopsListView.ScrollIntoView(stop);
            }
        }

        private void SelectStop(AutoSuggestBox sender, Stop stop)
        {
            sender.Text = string.Format("{0} {1}", stop.Id, stop.Name);
        }

        private void StopsListView_Loaded(object sender, RoutedEventArgs e)
        {
            _stopsListView = (PerfectListView)sender;
        }

        //private void NavigationChanged(Page page)
        //{
        //    if (SplitViewContent.Content != null && SplitViewContent.Content.GetType() == page.GetType())
        //        return;

        //    SplitViewContent.Navigate(page.GetType());
        //}
    }
}
