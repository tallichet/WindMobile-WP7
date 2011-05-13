using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Ch.Epyx.WindMobile.WP7.Service;
using Ch.Epyx.WindMobile.WP7.Model;
using System.Collections.Generic;
using Ch.Epyx.WindMobile.WP7.Service.TypedServices;
using Ch.Epyx.WindMobile.WP7.Service.Job;

namespace Ch.Epyx.WindMobile.WP7.ViewModel
{
    public class SocialViewModel : ApplicationViewModel
    {
        public string PageTitle { get { return "social"; } }

        public string ChatRoomId { get; private set; }

        public event EventHandler MessageSent;

        private SocialService SocialService { get; set; }

        public SocialViewModel(string chatRoomId)
        {
            ChatRoomId = chatRoomId;
            SocialService = new SocialService();
            SocialService.ErrorOccured += (s, e) =>
            {
                MessageBox.Show("Impossible de récupérer les messages");
                RaisePropertyChanged("LastError");
            };
            SocialService.LastResultChanged += (s, e) => RaisePropertyChanged("LatestMessages");
        }

        public void RefreshMessages()
        {
            SocialService.Refresh(ChatRoomId);
        }

        public List<ISocialMessage> LatestMessages
        {
            get { return SocialService.LastResult; }
        }

        public string LastError
        {
            get 
            {
                if (SocialService != null && SocialService.LastException != null)
                {
                    return SocialService.LastException.Message; 
                }
                return null;                
            }
        }

        public string NewMessage 
        {
            get; set;
        }

        public void SendMessage()
        {
            var sendJob = new SendSocialMessageJob();
            sendJob.JobCompleted += (s, e) =>
                {
                    this.RefreshMessages();
                    this.NewMessage = "";
                    RaisePropertyChanged("NewMessage");
                    RaiseMessageSent();
                };
            sendJob.Execute(new SendMessageData() { ChatRoomId = this.ChatRoomId, Message = NewMessage });
        }

        protected void RaiseMessageSent()
        {
            if (MessageSent != null)
            {
                MessageSent(this, new EventArgs());
            }
        }

        private class SendMessageData : ISendMessage
        {

            public ICredentials Credentials 
            { 
                get { return SettingsViewModel.Credentials; }
            }

            public string ChatRoomId
            {
                get;
                set;
            }

            public string Message
            {
                get;
                set;
            }


            public string BasicAuthentication
            {
                get {
                    var toEncode = SettingsViewModel.UsernameStatic + ":" + SettingsViewModel.PasswordStatic;
                    var strBytes = System.Text.Encoding.UTF8.GetBytes(toEncode);
                    return System.Convert.ToBase64String(strBytes);
                }
            }
        }
    }

    
}
