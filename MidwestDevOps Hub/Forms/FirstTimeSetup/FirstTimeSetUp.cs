using BusinessLogicLayer;
using HubModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MidwestDevOps_Hub.Forms.Login;

namespace MidwestDevOps_Hub.Forms.FirstTimeSetup
{
    public partial class FirstTimeSetUp : Form
    {
        public FirstTimeSetUp()
        {
            InitializeComponent();
        }

        private void btnCompanySelection_Click(object sender, EventArgs e)
        {
            try
            {
                using (var clientBLL = new BusinessLogicLayer.HubMainClients(TextHasher.Decrypt(Utility.HubMainConnectionString)))
                {
                    var clients = clientBLL.GetClientByUUID(mtbProductKey.Text);

                    if (clients != null)
                    {
                        Connection con = new Connection();
                        con.UUID = clients.UUID;
                        con.Name = clients.ClientName;

                        ConnectionHandler.SaveConnectionString(con);

                        Utility.RestartApplication(this);
                    }
                    else
                    {
                        lblError.Text = "Error: Invalid Product Key";
                    }
                }
            }
            catch (Exception ex)
            {
                BusinessLogicLayer.Logging.SaveLog(new Log() { exception = ex, time = DateTime.Now });
                MessageBox.Show("Something went wrong", "Error");
            }
        }
    }
}
