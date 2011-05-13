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
using System.IO.IsolatedStorage;

namespace Ch.Epyx.WindMobile.WP7.ViewModel
{
    public class SettingsViewModel : ApplicationViewModel
    {
        public static string UsernameStatic
        {
            get
            {
                string username;
                if (IsolatedStorageSettings.ApplicationSettings.TryGetValue<string>("username", out username))
                {
                    return username;
                }
                else
                {
                    return "cedric@epyx.ch";
                }
            }
            set
            {
                IsolatedStorageSettings.ApplicationSettings["username"] = value;
            }
        }

        public static string PasswordStatic
        {
            get
            {
                string password;
                if (IsolatedStorageSettings.ApplicationSettings.TryGetValue<string>("password", out password))
                {
                    return password;
                }
                else
                {
                    return "456";
                }
            }
            set
            {
                IsolatedStorageSettings.ApplicationSettings["password"] = value;
            }
        }

        public static ICredentials Credentials
        {
            get { return new NetworkCredential(UsernameStatic, PasswordStatic); }
        }

        public string Username
        {
            get { return UsernameStatic; }
            set { UsernameStatic = value; }
        }

        public string Password
        {
            get { return PasswordStatic; }
            set { PasswordStatic = value; }
        }

        public string PageTitle
        {
            get { return "paramètres"; }
        }
        
    }
}
