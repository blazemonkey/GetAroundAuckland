using GetAroundAuckland.Windows10.Interfaces;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetAroundAuckland.Windows10.ViewModels
{
    public class RoutesPageViewModel : BaseViewModel, IRoutesPageViewModel
    {
        private bool _isLoading;

        public bool IsLoading
        {
            get { return _isLoading; }
            private set
            {
                _isLoading = value;
                OnPropertyChanged("IsLoading");
            }
        }

        public RoutesPageViewModel()
        {

        }

        public async Task LoadRoutes()
        {
            await SqlService.GetRoutes();
        }
    }
}
