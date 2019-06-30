using MidwestDevOps_Hub.Forms;
using MidwestDevOps_Hub.Forms.Ticket;
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
        public Hub()
        {
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
            }
        }
    }
}
