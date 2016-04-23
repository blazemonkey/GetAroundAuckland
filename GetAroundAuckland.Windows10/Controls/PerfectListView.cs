using Windows.UI.Xaml.Controls;

namespace GetAroundAuckland.Windows10.Controls
{
    public class PerfectListView : ListView
    {
        public PerfectListView()
        {
            this.SizeChanged += PerfectScrollListView_SizeChanged;
        }

        private void PerfectScrollListView_SizeChanged(object sender, Windows.UI.Xaml.SizeChangedEventArgs e)
        {
            if (ItemsPanelRoot != null)
                ItemsPanelRoot.Width = e.NewSize.Width;
        }
    }
}
