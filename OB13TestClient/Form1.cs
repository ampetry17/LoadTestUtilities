using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OB13Service;

namespace OB13TestClient
{
    public partial class Form1 : Form
    {
        private OBSession _session;

        public Form1()
        {
            InitializeComponent();
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            _session = ConnectionManager.GetConnection("gfish", "gfish");
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            ConnectionManager.CloseConnection("gfish","gfish");
        }

        private void btnStoreWithFile_Click(object sender, EventArgs e)
        {
            OnBase13 ob = new OnBase13();
            ob.WriteDocument(@"C:\DocumentsTempCacheFolder\Assembled.tif");
        }

        private void btnStoreBytes_Click(object sender, EventArgs e)
        {
            string filePath = @"C:\DocumentsTempCacheFolder\Assembled.tif";
            byte[] b = System.IO.File.ReadAllBytes(filePath);
            OnBase13 ob = new OnBase13();
            ob.WriteDocument(b);
            
        }
    }
}
