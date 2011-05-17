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
using Microsoft.Phone.Controls;
using Ch.Epyx.WindMobile.WP7.ViewModel;
using System.Windows.Data;
using System.Threading;
using System.Diagnostics;

namespace Ch.Epyx.WindMobile.WP7.View
{
    public partial class SocialView : PhoneApplicationPage
    {
        public SocialViewModel ViewModel
        {
            get { return this.DataContext as SocialViewModel; }
        }

        public SocialView()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            string roomid = NavigationContext.QueryString["chatroomid"];
            this.DataContext = new SocialViewModel(roomid);
            ViewModel.MessageSent += MessageSent;
            ViewModel.MessageRefreshed += MessageRefreshed;
            ViewModel.RefreshMessages();            
        }

        public void Send(object sender, EventArgs args)
        {
            BindingExpression be = textboxNewMessage.GetBindingExpression(TextBox.TextProperty);
            be.UpdateSource();

            this.Focus();
            
            ViewModel.SendMessage();
        }

        public void Refresh(object sender, EventArgs args)
        {
            ViewModel.RefreshMessages();
        }

        private void MessageSent(object sender, MessageSentEventArgs args)
        {
            
        }

        private void MessageRefreshed(object sender, EventArgs args)
        {
            ListBoxMessage.UpdateLayout();
            ListBoxMessage.ScrollIntoView(ListBoxMessage.Items.Last());
        }
    }
}