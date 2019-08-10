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
    public partial class TicketCategoryDependencies : Form
    {

        public Hub MainForm = null;
        public int? OldCategory = null;
        public ManageTicketCategories ManageTicketCategories = null;

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

        public TicketCategoryDependencies(Hub form, ManageTicketCategories manageTicketCategories, int oldCategory)
        {
            MainForm = form;
            ManageTicketCategories = manageTicketCategories;
            OldCategory = oldCategory;

            InitializeComponent();

            PopulateComboBox();
        }

        private void PopulateComboBox()
        {
            using (var ticketCategoryBLL = new BusinessLogicLayer.TicketCategories(Utility.GetConnectionString()))
            {
                var categories = ticketCategoryBLL.GetAllCategories().Where(x => x.TicketCategoryID != OldCategory).ToList();

                cboCategories.ValueMember = "TicketCategoryID";
                cboCategories.DisplayMember = "Name";
                cboCategories.DataSource = categories;

                if (categories.Count() <= 0)
                {
                    MessageBox.Show("Can't have less than 1 category", "Warning");
                    this.Close();
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(cboCategories.SelectedItem.ToString()))
            {
                using (var ticketCategoryBLL = new BusinessLogicLayer.TicketCategories(Utility.GetConnectionString()))
                {
                    var cboID = (DataEntities.TicketCategory)cboCategories.SelectedItem;
                    var category = ticketCategoryBLL.GetCategoryByID(cboID.TicketCategoryID.Value);

                    if (category != null)
                    {
                        var numSwitched = ticketCategoryBLL.ReplaceDependentTickets(cboID.TicketCategoryID.Value, OldCategory.Value);

                        if (numSwitched > 0)
                        {
                            MessageBox.Show("Success changed " + numSwitched + " tickets", "Success");
                            DialogResult dialogResult = MessageBox.Show("Do you still want to delete your old category?", "Ticket Category", MessageBoxButtons.YesNo);
                            if (dialogResult == DialogResult.Yes)
                            {
                                var oldCategory = ticketCategoryBLL.GetCategoryByID(OldCategory.Value);

                                var success = ticketCategoryBLL.DeleteCategory(oldCategory.TicketCategoryID.Value);

                                if (success)
                                {
                                    MessageBox.Show("Deleted category successfully", "Success");
                                    ManageTicketCategories.UpdateCategories(true);
                                }
                                else
                                {
                                    MessageBox.Show("Couldn't delete category", "Error");
                                }
                            }
                            else if (dialogResult == DialogResult.No)
                            {

                            }
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("No tickets were switched over", "Warning");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Couldn't find category id: " + cboCategories.SelectedItem.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Not a correct category selected", "Error");
            }
        }
    }
}
