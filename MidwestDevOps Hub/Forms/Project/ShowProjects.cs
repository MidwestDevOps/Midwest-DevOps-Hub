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
    public partial class ShowProjects : Form
    {
        public Form MainForm = null;

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

        public ShowProjects(Form form)
        {
            MainForm = form;

            InitializeComponent();

            RefreshProjectListView();
        }

        private void RefreshProjectListView()
        {
            var projects = ProjectBLL.GetAllProjects().OrderBy(x => x.Name).ToList();

            lstProjects.DataSource = projects;
            lstProjects.DisplayMember = "Name";
            lstProjects.ValueMember = "ProjectID";
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
