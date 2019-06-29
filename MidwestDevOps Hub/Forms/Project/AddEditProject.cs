using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MidwestDevOps_Hub.Forms
{
    public partial class AddEditProject : Form
    {
        public Form MainForm = null;
        public int? ProjectID = null;

        public HubModels.ProjectModel projectModel = null;

        private BusinessLogicLayer.Projects _projectBLL
        {
            get; set;
        }

        public BusinessLogicLayer.Projects ProjectBLL
        {
            get
            {
                if (_projectBLL == null)
                {
                    _projectBLL = new BusinessLogicLayer.Projects(Utility.GetConnectionString());
                }

                return _projectBLL;
            }
        }

        public AddEditProject(int? projectID, Form mainForm)
        {
            ProjectID = projectID;
            MainForm = mainForm;

            InitializeComponent();

            if (projectID.HasValue)
            {
                var p = ProjectBLL.GetProjectByID(projectID.Value);

                if (p == null)
                {
                    MessageBox.Show("Couldn't retrieve project id: " + projectID, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    this.Text = "Project: " + p.Name;
                    lblTitle.Text = p.Name;
                    lblCreated.Text = "Created: " + p.CreatedDate;

                    txtName.Text = p.Name;
                    rtbDescription.Text = p.Description;

                }
            }
            else
            {
                this.Text = "New Project";
                lblTitle.Text = "New Project";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            HubModels.ProjectModel p = new HubModels.ProjectModel();

            p.ProjectID = ProjectID;
            p.Name = txtName.Text;
            p.Description = rtbDescription.Text;
            p.CreatedDate = DateTime.UtcNow;

            long? id = ProjectBLL.SaveProject(p.ConvertToEntity());

            if (ProjectID != null) //Update
            {
                if (id != null)
                {
                    MessageBox.Show("Successfully saved project id: " + id, "Success!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Couldn't save project id: " + ProjectID, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else //Create
            {
                if (id != null)
                {
                    MessageBox.Show("Successfully created project id: " + id, "Success!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Couldn't save project id: " + ProjectID, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
