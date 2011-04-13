using Microsoft.Phone.Controls;
using System.Diagnostics;
using System.Windows.Controls;

namespace Ch.Epix.WindMobile.WP7
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();
        }

        private void ListBox_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            NavigationService.Navigate(new System.Uri("/View/StationInfoView.xaml", System.UriKind.Relative));
        }
    }
}
