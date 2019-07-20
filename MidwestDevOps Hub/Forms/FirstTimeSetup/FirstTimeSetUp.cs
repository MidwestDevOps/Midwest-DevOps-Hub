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

namespace MidwestDevOps_Hub.Forms.FirstTimeSetup
{
    public partial class FirstTimeSetUp : Form
    {
        public FirstTimeSetUp()
        {
            InitializeComponent();

            LoadClients();
        }

        private void LoadClients()
        {
            try
            {
                using (var clientBLL = new BusinessLogicLayer.HubMainClients(TextHasher.Decrypt(Utility.HubMainConnectionString)))
                {
                    var clients = clientBLL.GetAllClients(true);

                    cbCompanyList.ValueMember = "ClientID";
                    cbCompanyList.DisplayMember = "ClientName";
                    cbCompanyList.DataSource = clients;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Something went wrong", "Error");
            }
        }

        private void btnCompanySelection_Click(object sender, EventArgs e)
        {
            try
            {
                using (var clientBLL = new BusinessLogicLayer.HubMainClients(TextHasher.Decrypt(Utility.HubMainConnectionString)))
                {
                    var clients = clientBLL.GetClientByID(Convert.ToInt32(cbCompanyList.SelectedValue));

                    Connection con = new Connection();
                    con.UUID = clients.UUID;
                    con.Name = clients.ClientName;

                    ConnectionHandler.SaveConnectionString(con);

                    Utility.RestartApplication(this);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Something went wrong", "Error");
            }
        }
    }
}
