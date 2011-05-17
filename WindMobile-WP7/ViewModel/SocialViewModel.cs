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
        public string PageTitle { get; private set; }

        public string ChatRoomId { get; private set; }

        public event EventHandler<MessageSentEventArgs> MessageSent;
        public event EventHandler MessageRefreshed;

        public Visibility ShowProgress { get; private set; }

        private SocialService SocialService { get; set; }

        public SocialViewModel(IStationInfo stationInfo)
        {
            ChatRoomId = stationInfo.Id;
            PageTitle = stationInfo.Name;
            this.MessageSent += MessageSentHandler;
            SocialService = new SocialService();
            SocialService.ErrorOccured += (s, e) =>
            {
                MessageBox.Show("Impossible de récupérer les messages");
                RaisePropertyChanged("LastError");

                ShowProgress = Visibility.Collapsed;
                RaisePropertyChanged("ShowProgress");
            };
            SocialService.LastResultChanged += (s, e) =>
                {
                    RaisePropertyChanged("LatestMessages");
                    ShowProgress = Visibility.Collapsed;
                    RaisePropertyChanged("ShowProgress");
                    RaiseMessageRefreshed();
                };
        }

        public void RefreshMessages()
        {
            SocialService.Refresh(ChatRoomId);
            ShowProgress = Visibility.Visible;
            RaisePropertyChanged("ShowProgress");
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
                    RaiseMessageSent(NewMessage);
                    this.NewMessage = String.Empty;
                    RaisePropertyChanged("NewMessage");
                    
                };
            sendJob.Execute(new SendMessageData() { ChatRoomId = this.ChatRoomId, Message = NewMessage });
        }

        protected void RaiseMessageSent(string message)
        {
            if (MessageSent != null)
            {
                MessageSent(this, new MessageSentEventArgs(message));
            }
        }

        protected void RaiseMessageRefreshed()
        {
            if (MessageRefreshed != null)
            {
                MessageRefreshed(this, new EventArgs());
            }
        }

        private void MessageSentHandler(object sender, MessageSentEventArgs args)
        {
            SocialService.LastResult.Add(args.Message);
            RaisePropertyChanged("LatestMessages");
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



    public class MessageSentEventArgs : EventArgs
    {
        public ISocialMessage Message { get; private set; }

        public MessageSentEventArgs(string message)
        {
            this.Message = new SocialMessage()
            {
                Date = DateTime.Now,
                Pseudo = SettingsViewModel.PseudoStatic,
                Text = message
            };
        }


        class SocialMessage : ISocialMessage
        {
            public DateTime Date
            {
                get;
                set;
            }

            public string Pseudo
            {
                get;
                set;
            }

            public string Text
            {
                get;
                set;
            }
        }
    }

    
}
