using GetAroundAuckland.Windows10.Interfaces;
using GetAroundAuckland.Windows10.Models;
using Services.MessengerService;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Microsoft.Practices.Unity;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Maps;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.Devices.Geolocation;
using Windows.UI;
using Windows.UI.Text;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace GetAroundAuckland.Windows10.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class RoutePage : Page
    {
        private MapControl _mapControl;
        public IMessengerService MessengerService { get; set; }
        private IRoutePageViewModel _vm;

        public RoutePage()
        {
            this.InitializeComponent();
            _vm = (IRoutePageViewModel)DataContext;
            MessengerService = App.Container.Resolve<MessengerService>();
        }

        private void RoutesMapControl_Loaded(object sender, RoutedEventArgs e)
        {
            var map = (MapControl)sender;
            //SetMapControl(map);
            _mapControl = map;
            MessengerService.Register<IEnumerable<Shape>>(this, "DrawShapes", DrawShapes);
            MessengerService.Register<bool>(this, "ClearShapes", ClearShapes);

            DrawShapes(_vm.GetShapes());
            DrawStops(_vm.GetStops());
        }

        private void ClearShapes(bool clear)
        {
            _mapControl.MapElements.Clear();
        }

        private void DrawShapes(IEnumerable<Shape> shapes)
        {
            var polyline = new MapPolyline();
            var posList = new List<BasicGeoposition>();
            foreach (var shape in shapes)
            {
                posList.Add(new BasicGeoposition()
                {
                    Latitude = Convert.ToDouble(shape.Latitude),
                    Longitude = Convert.ToDouble(shape.Longitude)
                });
            }

            polyline.StrokeColor = Color.FromArgb(0xFF, 0x00, 0x97, 0xFF);
            polyline.StrokeThickness = 4;
            polyline.Path = new Geopath(posList);
            _mapControl.MapElements.Add(polyline);
        }

        private void DrawStops(IEnumerable<Stop> stops)
        {
            var geopoints = new List<Geopoint>();
            _mapControl.Children.Clear();

            var stopsToDraw = stops.ToList();
            for (var i = 0; i < stopsToDraw.Count(); i++)
            {
                var stop = stopsToDraw[i];
                var centerPoint = DrawPointOnMap(stop, i + 1);
                geopoints.Add(centerPoint);
            }

            SetCenterOfPoints(geopoints);
        }

        private void CenterMapStop(Stop stop)
        {
            var geopoints = new List<Geopoint>();

            var basicGeoposition = new BasicGeoposition();
            basicGeoposition.Latitude = Convert.ToDouble(stop.Latitude);
            basicGeoposition.Longitude = Convert.ToDouble(stop.Longitude);
            var geopoint = new Geopoint(basicGeoposition);

            geopoints.Add(geopoint);
            SetCenterOfPoints(geopoints);
        }

        private Geopoint DrawPointOnMap(Stop stop, int index)
        {
            var center = new BasicGeoposition();
            center.Latitude = Convert.ToDouble(stop.Latitude);
            center.Longitude = Convert.ToDouble(stop.Longitude);
            var centerPoint = new Geopoint(center);

            var text = new TextBlock
            {
                FontWeight = FontWeights.Light,
                FontSize = 10,
                Text = index + " / " + stop.Code.ToString(),
                Foreground = new SolidColorBrush(Colors.White),
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center
            };

            var grid = new Grid
            {
                Height = 15,
                Width = 45,
                Margin = new Thickness(10),
                Background = new SolidColorBrush(Colors.Black)
            };

            grid.Children.Add(text);
            MapControl.SetLocation(grid, centerPoint);
            MapControl.SetNormalizedAnchorPoint(grid, new Point(0.5, 0.5));
            _mapControl.Children.Add(grid);

            return centerPoint;
        }

        private void SetCenterOfPoints(IEnumerable<Geopoint> positions)
        {
            var mapWidth = _mapControl.ActualWidth;
            var mapHeight = _mapControl.ActualHeight;
            if (mapWidth == 0 || mapHeight == 0)
                return;

            if (positions.Count() == 0)
                return;

            if (positions.Count() == 1)
            {
                var singleGeoposition = new BasicGeoposition();
                singleGeoposition.Latitude = positions.First().Position.Latitude;
                singleGeoposition.Longitude = positions.First().Position.Longitude;
                _mapControl.Center = new Geopoint(singleGeoposition);
                _mapControl.ZoomLevel = 16;
                return;
            }

            var maxLatitude = positions.Max(x => x.Position.Latitude);
            var minLatitude = positions.Min(x => x.Position.Latitude);

            var maxLongitude = positions.Max(x => x.Position.Longitude);
            var minLongitude = positions.Min(x => x.Position.Longitude);

            var centerLatitude = ((maxLatitude - minLatitude) / 2) + minLatitude;
            var centerLongitude = ((maxLongitude - minLongitude) / 2) + minLongitude;

            var nw = new BasicGeoposition()
            {
                Latitude = maxLatitude,
                Longitude = minLongitude
            };

            var se = new BasicGeoposition()
            {
                Latitude = minLatitude,
                Longitude = maxLongitude
            };

            var buffer = 1;
            //best zoom level based on map width
            var zoomWidth = Math.Log(360.0 / 256.0 * (mapWidth - 2 * buffer) / (maxLongitude - minLongitude)) / Math.Log(2);
            //best zoom level based on map height
            var zoomHeight = Math.Log(180.0 / 256.0 * (mapHeight - 2 * buffer) / (maxLatitude - minLatitude)) / Math.Log(2);
            var zoom = (zoomWidth + zoomHeight) / 2;
            _mapControl.ZoomLevel = zoom - 0.8;

            //var box = new GeoboundingBox(nw, se);
            var geoposition = new BasicGeoposition();
            geoposition.Latitude = ((maxLatitude - minLatitude) / 2) + minLatitude;
            geoposition.Longitude = ((maxLongitude - minLongitude) / 2) + minLongitude;
            _mapControl.Center = new Geopoint(geoposition);
        }
    }
}
