using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace LoadTestDocumentPurger
{
    public partial class DriveSpaceIndicator : UserControl
    {


        public DriveSpaceIndicator(DriveInfo drInfo)
        {
            InitializeComponent();
            SetDriveInfo(drInfo);
        }

        public void SetDriveInfo(DriveInfo drInfo)
        {
            double freeSpace = (double) drInfo.TotalFreeSpace/1024/1024/1024;
            double totalSize = (double) drInfo.TotalSize/1024/1024/1024;

            lblDriveName.Text = "Drive " + drInfo.Name;
            lblFreeSpace.Text = freeSpace.ToString("#.#") + " GB free of " + totalSize.ToString("#.#") + " GB";
            pbSpace.Maximum = (int)totalSize;
            pbSpace.Value = (int)(totalSize - freeSpace);
        }
    }
}
