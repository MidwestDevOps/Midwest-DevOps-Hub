using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MidwestDevOps_Hub.Forms
{
    public partial class AddEditProject : Form
    {
        public Form MainForm = null;
        public int? ProjectID = null;

        #region Business Layer

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

        #endregion

        #region Control Updates

        private void RefreshProjectData(int? projectID)
        {
            DataEntities.Project project = null;

            if (projectID.HasValue)
            {
                project = ProjectBLL.GetProjectByID(projectID.Value);

                if (project == null)
                {
                    MessageBox.Show("Couldn't retrieve project id: " + projectID, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    UpdateProjectData(project, false);
                }
            }
            else
            {
                UpdateProjectData(project, true);
            }
        }

        private void UpdateProjectData(DataEntities.Project project, bool isNew)
        {
            if (isNew)
            {
                if (lblTitle.InvokeRequired)
                {
                    lblTitle.Invoke(new MethodInvoker(() => this.Text = "New Project"));
                    lblTitle.Invoke(new MethodInvoker(() => lblTitle.Text = "New Project"));
                }
                else
                {
                    this.Text = "New Project";
                    lblTitle.Text = "New Project";
                }
            }
            else
            {
                if (txtName.InvokeRequired)
                {
                    txtName.Invoke(new MethodInvoker(() => this.Text = "Project: " + project.Name));
                    lblTitle.Invoke(new MethodInvoker(() => lblTitle.Text = project.Name));
                    lblCreated.Invoke(new MethodInvoker(() => lblCreated.Text = "Created: " + project.CreatedDate));
                    txtName.Invoke(new MethodInvoker(() => txtName.Text = project.Name));
                    rtbDescription.Invoke(new MethodInvoker(() => rtbDescription.Text = project.Description));
                }
                else
                {
                    this.Text = "Project: " + project.Name;
                    lblTitle.Text = project.Name;
                    lblCreated.Text = "Created: " + project.CreatedDate;

                    txtName.Text = project.Name;
                    rtbDescription.Text = project.Description;
                }
            }
        }

        #endregion

        public AddEditProject(int? projectID, Form mainForm)
        {
            ProjectID = projectID;
            MainForm = mainForm;

            InitializeComponent();

            Thread t = new Thread(() => RefreshProjectData(projectID));
            t.Start();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            HubModels.ProjectModel p = new HubModels.ProjectModel();

            p.ProjectID = ProjectID;
            p.Name = txtName.Text;
            p.Description = rtbDescription.Text;
            p.CreatedDate = DateTime.UtcNow;
            p.CreatedBy = 0;

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
