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
    public partial class ManageTicketCategories : Form
    {
        public Hub MainForm = null;
        public bool CreateNewCategory = true;
        public string SelectedColor = "";

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

        private void RefreshCategoryData()
        {
            using (var ticketCategoriesBLL = new BusinessLogicLayer.TicketCategories(Utility.GetConnectionString()))
            {
                var categories = ticketCategoriesBLL.GetAllCategories();

                if (categories == null)
                {
                    MessageBox.Show("Couldn't retrieve categories", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    UpdateCategoryList(categories);
                }
            }
        }

        private void UpdateCategoryList(List<DataEntities.TicketCategory> categories)
        {
            if (ltvTickets.InvokeRequired)
            {
                ltvTickets.Invoke(new MethodInvoker(() => ltvTickets.Items.Clear()));
            }

            foreach (var i in categories)
            {
                string[] row = { i.TicketCategoryID.ToString(), i.Name, i.Color };

                if (ltvTickets.InvokeRequired)
                {
                    ltvTickets.Invoke(new MethodInvoker(() => ltvTickets.Items.Add(new ListViewItem(row))));
                }
                else
                {
                    ltvTickets.Invoke(new MethodInvoker(() => ltvTickets.Items.Add(new ListViewItem(row))));
                }
            }
        }

        #endregion

        #region Events

        private void btnClearCategory_Click(object sender, EventArgs e)
        {
            LoadEditFields(new DataEntities.TicketCategory());
        }

        private void txtColor_DoubleClick(object sender, EventArgs e)
        {
            colorDialog1.ShowDialog();
        }

        private void ManageTicketCategories_MouseEnter(object sender, EventArgs e)
        {
            UpdateColorTextbox();
        }

        private void ltvTickets_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ltvTickets.SelectedItems.Count > 0 && ltvTickets.SelectedItems[0] != null)
            {
                using (var ticketCategoryBLL = new BusinessLogicLayer.TicketCategories(Utility.GetConnectionString()))
                {
                    var ticketCategory = ticketCategoryBLL.GetCategoryByID(Convert.ToInt32(ltvTickets.SelectedItems[0].Text));

                    if (ticketCategory != null)
                    {
                        LoadEditFields(ticketCategory);
                    }
                    else
                    {
                        MessageBox.Show("Couldn't load ticket category", "Error");
                    }
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            using (var ticketCategoryBLL = new BusinessLogicLayer.TicketCategories(Utility.GetConnectionString()))
            {
                var mCategory = new DataEntities.TicketCategory();

                if (!string.IsNullOrEmpty(txtName.Text))
                {

                    if (!string.IsNullOrEmpty(txtID.Text))
                    {
                        mCategory = ticketCategoryBLL.GetCategoryByID(Convert.ToInt32(txtID.Text));

                        mCategory.TicketCategoryID = Convert.ToInt32(txtID.Text);
                        mCategory.Name = txtName.Text;
                        mCategory.Color = string.IsNullOrEmpty(txtColor.Text) ? "FFFFFF" : txtColor.Text;
                    }
                    else
                    {
                        mCategory.Name = txtName.Text;
                        mCategory.Color = txtColor.Text;

                        mCategory.Active = true;
                        mCategory.CreatedBy = MainForm.UserSession.UserID;
                        mCategory.CreatedDate = DateTime.Now;
                    }

                    var saveCategory = ticketCategoryBLL.SaveCategory(mCategory);

                    if (saveCategory == null)
                    {
                        MessageBox.Show("Couldn't save ticket category", "Error");
                    }
                    else
                    {
                        var newCategory = ticketCategoryBLL.GetCategoryByID(Convert.ToInt32(saveCategory));

                        if (newCategory != null)
                        {
                            LoadEditFields(newCategory);
                        }
                    }

                    UpdateCategories(false);
                }
                else
                {
                    MessageBox.Show("Name can't be empty", "Validation");
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtID.Text))
            {
                using (var ticketCategoryBLL = new BusinessLogicLayer.TicketCategories(Utility.GetConnectionString()))
                {
                    var category = ticketCategoryBLL.GetCategoryByID(Convert.ToInt32(txtID.Text));

                    if (category != null)
                    {
                        var numDependents = ticketCategoryBLL.NumberOfDependentTickets(category.TicketCategoryID.Value);

                        if (numDependents <= 0 || numDependents == null)
                        {
                            if (ticketCategoryBLL.DeleteCategory(category.TicketCategoryID.Value))
                            {
                                UpdateCategories(true);

                                LoadEditFields(new DataEntities.TicketCategory());

                                MessageBox.Show("Category deleted successfully", "Success");
                            }
                            else
                            {
                                MessageBox.Show("Couldn't delete category", "Error");
                            }
                        }
                        else
                        {
                            TicketCategoryDependencies categoryDependencies = new TicketCategoryDependencies(MainForm, this, category.TicketCategoryID.Value);
                            categoryDependencies.MdiParent = MainForm;
                            categoryDependencies.Show();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Couldn't find category", "Error");
                    }
                }
            }
        }

        #endregion

        public ManageTicketCategories(Hub form)
        {
            MainForm = form;

            InitializeComponent();

            LoadEditFields(new DataEntities.TicketCategory());

            Thread t = new Thread(() => RefreshCategoryData());
            t.Start();
        }

        public void UpdateCategories(bool clearTextBox)
        {
            Thread t = new Thread(() => RefreshCategoryData());
            t.Start();

            if (clearTextBox)
            {
                txtID.Text = "";
                txtName.Text = "";
                txtColor.Text = "";
            }
        }

        private void UpdateColorTextbox()
        {
            SelectedColor = (colorDialog1.Color.ToArgb() & 0x00FFFFFF).ToString("X6");

            if (SelectedColor != "000000")
            {
                txtColor.Text = SelectedColor;
                Color c = Color.FromArgb(Convert.ToInt32(SelectedColor, 16));
                txtColor.BackColor = Color.FromArgb(255, c.R, c.G, c.B);
            }
        }

        private void LoadEditFields(DataEntities.TicketCategory ticketCategory)
        {
            txtID.Text = ticketCategory.TicketCategoryID.ToString();
            txtName.Text = ticketCategory.Name;
            txtColor.Text = ticketCategory.Color;

            if (string.IsNullOrEmpty(txtID.Text)) //No category selected
            {
                btnSave.Text = "Create Category";
                CreateNewCategory = true;
                btnClearCategory.Visible = false;
                btnDelete.Visible = false;
            }
            else //A category is selected
            {
                btnSave.Text = "Update Category";
                btnClearCategory.Text = "Create Category";
                CreateNewCategory = false;
                btnClearCategory.Visible = true;
                btnDelete.Visible = true;
            }

            SelectedColor = ticketCategory.Color;
            if (!string.IsNullOrEmpty(SelectedColor))
            {
                colorDialog1.Color = Color.FromArgb(Convert.ToInt32(SelectedColor, 16));
                UpdateColorTextbox();
            }
            else
            {
                colorDialog1.Color = Color.FromArgb(255, 255, 255, 255);
                UpdateColorTextbox();
            }
        }
    }
}
