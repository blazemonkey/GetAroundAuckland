﻿using GetAroundAuckland.Windows10.Helpers;
using GetAroundAuckland.Windows10.Interfaces;
using GetAroundAuckland.Windows10.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace GetAroundAuckland.Windows10.ViewModels
{
    public class RoutesPageViewModel : BaseViewModel, IRoutesPageViewModel
    {
        private bool _isLoading;
        private ObservableCollection<Route> _routes;
        private IList<AlphaKeyGroup<Route>> _grouped;

        public bool IsLoading
        {
            get { return _isLoading; }
            private set
            {
                _isLoading = value;
                OnPropertyChanged("IsLoading");
            }
        }

        public ObservableCollection<Route> Routes
        {
            get { return _routes; }
            private set
            {
                _routes = value;
                OnPropertyChanged("Routes");
            }
        }

        public IList<AlphaKeyGroup<Route>> Grouped
        {
            get { return _grouped; }
            set
            {
                _grouped = value;
                OnPropertyChanged("Grouped");
            }
        }

        public RoutesPageViewModel()
        {

        }

        public async Task LoadRoutes()
        {
            try
            {
                IsLoading = true;
                var response = await RestService.GetApi<List<Route>>("http://localhost:2412/api/", "routes");
                Routes = new ObservableCollection<Route>(response.OrderBy(x => x.AgencyId).ThenBy(x => x.ShortName).ThenBy(x => x.LongName));

                Grouped = AlphaKeyGroup<Route>.CreateGroups(Routes, CultureInfo.CurrentUICulture, s => s.LongName, true);
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
