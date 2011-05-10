using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Ch.Epyx.WindMobile.WP7.Model;

namespace Ch.Epyx.WindMobile.WP7.View
{
    public partial class StationInfoControl : UserControl
    {
        public StationInfoControl()
        {
            InitializeComponent();
        }

        private void chatButtonClicked(object sender, RoutedEventArgs arg)
        {
            RaiseChatButtonClick();
        }

        protected void RaiseChatButtonClick()
        {
            if (ChatButtonClick != null)
            {
                ChatButtonClick(this, new ChatButtonClickedEventArgs(DataContext as IStationInfo));
            }
        }

        public event EventHandler<ChatButtonClickedEventArgs> ChatButtonClick;
    }

    public class ChatButtonClickedEventArgs : EventArgs 
    {
        public IStationInfo StationInfo { get; private set; }

        public ChatButtonClickedEventArgs(IStationInfo stationInfo) 
        {
            StationInfo = stationInfo;
        }
    }
}
