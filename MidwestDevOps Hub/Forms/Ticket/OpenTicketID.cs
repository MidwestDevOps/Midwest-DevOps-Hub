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
    public partial class OpenTicketID : Form
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

        public OpenTicketID(Form form)
        {
            MainForm = form;

            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (IsDigitsOnly(textBox1.Text))
            {
                using (var ticketBLL = new BusinessLogicLayer.Tickets(Utility.GetConnectionString()))
                {
                    var ticket = ticketBLL.GetTicketByID(Convert.ToInt32(textBox1.Text));

                    if (ticket != null)
                    {
                        ShowTicket showTicket = new ShowTicket(MainForm, ticket.TicketID);
                        showTicket.MdiParent = MainForm;
                        showTicket.Show();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Couldn't find ticket id: " + textBox1.Text, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        bool IsDigitsOnly(string str)
        {
            foreach (char c in str)
            {
                if (c < '0' || c > '9')
                    return false;
            }

            return true;
        }
    }
}
