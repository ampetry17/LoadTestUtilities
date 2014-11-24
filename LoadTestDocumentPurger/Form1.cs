using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Hyland.Unity;
using System.Configuration;
using ConfigurationManager = System.Configuration.ConfigurationManager;

namespace LoadTestDocumentPurger
{
    public partial class Form1 : Form
    {
        private bool _stop;
        private int _purgeCount;
        private Hyland.Unity.Application _app;
        private List<Purger> _purgers; 
       
        
        public Form1()
        {
            InitializeComponent();
        }

        private void btnPurge_Click(object sender, EventArgs e)
        {
            _stop = false;
            OpenConnection();
            StartPurgers();

            //while (!_stop)
            //{
            //    PurgeBatch();
            //}
            _app.Disconnect();
        }

        private void LaunchPurgers()
        {
            _purgers = new List<Purger>();
            for (int i = 1; i <= 10; i++)
            {
                Purger newPurger = new Purger("LoadTest" + i.ToString("0000"),i-1);
                newPurger.DocumentPurged += PurgerPurgedDocument;
                newPurger.PurgeDone += PurgerDone;
                _purgers.Add(newPurger);
            }
        }

        private void PurgerPurgedDocument(object sender, EventArgs e)
        {
            PurgeCount += 1;
        }

        private void PurgerDone(object sender, EventArgs e)
        {
            Purger p = sender as Purger;
            PurgeCount += p.PurgeCount;

            //If all are done, restart the process
            if (_purgers.All(x => x.IsPurgeDone) && !_stop)
            {
                StartPurgers();
            }
        }

        private void StartPurgers()
        {
            DocumentQuery docQuery = _app.Core.CreateDocumentQuery();
            docQuery.AddDateRange(DateTime.Today.AddYears(-1), DateTime.Today.AddDays(-1));
            long batchSize = 100;
            DocumentList docList = docQuery.Execute(batchSize);
            List<long> documentIDs = (from d in docList select d.ID).ToList();

            if (docList.Count == 0)
            {
                MessageBox.Show("All done!");
                return;
            }

            foreach (Purger purger in _purgers)
            {
                List<long> purgerDocIDs = new List<long>(documentIDs);
                purgerDocIDs.RemoveAll(x => (x + purger.LastDigit)%10 != 0);
                purger.PurgeDocuments(purgerDocIDs);
                //break;
            }
            
        }

        private void StopPurgers()
        {
            _purgers.ForEach(x => x.StopPurge());
        }

        private void PurgeBatch()
        {
            List<string> lastDigits = ConfigurationManager.AppSettings["LastDigits"].Split(",".ToCharArray()).ToList();

            DocumentQuery docQuery = _app.Core.CreateDocumentQuery();
            docQuery.AddDateRange(DateTime.Today.AddYears(-1), DateTime.Today.AddDays(-1));
            long batchSize = 100;
            DocumentList docList = docQuery.Execute(batchSize);

            if (docList.Count == 0)
            {
                MessageBox.Show("All done!");
                return;
            }

            foreach (Document doc in docList)
            {
                string sDocID = doc.ID.ToString();
                if (lastDigits.Contains(sDocID.Substring(sDocID.Length - 1, 1)))
                {
                    _app.Core.Storage.PurgeDocument(doc);
                    PurgeCount += 1;
                }
                if (_stop)
                {
                    return;
                }
            }

        }


        private void OpenConnection()
        {
            AuthenticationProperties connectProperties =
                Hyland.Unity.Application.CreateOnBaseAuthenticationProperties(
                    System.Configuration.ConfigurationSettings.AppSettings["DmsServiceURL"],
                    ConfigurationManager.AppSettings["DmsUserName"],
                    ConfigurationManager.AppSettings["DmsPassword"],
                    System.Configuration.ConfigurationSettings.AppSettings["DmsServiceDataSource"]);
            _app = Hyland.Unity.Application.Connect(connectProperties);
        }


        private void btnStop_Click(object sender, EventArgs e)
        {
            _stop = true;
            StopPurgers();
        }


        private int PurgeCount
        {
            get { return _purgeCount; }
            set
            {
                _purgeCount = value;
                lblPurgeCount.Text = "Documents Purged: " + _purgeCount.ToString();
                System.Windows.Forms.Application.DoEvents();
            }
        }

        private void btnLaunchPurgers_Click(object sender, EventArgs e)
        {
            LaunchPurgers();
        }
    }
}
