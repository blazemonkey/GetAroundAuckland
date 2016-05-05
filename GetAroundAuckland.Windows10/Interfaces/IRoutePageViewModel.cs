using GetAroundAuckland.Windows10.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetAroundAuckland.Windows10.Interfaces
{
    public interface IRoutePageViewModel
    {
        ObservableCollection<Shape> GetShapes();
        ObservableCollection<Stop> GetStops();
    }
}
