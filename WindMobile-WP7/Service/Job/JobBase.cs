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
using System.ComponentModel;

namespace Ch.Epix.WindMobile.WP7.Service.Job
{
    public abstract class JobBase : IJob
    {
        private BackgroundWorker worker;
        private WebClient client;

        private object downloadedObject;
        private object result;

        protected string baseUrl = "http://windmobile.vol-libre-suchet.ch:1588/windmobile/";

        public JobBase()
        {
            InitWebClient();
            InitWorker();
        }

        public virtual bool IsBusy
        {
            get;
            private set;
        }

        public abstract void Execute();

        /// <summary>
        /// Executed before doing anything else. 
        /// Thread: UI
        /// </summary>
        protected virtual void Init() 
        {
            IsBusy = true;
        }

        /// <summary>
        /// Just return the URL for the download string
        /// </summary>
        /// <returns>Url to access the data</returns>
        protected abstract Uri GetUrl();

        /// <summary>
        /// Occurs when the string was downloaded correctly
        /// </summary>
        /// <param name="downloadedString">The resulting string</param>
        /// <returns></returns>
        protected virtual Object OnDownloadStringCompleted(String downloadedString) { return null; }
        /// <summary>
        /// Allow to process download error message
        /// </summary>
        /// <param name="exception">Error exception</param>
        protected virtual void OnDownloadStringError(Exception exception) { }

        /// <summary>
        /// This happen in the UI Thread, just before starting the background worker
        /// </summary>
        /// <param name="arg">Object to use as argument for the background work</param>
        protected virtual void OnJobStarting(object arg) { }

        /// <summary>
        /// Background work. Done in the Background Thread (no access to UI thread Dependency properties)
        /// </summary>
        /// <param name="cancel">Was the job canceled?</param>
        /// <param name="arg">argument for the background job</param>
        /// <returns>Object return by the </returns>
        protected virtual Object JobRun(ref bool cancel, object arg) { return null; }

        /// <summary>
        /// Occurs after the job ends, in the UI Thread, just before the JobCompleted event raise
        /// </summary>
        /// <param name="result">Result of the background job</param>
        protected virtual void OnJobCompleted(object result) { }

        /// <summary>
        /// Occurs when the job was completed
        /// </summary>
        public event EventHandler<JobFinishedEventArgs> JobCompleted;

        /// <summary>
        /// Start a job when needing to download
        /// </summary>
        public void StartDownloadJob()
        {
            Init();
            if (client.IsBusy == false)
            {
                client.DownloadStringAsync(GetUrl());
            }
        }

        /// <summary>
        /// Start a background job without download
        /// </summary>
        /// <param name="arg">Argument for the background job</param>
        public void StartBackgroundJob(object arg)
        {
            Init();
            worker.RunWorkerAsync(arg);
        }

        private void InitWebClient() 
        {
            Init();
            client = new WebClient();
            client.DownloadStringCompleted += (s, e) =>
            {
                if (e.Error != null)
                {
                    OnDownloadStringError(e.Error);
                }
                else if (e.Cancelled == false)
                {
                    downloadedObject = OnDownloadStringCompleted(e.Result);
                }
                worker.RunWorkerAsync(downloadedObject);
            };
        }

        private void InitWorker()
        {
            worker = new BackgroundWorker();
            worker.WorkerReportsProgress = true;
            worker.WorkerSupportsCancellation = true;
            worker.DoWork += (s, e) =>
                {
                    bool cancel = false;
                    e.Result = JobRun(ref cancel, e.Argument);
                    e.Cancel = cancel;
                };
            worker.RunWorkerCompleted += (s, e) =>
                {
                    if (e.Cancelled == false && e.Error == null)
                    {
                        result = e.Result;
                        RaiseJobCompleted(result);
                    }
                    IsBusy = false;
                };
        }

        private void RaiseJobCompleted(object result)
        {
            if (JobCompleted != null)
            {
                JobCompleted(this, new JobFinishedEventArgs(result));
            }
        }
    }

    
}
