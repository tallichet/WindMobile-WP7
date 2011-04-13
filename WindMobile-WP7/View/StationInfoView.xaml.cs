using Microsoft.Phone.Controls;
using Ch.Epix.WindMobile.WP7.ViewModel;

namespace Ch.Epix.WindMobile.WP7.View
{
    /// <summary>
    /// Description for StationInfoView.
    /// </summary>
    public partial class StationInfoView : PhoneApplicationPage
    {
        /// <summary>
        /// Initializes a new instance of the StationInfoView class.
        /// </summary>
        public StationInfoView()
        {
            InitializeComponent();
        }

        private void PhoneApplicationPage_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            (this.DataContext as StationInfoViewModel).GetStationDataCommand.Execute(
                ViewModel.ViewModelLocator.MainStatic.CurrentStationInfo.Id);
        }

    }
}