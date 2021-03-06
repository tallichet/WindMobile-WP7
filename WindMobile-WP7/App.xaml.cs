﻿using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Navigation;
using Ch.Epyx.WindMobile.WP7.Service;
using Ch.Epyx.WindMobile.WP7.ViewModel;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace Ch.Epyx.WindMobile.WP7
{
    public partial class App : Application
    {

        // Easy access to the root frame
        public PhoneApplicationFrame RootFrame { get; private set; }

        

        // Constructor
        public App()
        {
            // Global handler for uncaught exceptions. 
            // Note that exceptions thrown by ApplicationBarItem.Click will not get caught here.
            UnhandledException += Application_UnhandledException;

            // Standard Silverlight initialization
            InitializeComponent();

            // Phone-specific initialization
            InitializePhoneApplication();
        }

        // Code to execute when the application is launching (eg, from Start)
        // This code will not execute when the application is reactivated
        private void Application_Launching(object sender, LaunchingEventArgs e)
        {
        }

        // Code to execute when the application is activated (brought to foreground)
        // This code will not execute when the application is first launched
        private void Application_Activated(object sender, ActivatedEventArgs e)
        {
            ApplicationRunData.LoadState(PhoneApplicationService.Current.State);
            ServiceCentral.LoadState(PhoneApplicationService.Current.State);
        }

        // Code to execute when the application is deactivated (sent to background)
        // This code will not execute when the application is closing
        private void Application_Deactivated(object sender, DeactivatedEventArgs e)
        {
            ApplicationRunData.SaveState(PhoneApplicationService.Current.State);
            ServiceCentral.SaveState(PhoneApplicationService.Current.State);
        }

        // Code to execute when the application is closing (eg, user hit Back)
        // This code will not execute when the application is deactivated
        private void Application_Closing(object sender, ClosingEventArgs e)
        {
            
        }
        

        // Code to execute if a navigation fails
        void RootFrame_NavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            if (System.Diagnostics.Debugger.IsAttached)
            {
                // A navigation has failed; break into the debugger
                System.Diagnostics.Debugger.Break();
            }
        }

        // Code to execute on Unhandled Exceptions
        private void Application_UnhandledException(object sender, ApplicationUnhandledExceptionEventArgs e)
        {
            if (System.Diagnostics.Debugger.IsAttached)
            {
                Debug.WriteLine(e.ExceptionObject.GetType().Name);
                Debug.WriteLine(e.ExceptionObject.Message);
                Debug.WriteLine(e.ExceptionObject.StackTrace);

                // An unhandled exception has occurred; break into the debugger
                System.Diagnostics.Debugger.Break();
            }
            e.Handled = true;
            //ViewModel.ViewModelLocator.ExceptionStatic = new ExceptionViewModel(e.ExceptionObject);
            //(RootVisual as PhoneApplicationFrame).Source = new Uri("/View/ExceptionView.xaml", UriKind.Relative);
        }

        #region Phone application initialization

        // Avoid double-initialization
        private bool phoneApplicationInitialized = false;

        // Do not add any additional code to this method
        private void InitializePhoneApplication()
        {
            if (phoneApplicationInitialized)
                return;

            // Create the frame but don't set it as RootVisual yet; this allows the splash
            // screen to remain active until the application is ready to render.
            RootFrame = //new TransitionFrame();
                new PhoneApplicationFrame();
            RootFrame.Navigated += CompleteInitializePhoneApplication;

            // Handle navigation failures
            RootFrame.NavigationFailed += RootFrame_NavigationFailed;

            // Ensure we don't initialize again
            phoneApplicationInitialized = true;
        }

        // Do not add any additional code to this method
        private void CompleteInitializePhoneApplication(object sender, NavigationEventArgs e)
        {
            // Set the root visual to allow the application to render
            if (RootVisual != RootFrame)
                RootVisual = RootFrame;

            // Remove this handler since it is no longer needed
            RootFrame.Navigated -= CompleteInitializePhoneApplication;
        }

        #endregion
    }
}
