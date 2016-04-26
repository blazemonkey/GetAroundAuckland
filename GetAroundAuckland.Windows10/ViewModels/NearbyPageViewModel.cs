using GetAroundAuckland.Windows10.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;

namespace GetAroundAuckland.Windows10.ViewModels
{
    public class NearbyPageViewModel : BaseViewModel, INearbyPageViewModel
    {
        private string _mapKey;

        private IDisposable _statusChanged;
        private IDisposable _positionChanged;
        private double _lastPositionLatitude;
        private double _lastPositionLongitude;
        private bool _gpsState;
        private Geolocator _geolocator;
        private double _zoomLevel;
        private Geopoint _center;

        public string MapKey
        {
            get { return _mapKey; }
            private set
            {
                _mapKey = value;
                OnPropertyChanged("MapKey");
            }
        }

        public IDisposable StatusChanged
        {
            get { return _statusChanged; }
            private set
            {
                _statusChanged = value;
                OnPropertyChanged("StatusChanged");
            }
        }

        public IDisposable PositionChanged
        {
            get { return _positionChanged; }
            private set
            {
                _positionChanged = value;
                OnPropertyChanged("PositionChanged");
            }
        }

        public bool GpsState
        {
            get { return _gpsState; }
            set
            {
                _gpsState = value;
                OnPropertyChanged("GpsState");
            }
        }

        public double LastPositionLatitude
        {
            get { return _lastPositionLatitude; }
            set
            {
                _lastPositionLatitude = value;
                OnPropertyChanged("LastPositionLatitude");
            }
        }

        public double LastPositionLongitude
        {
            get { return _lastPositionLongitude; }
            set
            {
                _lastPositionLongitude = value;
                OnPropertyChanged("LastPositionLongitude");
            }
        }

        public Geolocator Geolocator
        {
            get { return _geolocator; }
            private set
            {
                _geolocator = value;
                OnPropertyChanged("Geolocator");
            }
        }

        public double ZoomLevel
        {
            get { return _zoomLevel; }
            set
            {
                _zoomLevel = value;
                OnPropertyChanged("ZoomLevel");
            }
        }

        public Geopoint Center
        {
            get { return _center; }
            set
            {
                _center = value;
                OnPropertyChanged("Center");
            }
        }

        public NearbyPageViewModel()
        {
            ZoomLevel = 16;
        }


    }
}
