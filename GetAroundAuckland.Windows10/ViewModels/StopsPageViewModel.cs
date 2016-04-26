using GetAroundAuckland.Windows10.Helpers;
using GetAroundAuckland.Windows10.Interfaces;
using GetAroundAuckland.Windows10.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetAroundAuckland.Windows10.ViewModels
{
    public class StopsPageViewModel : BaseViewModel, IStopsPageViewModel
    {
        private bool _isLoading;
        private ObservableCollection<Stop> _stops;
        private IList<AlphaKeyGroup<Stop>> _grouped;

        public bool IsLoading
        {
            get { return _isLoading; }
            private set
            {
                _isLoading = value;
                OnPropertyChanged("IsLoading");
            }
        }

        public ObservableCollection<Stop> Stops
        {
            get { return _stops; }
            private set
            {
                _stops = value;
                OnPropertyChanged("Stops");
            }
        }

        public IList<AlphaKeyGroup<Stop>> Grouped
        {
            get { return _grouped; }
            set
            {
                _grouped = value;
                OnPropertyChanged("Grouped");
            }
        }

        public StopsPageViewModel()
        {

        }

        public async Task LoadStops()
        {
            try
            {
                IsLoading = true;
                var response = await RestService.GetApi<List<Stop>>("http://localhost:2412/api/", "stops");
                Stops = new ObservableCollection<Stop>(response);

                Grouped = AlphaKeyGroup<Stop>.CreateGroups(Stops, CultureInfo.CurrentUICulture, s => s.Name, true);
            }
            catch (Exception)
            {

            }
            finally
            {
                IsLoading = false;
            }
        }
    }
}
