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
using Ch.Epyx.WindMobile.WP7.Model;
using System.Diagnostics;
using System.IO;

namespace Ch.Epyx.WindMobile.WP7.Service.Job
{
    public class SendSocialMessageJob : IJob<ISendMessage, object>
    {
        public bool IsBusy
        {
            get { throw new NotImplementedException(); }
        }

        public void Execute(ISendMessage param)
        {
            var client = new WebClient();
            
            
            client.UploadStringCompleted += (s, e) =>
            {
                if (e.Error != null)
                    RaiseJobError(e.Error);
                else
                    RaiseJobCompleted();
            };

            client.Headers["Content-Type"] = "text/plain";
            client.Headers["Authorization"] = "Basic " + param.BasicAuthentication;
            
            client.UploadStringAsync(new Uri(Constants.BaseUrl + String.Format("chatrooms/{0}/postmessage", param.ChatRoomId), UriKind.Absolute), "POST", param.Message);
        }

        protected void RaiseJobCompleted()
        {
            if (JobCompleted != null)
            {
                JobCompleted(this, new JobFinishedEventArgs<object>(null));                
            }
        }

        protected void RaiseJobError(Exception e)
        {
            if (JobError != null)
            {
                JobError(this, new JobErrorEventArgs("Envoie de message", e));
            }
            var webex = e as WebException;
            if (webex != null)
            {
                if (Debugger.IsAttached)
                {
                    Debug.WriteLine("====  WebException  ====");
                    Debug.WriteLine("Status = " + webex.Status);
                    if (webex.Data != null)
                    {   
                        foreach (var k in webex.Data.Keys)
                        {
                            Debug.WriteLine(k + ":\t" + webex.Data[k]);
                        }
                    }
                    Debug.WriteLine(new StreamReader(webex.Response.GetResponseStream()).ReadToEnd());
                }
            }
            MessageBox.Show(@"Impossible d'envoyer un message
Vérifier votre connextion au réseau");
        }

        public event EventHandler<JobFinishedEventArgs<object>> JobCompleted;

        public event EventHandler<JobErrorEventArgs> JobError;

    }
}
