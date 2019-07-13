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

namespace MidwestDevOps_Hub.Forms.Ticket
{
    public partial class CreateTicket : Form
    {
        public Form MainForm = null;

        #region Business Layer

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

        private BusinessLogicLayer.TicketCategories _categoryBLL
        {
            get; set;
        }

        public BusinessLogicLayer.TicketCategories CategoryBLL
        {
            get
            {
                if (_categoryBLL == null)
                {
                    _categoryBLL = new BusinessLogicLayer.TicketCategories(Utility.GetConnectionString());
                }

                return _categoryBLL;
            }
        }

        private BusinessLogicLayer.TicketPriorities _priorityBLL
        {
            get; set;
        }

        public BusinessLogicLayer.TicketPriorities PriorityBLL
        {
            get
            {
                if (_priorityBLL == null)
                {
                    _priorityBLL = new BusinessLogicLayer.TicketPriorities(Utility.GetConnectionString());
                }

                return _priorityBLL;
            }
        }

        private BusinessLogicLayer.Tickets _ticketBLL
        {
            get; set;
        }

        public BusinessLogicLayer.Tickets TicketBLL
        {
            get
            {
                if (_ticketBLL == null)
                {
                    _ticketBLL = new BusinessLogicLayer.Tickets(Utility.GetConnectionString());
                }

                return _ticketBLL;
            }
        }

        #endregion

        #region Control Updates

        private void UpdateProjectComboBox()
        {
            var projects = ProjectBLL.GetAllProjects();

            if (projects == null)
            {
                MessageBox.Show("Couldn't retrieve projects", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                UpdateProjectComboBox(projects);
            }
        }

        private void UpdateCategoryComboBox()
        {
            var categories = CategoryBLL.GetAllCategories();

            if (categories == null)
            {
                MessageBox.Show("Couldn't retrieve categories", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                UpdateCategoryComboBox(categories);
            }
        }

        private void UpdatePriorityComboBox()
        {
            var priorities = PriorityBLL.GetAllPriorities();

            if (priorities == null)
            {
                MessageBox.Show("Couldn't retrieve priorities", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                UpdatePriorityComboBox(priorities);
            }
        }

        private void UpdateProjectComboBox(List<DataEntities.Project> projects)
        {
            if (cbProject.InvokeRequired)
            {
                cbProject.Invoke(new MethodInvoker(() => cbProject.DataSource = projects.ToList()));
                cbProject.Invoke(new MethodInvoker(() => cbProject.DisplayMember = "Name"));
                cbProject.Invoke(new MethodInvoker(() => cbProject.ValueMember = "ProjectID"));
            }
            else
            {
                cbProject.DataSource = projects.ToList();
                cbProject.DisplayMember = "Name";
                cbProject.ValueMember = "ProjectID";
            }
        }

        private void UpdateCategoryComboBox(List<DataEntities.TicketCategory> categories)
        {
            if (cbProject.InvokeRequired)
            {
                cbCategory.Invoke(new MethodInvoker(() => cbCategory.DataSource = categories.OrderBy(x => x.Name).ToList()));
                cbCategory.Invoke(new MethodInvoker(() => cbCategory.DisplayMember = "Name"));
                cbCategory.Invoke(new MethodInvoker(() => cbCategory.ValueMember = "TicketCategoryID"));
            }
            else
            {
                cbCategory.DataSource = categories.OrderBy(x => x.Name).ToList();
                cbCategory.DisplayMember = "Name";
                cbCategory.ValueMember = "TicketCategoryID";
            }
        }

        private void UpdatePriorityComboBox(List<DataEntities.TicketPriority> priorities)
        {
            if (cbPriority.InvokeRequired)
            {
                cbPriority.Invoke(new MethodInvoker(() => cbPriority.DataSource = priorities.OrderBy(x => x.Name).ToList()));
                cbPriority.Invoke(new MethodInvoker(() => cbPriority.DisplayMember = "Name"));
                cbPriority.Invoke(new MethodInvoker(() => cbPriority.ValueMember = "TicketPriorityID"));
            }
            else
            {
                cbPriority.DataSource = priorities.OrderBy(x => x.Name).ToList();
                cbPriority.DisplayMember = "Name";
                cbPriority.ValueMember = "TicketPriorityID";
            }
        }

        #endregion

        public CreateTicket(Form form)
        {
            MainForm = form;

            InitializeComponent();

            Thread tProject = new Thread(new ThreadStart(UpdateProjectComboBox));
            tProject.Start();

            Thread tCategory = new Thread(new ThreadStart(UpdateCategoryComboBox));
            tCategory.Start();

            Thread tPriority = new Thread(new ThreadStart(UpdatePriorityComboBox));
            tPriority.Start();
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            HubModels.TicketModel t = new HubModels.TicketModel();

            t.ProjectID = Convert.ToInt32(cbProject.SelectedValue);
            t.CategoryID = Convert.ToInt32(cbCategory.SelectedValue);
            t.PriorityID = Convert.ToInt32(cbPriority.SelectedValue);
            t.Subject = txtSubject.Text;
            t.Issue = rtbIssue.Text;

            t.CreatedBy = 0;
            t.CreatedDate = DateTime.Now;
            t.Active = true;

            long? id = TicketBLL.SaveTicket(t.ConvertToEntity());

            if (id != null)
            {
                MessageBox.Show("Successfully created ticket id: " + id, "Success!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ShowTicket showTicket = new ShowTicket(MainForm, id == null ? (int?)null : Convert.ToInt32(id));
                showTicket.MdiParent = MainForm;
                showTicket.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Couldn't create ticket", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
