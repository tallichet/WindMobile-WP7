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
            ViewModel.RefreshMessages();            
        }

        public void Send(object sender, EventArgs args)
        {
            BindingExpression be = textboxNewMessage.GetBindingExpression(TextBox.TextProperty);
            be.UpdateSource();            
            
            ViewModel.SendMessage();
        }

        private void MessageSent(object sender, EventArgs args)
        {
            this.ListBoxMessage.Focus(); // remove focus from the text box to hide the keyboard
        }
    }
}