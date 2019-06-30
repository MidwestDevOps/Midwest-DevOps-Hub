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
    public partial class ShowTickets : Form
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

        private void RefreshTicketData()
        {
            var tickets = TicketBLL.GetAllTickets();

            if (tickets == null)
            {
                MessageBox.Show("Couldn't retrieve tickets", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                UpdateTicketList(tickets);
            }
        }

        private void UpdateTicketList(List<DataEntities.Ticket> tickets)
        {
            foreach (var i in tickets)
            {
                string[] row = { i.Subject, i.ProjectName, i.CategoryName };

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

        public ShowTickets(Form form)
        {
            MainForm = form;

            InitializeComponent();

            Thread t = new Thread(() => RefreshTicketData());
            t.Start();
        }

        private void listView1_DoubleClick(object sender, EventArgs e)
        {

        }
    }
}
