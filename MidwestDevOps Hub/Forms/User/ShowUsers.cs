using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MidwestDevOps_Hub.Forms.User
{
    public partial class ShowUsers : Form
    {
        public Form MainForm = null;

        public HubModels.ProjectModel projectModel = null;

        public ShowUsers(Form form)
        {
            MainForm = form;

            InitializeComponent();

            LoadUsers();
        }

        public void LoadUsers()
        {
            ltvUsers.Items.Clear();

            List<HubModels.UserModel.UserModelOnlineModel> userModels = new List<HubModels.UserModel.UserModelOnlineModel>();

            using (var userBLL = new BusinessLogicLayer.Users(Utility.GetConnectionString()))
            {
                var userSessionBLL = new BusinessLogicLayer.UserSessions(userBLL.GetConnection());

                var users = userBLL.GetAllUsers();

                foreach(var user in users)
                {
                    HubModels.UserModel.UserModelOnlineModel p = new HubModels.UserModel.UserModelOnlineModel();

                    p.UserID = user.UserID.Value;
                    p.UserName = user.Username;

                    var userSession = userSessionBLL.GetUserSessionLatestRecordForUserID(user.UserID.Value);

                    if (userSession != null)
                    {
                        p.UserSessionStatusLID = userSession.StatusLID;
                        p.LastOnline = userSession.CreatedDate;
                    }
                    else
                    {
                        p.UserSessionStatusLID = -1;
                        p.LastOnline = null;
                    }

                    userModels.Add(p);
                }
            }

            foreach (var userModel in userModels)
            {

                string status = "Unknown";

                if (userModel.UserSessionStatusLID == (int)DataEntities.Lookup.UserSession.ACTIVE)
                {
                    status = "Online";
                }
                else if (userModel.UserSessionStatusLID == (int)DataEntities.Lookup.UserSession.AWAY)
                {
                    status = "Away";
                }
                else if (userModel.UserSessionStatusLID == (int)DataEntities.Lookup.UserSession.INACTIVE)
                {
                    status = "Offline";
                }

                string[] row = { userModel.UserID.ToString(), userModel.UserName, "Nothin", status, userModel.LastOnline != null ? userModel.LastOnline.ToString() : null};

                ListViewItem item = new ListViewItem(row);

                if (userModel.UserSessionStatusLID == (int)DataEntities.Lookup.UserSession.ACTIVE)
                {
                    item.BackColor = Color.FromArgb(255, 40, 167, 69);
                }
                else if (userModel.UserSessionStatusLID == (int)DataEntities.Lookup.UserSession.AWAY)
                {
                    item.BackColor = Color.FromArgb(255, 255, 193, 7);
                }
                else if (userModel.UserSessionStatusLID == (int)DataEntities.Lookup.UserSession.INACTIVE)
                {
                    item.BackColor = Color.FromArgb(255, 220, 53, 69);
                }
                else
                {
                    item.BackColor = Color.FromArgb(255, 237, 237, 237);
                }

                ltvUsers.Items.Add(item);
            }

            lblTime.Text = "Last Updated: " + DateTime.Now;

            ltvUsers.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            ltvUsers.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);

            ltvUsers.Columns[0].Width = 0;
            //ltvUsers.Columns[0].Dispose();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadUsers();
        }
    }
}
