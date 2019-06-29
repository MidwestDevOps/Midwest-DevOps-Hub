using MidwestDevOps_Hub.Forms;
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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            BusinessLogicLayer.Projects c = new BusinessLogicLayer.Projects(Utility.GetConnectionString());

            var s = c.GetProjectByID(1);

            var ss = 1;
        }

        private void createProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var s = sender as ToolStripMenuItem;

            switch (s.AccessibilityObject.Name)
            {
                case "Create Project":
                    CreateProject createProject = new CreateProject();
                    createProject.MdiParent = this;
                    createProject.Show();
                    break;
            }
        }
    }
}
