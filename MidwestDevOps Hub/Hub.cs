using MidwestDevOps_Hub.Forms;
using MidwestDevOps_Hub.Forms.Login;
using MidwestDevOps_Hub.Forms.Ticket;
using MidwestDevOps_Hub.Forms.User;
using MidwestDevOps_Hub.Forms.Utilities;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MidwestDevOps_Hub
{
    public partial class Hub : Form
    {
        Timer timeOutTimer = new Timer();

        public HubModels.UserSessionModel UserSession
        {
            get; set;
        }

        public Hub(HubModels.UserSessionModel userSessionModel)
        {
            UserSession = userSessionModel;

            InitializeComponent();

            foreach (Control ctrl in this.Controls)
            {
                if (ctrl is MdiClient)
                {
                    ctrl.BackColor = Color.FromArgb(255, 31, 169, 156);
                    ctrl.BackgroundImage = MidwestDevOps_Hub.Properties.Resources.Logo;
                    ctrl.BackgroundImageLayout = ImageLayout.Center; //Doesn't get set
                }
            }

            Timer inactivityTimer = new Timer();
            inactivityTimer.Tick += InactivityTimerEvent;
            inactivityTimer.Interval = 5000;
            inactivityTimer.Start();
        }

        private void InactivityTimerEvent(object sender, EventArgs e)
        {
            uint inactiveMilliseconds = Utility.GetInactiveMilliseconds();

            if (inactiveMilliseconds >= 600000 && inactiveMilliseconds < 65000000) //10 minutes
            {
                if (UserSession.StatusLID != (int)DataEntities.Lookup.UserSession.AWAY)
                {
                    using (var userSessionBLL = new BusinessLogicLayer.UserSessions(Utility.GetConnectionString()))
                    {
                        var userSession = userSessionBLL.GetUserSessionLatestRecordForUserID(UserSession.UserID);

                        userSession.StatusLID = (int)DataEntities.Lookup.UserSession.AWAY;

                        UserSession = new HubModels.UserSessionModel(userSession);

                        userSessionBLL.SaveUserSession(userSession);
                    }
                }
            }
            else if (inactiveMilliseconds >= 65000000) //.75 days
            {
                Utility.RestartApplication(this);
            }
            else
            {
                if (UserSession.StatusLID != (int)DataEntities.Lookup.UserSession.ACTIVE)
                {
                    using (var userSessionBLL = new BusinessLogicLayer.UserSessions(Utility.GetConnectionString()))
                    {
                        var userSession = userSessionBLL.GetUserSessionLatestRecordForUserID(UserSession.UserID);

                        userSession.StatusLID = (int)DataEntities.Lookup.UserSession.ACTIVE;

                        UserSession = new HubModels.UserSessionModel(userSession);

                        userSessionBLL.SaveUserSession(userSession);
                    }
                }
            }
        }

        private void createProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var s = sender as ToolStripMenuItem;

            switch (s.AccessibilityObject.Name)
            {
                case "Create Project":
                    AddEditProject createProject = new AddEditProject(null, this);
                    createProject.MdiParent = this;
                    createProject.Show();
                    break;
                case "View Projects":
                    ShowProjects editProject = new ShowProjects(this);
                    editProject.MdiParent = this;
                    editProject.Show();
                    break;
                case "Create Ticket":
                    CreateTicket createTicket = new CreateTicket(this);
                    createTicket.MdiParent = this;
                    createTicket.Show();
                    break;
                case "View Tickets":
                    ShowTickets showTickets = new ShowTickets(this);
                    showTickets.MdiParent = this;
                    showTickets.Show();
                    break;
                case "Open Ticket":
                    OpenTicketID openTicket = new OpenTicketID(this);
                    openTicket.MdiParent = this;
                    openTicket.Show();
                    break;
                case "View Users":
                    ShowUsers showUsers = new ShowUsers(this);
                    showUsers.MdiParent = this;
                    showUsers.Show();
                    break;
                case "Crypt / Decrypt":
                    DecryptCrypt decryptCrypt = new DecryptCrypt(this);
                    decryptCrypt.MdiParent = this;
                    decryptCrypt.Show();
                    break;
                case "Open Hub Folder":
                    Process.Start(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "MidwestDevOps"));
                    break;
            }
        }

        private void Hub_FormClosing(object sender, FormClosingEventArgs e)
        {
            using (var userSessionBLL = new BusinessLogicLayer.UserSessions(Utility.GetConnectionString()))
            {
                var userSession = UserSession;
                userSession.StatusLID = (int)DataEntities.Lookup.UserSession.INACTIVE;
                userSession.ExpireDate = DateTime.Now.AddDays(-1);

                userSessionBLL.SaveUserSession(userSession.ConvertToEntity());
            }
        }
    }
}
