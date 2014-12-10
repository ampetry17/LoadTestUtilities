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
using System.IO;

namespace LoadTestDocumentPurger
{
    public partial class Form1 : Form
    {
        private bool _stop;
        private int _purgeCount;
        private Hyland.Unity.Application _app;
        private List<Purger> _purgers;
        private int _NumberOfPurgers;
        private DateTime _purgeStartTime = DateTime.Now;
        private long _batchSize = 1000;
       
        
        public Form1()
        {
            InitializeComponent();

            _NumberOfPurgers = int.Parse(ConfigurationManager.AppSettings["NumberOfPurgers"]);
            ShowDriveInfo();
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
            //_app.Disconnect();
        }

        private void LaunchPurgers()
        {
            _purgers = new List<Purger>();
            for (int i = 1; i <= _NumberOfPurgers; i++)
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
            
            //PurgeCount += p.PurgeCount;

            //If all are done, restart the process
            if (_purgers.All(x => x.IsPurgeDone) && !_stop)
            {
                ShowDriveInfo();
                ShowPurgeRate();
                StartPurgers();
            }
        }

        private void ShowPurgeRate()
        {
            double elapsedSeconds = DateTime.Now.Subtract(_purgeStartTime).TotalSeconds;

            double purgeRate = _batchSize/elapsedSeconds;
            lblPurgeRate.Text = "Purge rate: " + purgeRate.ToString("#.0") + " docs/second";

        }
        private void StartPurgers()
        {
            //lblPurgeCount.Text = "Starting Purgers";
            System.Windows.Forms.Application.DoEvents();

            if (_purgers == null)
            {
                LaunchPurgers();
            }

            _purgeStartTime = DateTime.Now;

            DocumentQuery docQuery = _app.Core.CreateDocumentQuery();
            docQuery.AddDateRange(DateTime.Today.AddYears(-1), DateTime.Now.AddHours(-1));
            DocumentList docList = docQuery.Execute(_batchSize);
            List<long> documentIDs = (from d in docList select d.ID).ToList();

            if (docList.Count == 0)
            {
                MessageBox.Show("All done!");
                return;
            }

            int docsPerPurger = docList.Count/_NumberOfPurgers;

            int purgerID = 0;
            foreach (Purger purger in _purgers)
            {
                List<long> purgerDocIDs = new List<long>();
                foreach (long docID in documentIDs)
                {
                    purgerDocIDs.Add(docID);
                    if (purgerDocIDs.Count >= docsPerPurger)
                    {
                        break;
                    }
                }

                foreach (long docID in purgerDocIDs)
                {
                    documentIDs.Remove(docID);
                }
                
                //purgerDocIDs.RemoveRange();
                //purgerDocIDs.RemoveAll(x => (x - purger.LastDigit) % _NumberOfPurgers != 0);
                purger.PurgeDocuments(purgerDocIDs);
                //break;
            }
            
        }

        private void StopPurgers()
        {
            _purgers.ForEach(x => x.StopPurge());
        }

        private void OpenConnection()
        {
            if (_app == null)
            {
                AuthenticationProperties connectProperties =
                    Hyland.Unity.Application.CreateOnBaseAuthenticationProperties(
                        System.Configuration.ConfigurationSettings.AppSettings["DmsServiceURL"],
                        ConfigurationManager.AppSettings["DmsUserName"],
                        ConfigurationManager.AppSettings["DmsPassword"],
                        System.Configuration.ConfigurationSettings.AppSettings["DmsServiceDataSource"]);
                _app = Hyland.Unity.Application.Connect(connectProperties);
            }
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

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_purgers != null)
            {
                foreach (Purger p in _purgers)
                {
                    p.CloseConnection();
                }
            }

            if (_app != null)
            {
                _app.Disconnect();
            }
        }

        private void ShowDriveInfo()
        {
            DriveInfo[] allDrives = DriveInfo.GetDrives();

            foreach (DriveInfo drInfo in allDrives)
            {
                if (drInfo.DriveType == DriveType.Fixed && drInfo.IsReady)
                {
                    DriveSpaceIndicator dsi;
                    Control[] oDriveInfo = pnlDrives.Controls.Find("Drive" + drInfo.Name, false);
                    if (oDriveInfo.Length > 0)
                    {
                        dsi = (DriveSpaceIndicator) oDriveInfo[0];
                        dsi.SetDriveInfo(drInfo);
                    }
                    else
                    {
                        dsi = new DriveSpaceIndicator(drInfo);
                        dsi.Name = "Drive" + drInfo.Name;
                        pnlDrives.Controls.Add(dsi);
                    }
                }
            }
        }

        private void btnUserSetup_Click(object sender, EventArgs e)
        {
            if (_app == null)
            {
                OpenConnection();
            }

            frmUser fUser = new frmUser(_app);
            fUser.ShowDialog();
        }
    }
}
