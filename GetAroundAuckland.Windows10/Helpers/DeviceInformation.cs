using Windows.Graphics.Display;

namespace GetAroundAuckland.Windows10.Helpers
{
    public class DeviceInformation
    {
        public enum Orientations { Portrait, Landscape }
        public static Orientations Orientation => DisplayInformation.GetForCurrentView().CurrentOrientation.ToString().Contains("Landscape") ? Orientations.Landscape : Orientations.Portrait;
        public static DisplayInformation DisplayInformation => DisplayInformation.GetForCurrentView();
    }
}
