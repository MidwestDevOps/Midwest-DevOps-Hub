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
    public partial class ShowProjects : Form
    {
        public Hub MainForm = null;

        public HubModels.ProjectModel projectModel = null;

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

        private void RefreshProjectListView()
        {
            using (var projectBLL = new BusinessLogicLayer.Projects(Utility.GetConnectionString()))
            {

                var projects = projectBLL.GetAllProjects().OrderBy(x => x.Name).ToList();

                UpdateProjectListView(projects);
            }
        }

        private void UpdateProjectListView(List<DataEntities.Project> projects)
        {
            if (lstProjects.InvokeRequired)
            {
                lstProjects.Invoke(new MethodInvoker(() => lstProjects.DataSource = projects));
                lstProjects.Invoke(new MethodInvoker(() => lstProjects.DisplayMember = "Name"));
                lstProjects.Invoke(new MethodInvoker(() => lstProjects.ValueMember = "ProjectID"));
            }
            else
            {
                lstProjects.DataSource = projects;
                lstProjects.DisplayMember = "Name";
                lstProjects.ValueMember = "ProjectID";
            }
        }

        #endregion

        public ShowProjects(Hub form)
        {
            MainForm = form;

            InitializeComponent();

            lstProjects.Items.Add("Loading...");

            Thread t = new Thread(new ThreadStart(RefreshProjectListView));
            t.Start();
        }

        private void lstProjects_DoubleClick(object sender, EventArgs e)
        {
            if (lstProjects.SelectedValue != null)
            {
                AddEditProject addEditProject = new AddEditProject(Convert.ToInt32(lstProjects.SelectedValue), MainForm);
                addEditProject.MdiParent = MainForm;
                addEditProject.Show();
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            RefreshProjectListView();
        }
    }
}
