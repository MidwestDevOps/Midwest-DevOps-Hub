using MidwestDevOps_Hub.Forms;
using MidwestDevOps_Hub.Forms.Ticket;
using MidwestDevOps_Hub.Forms.User;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
        public HubModels.UserSessionModel UserSession
        {
            get; set;
        }

        public Hub(HubModels.UserSessionModel userSessionModel)
        {
            UserSession = userSessionModel;

            InitializeComponent();
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
            }
        }

        private void Hub_FormClosing(object sender, FormClosingEventArgs e)
        {
            using (var userSessionBLL = new BusinessLogicLayer.UserSessions(Utility.GetConnectionString()))
            {
                var userSession = UserSession;
                userSession.StatusLID = (int)DataEntities.Lookup.UserSession.INACTIVE;

                userSessionBLL.SaveUserSession(userSession.ConvertToEntity());
            }
        }
    }
}
