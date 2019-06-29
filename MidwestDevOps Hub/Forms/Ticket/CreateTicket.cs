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
    public partial class CreateTicket : Form
    {
        public Form MainForm = null;

        #region BusinessLayer

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

        public CreateTicket(Form form)
        {
            MainForm = form;

            InitializeComponent();

            UpdateProjectComboBox();
            UpdateCategoryComboBox();
            UpdatePriorityComboBox();
        }

        private void UpdateProjectComboBox()
        {
            cbProject.DataSource = ProjectBLL.GetAllProjects().OrderBy(x => x.Name).ToList();
            cbProject.DisplayMember = "Name";
            cbProject.ValueMember = "ProjectID";
        }

        private void UpdateCategoryComboBox()
        {
            cbCategory.DataSource = CategoryBLL.GetAllCategories().OrderBy(x => x.Name).ToList();
            cbCategory.DisplayMember = "Name";
            cbCategory.ValueMember = "TicketCategoryID";
        }

        private void UpdatePriorityComboBox()
        {
            cbPriority.DataSource = PriorityBLL.GetAllPriorities().OrderBy(x => x.Name).ToList();
            cbPriority.DisplayMember = "Name";
            cbPriority.ValueMember = "TicketPriorityID";
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            HubModels.TicketModel t = new HubModels.TicketModel();

            t.ProjectID = Convert.ToInt32(cbProject.SelectedValue);
            t.CategoryID = Convert.ToInt32(cbCategory.SelectedValue);
            t.PriorityID = Convert.ToInt32(cbPriority.SelectedValue);
            t.Subject = txtSubject.Text;
            t.Issue = rtbIssue.Text;

            long? id = TicketBLL.SaveTicket(t.ConvertToEntity());

            if (id != null)
            {
                MessageBox.Show("Successfully created ticket id: " + id, "Success!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Couldn't create ticket", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
