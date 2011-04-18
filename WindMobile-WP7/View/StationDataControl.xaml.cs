using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Ch.Epix.WindMobile.WP7.Model;
using Ch.Epix.WindMobile.WP7.ViewModel;

namespace Ch.Epix.WindMobile.WP7
{
	public partial class StationDataControl : UserControl
	{
        private StationInfoViewModel ViewModel
        {
            get
            {
                return this.DataContext as StationInfoViewModel;
            }
        }

		public StationDataControl()
		{
			// Required to initialize variables
			InitializeComponent();
		}

        void ViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "StationData")
            {
                UpdateTrend();
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            ViewModel.PropertyChanged += ViewModel_PropertyChanged;
            UpdateTrend();
        }

        private void UpdateTrend()
        {
            if (ViewModel != null && ViewModel.StationData != null)
            {
                this.TrendRotateTransform.Angle = -ViewModel.StationData.WindTrend;
            }
        }

        private void ShowErrorMessage()
        {
            if (ViewModel != null && ViewModel.ErrorMessage != null)
            {
                MessageBox.Show(ViewModel.ErrorMessage, "Erreur", MessageBoxButton.OK);
            }
        }
	}
}