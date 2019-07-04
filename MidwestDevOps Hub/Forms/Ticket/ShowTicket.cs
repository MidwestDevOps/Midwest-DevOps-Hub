using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MidwestDevOps_Hub.Forms.Ticket
{
    public partial class ShowTicket : Form
    {
        public Form MainForm = null;

        public int? TicketID = null;

        public DataEntities.Ticket Ticket = null;

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

            if (Ticket != null)
            {
                cbProject.SelectedValue = Ticket.ProjectID;
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

            if (Ticket != null)
            {
                cbCategory.SelectedValue = Ticket.CategoryID;
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

            if (Ticket != null)
            {
                cbPriority.SelectedValue = Ticket.PriorityID;
            }
        }

        private void UpdateProjectComboBox(List<DataEntities.Project> projects)
        {
            if (cbProject.InvokeRequired)
            {
                cbProject.Invoke(new MethodInvoker(() => cbProject.DataSource = projects.OrderBy(x => x.Name).ToList()));
                cbProject.Invoke(new MethodInvoker(() => cbProject.DisplayMember = "Name"));
                cbProject.Invoke(new MethodInvoker(() => cbProject.ValueMember = "ProjectID"));
            }
            else
            {
                cbProject.DataSource = projects.OrderBy(x => x.Name).ToList();
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

        public ShowTicket(Form form, int? ticketID)
        {
            MainForm = form;
            TicketID = ticketID;

            if (TicketID.HasValue)
                Ticket = TicketBLL.GetTicketByID(TicketID.Value);

            InitializeComponent();

            UpdateCategoryComboBox();
            UpdatePriorityComboBox();
            UpdateProjectComboBox();

            if (TicketID.HasValue)
            {
                this.Name = "Ticket ID: " + ticketID;
                lblTicketIDDisplay.Text = ticketID.ToString();

                rtbIssue.Text = Ticket.Issue;
                txtSubject.Text = Ticket.Subject;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Ticket.CategoryID = Convert.ToInt32(cbCategory.SelectedValue);
            Ticket.Issue = rtbIssue.Text;
            Ticket.PriorityID = Convert.ToInt32(cbPriority.SelectedValue);
            Ticket.ProjectID = Convert.ToInt32(cbProject.SelectedValue);
            Ticket.Subject = txtSubject.Text;

            long? id = TicketBLL.SaveTicket(Ticket);

            if (id != null)
            {
                if (id != null)
                {
                    MessageBox.Show("Successfully saved ticket id: " + id, "Success!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Couldn't save ticket id: " + TicketID, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else //Create
            {
                if (id != null)
                {
                    MessageBox.Show("Successfully created ticket id: " + id, "Success!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Couldn't save ticket id: " + TicketID, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
