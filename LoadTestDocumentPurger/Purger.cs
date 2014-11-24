using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hyland.Unity;
using ConfigurationManager = System.Configuration.ConfigurationManager;

namespace LoadTestDocumentPurger
{
    public class Purger:IDisposable
    {
        private Hyland.Unity.Application _app;
        private string _userName;
        public long LastDigit { get; set; }
        public event EventHandler PurgeDone;
        public event EventHandler DocumentPurged;
        public bool IsPurgeDone { get; set; }
        public bool _stop;
        private List<long> _docIDs; 
        private BackgroundWorker _bw;
        public int PurgeCount { get; set; }

        public Purger(string userName, int lastDigit)
        {
            _userName = userName;
            LastDigit = lastDigit;
            OpenConnection();
            _stop = false;
        }

        public void PurgeDocuments(List<long> docIDs)
        {
            _stop = false;
            IsPurgeDone = false;
            _docIDs = docIDs;

            _bw = new BackgroundWorker();
            _bw.DoWork += _bw_DoWork;
            _bw.RunWorkerCompleted += _bw_RunWorkerCompleted;
            _bw.RunWorkerAsync();
            
        }

        void _bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            IsPurgeDone = true;
            PurgeDone(this, null);
        }

        void _bw_DoWork(object sender, DoWorkEventArgs e)
        {
            PurgeCount = 0;

            foreach (long docID in _docIDs)
            {
                Document doc = _app.Core.GetDocumentByID(docID);

                if (doc != null && !_stop)
                {
                    _app.Core.Storage.PurgeDocument(doc);
                    PurgeCount += 1;
                }
            }
        }



        public void StopPurge()
        {
            _stop = true;
        }


        private void OpenConnection()
        {
            AuthenticationProperties connectProperties =
                Hyland.Unity.Application.CreateOnBaseAuthenticationProperties(
                    ConfigurationManager.AppSettings["DmsServiceURL"],
                    _userName,
                    "password",
                    ConfigurationManager.AppSettings["DmsServiceDataSource"]);
            _app = Hyland.Unity.Application.Connect(connectProperties);
        }

        public void Dispose()
        {
            _app.Disconnect();
            _app.Dispose();
        }
    }
}
