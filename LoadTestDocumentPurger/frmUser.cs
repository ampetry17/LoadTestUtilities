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

namespace LoadTestDocumentPurger
{
    public partial class frmUser : Form
    {
        private Hyland.Unity.Application _app;

        public frmUser(Hyland.Unity.Application app)
        {
            InitializeComponent();
            _app = app;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            for (int i = 1; i <= 30; i++)
            {
                string userName = "LoadTest" + i.ToString("0000");
                User existingUser = _app.Core.GetUser(userName);

                if (existingUser == null)
                {
                    UserAdministration userAdmin = _app.Core.UserAdministration;
                    NewUserProperties userProperties = userAdmin.CreateNewUserProperties(
                        "LoadTest" + i.ToString("0000"),
                        "password");
                    List<UserGroup> userGroups = new List<UserGroup>();
                    userGroups.Add(_app.Core.UserGroups.Find("MANAGER"));

                    userProperties.UserGroups = userGroups;
                    User user = userAdmin.CreateUser(userProperties);
                }
            }

        }
    }
}
