using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Ch.Epyx.WindMobile.WP7.Model;
using Ch.Epyx.WindMobile.WP7.ViewModel;
using System.Windows.Media.Imaging;

namespace Ch.Epyx.WindMobile.WP7.View
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
            (DataContext as StationInfoViewModel).RefreshCommand.Execute(null);
            ViewModel.Activated += (s, arg) => Animate();
            UpdateTrend();
        }

        private void UpdateTrend()
        {
            if (ViewModel != null && ViewModel.StationData != null)
            {
                //this.TrendRotateTransform.Angle = -ViewModel.StationData.WindTrend;
                //this.RotateTrendAnimationAngle.To = -ViewModel.StationData.WindTrend;
                //this.RotateTrendAnimation.Begin();
                Animate();
                ImageTrend.Source = new BitmapImage(
                    new Uri("../Images/arrow_" + 
                        (ViewModel.StationData.WindTrend > 0 ? "red" : "green") + ".png", UriKind.Relative)
                    );
                //"/WindMobile-WP7;component
            }
        }

        private void ShowErrorMessage()
        {
            if (ViewModel != null && ViewModel.ErrorMessage != null)
            {
                MessageBox.Show(ViewModel.ErrorMessage, "Erreur", MessageBoxButton.OK);
            }
        }

        public void Animate()
        {
            if (ViewModel.StationData != null)
            {
                this.RotateTrendAnimationAngle.From = 0;
                this.RotateTrendAnimationAngle.To = -ViewModel.StationData.WindTrend;
                this.RotateTrendAnimation.Begin();
            }
        }

        
	}
}