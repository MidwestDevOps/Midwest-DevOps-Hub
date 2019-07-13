using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;

namespace MidwestDevOps_Hub.Forms.Login
{
    public partial class Login : Form
    {

        #region Business Layer

        private BusinessLogicLayer.Users _userBLL
        {
            get; set;
        }

        public BusinessLogicLayer.Users UserBLL
        {
            get
            {
                if (_userBLL == null)
                {
                    _userBLL = new BusinessLogicLayer.Users(Utility.GetConnectionString());
                }

                return _userBLL;
            }
        }

        #endregion

        public Login()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            using (var userBLL = new BusinessLogicLayer.Users(Utility.GetConnectionString()))
            {
                var user = userBLL.GetUserByUsername(txtUsername.Text);

                if (user != null)
                {
                    if (userBLL.VerifyPassword(user, txtPassword.Text))
                    {
                        this.Hide();
                        Hub f = new Hub();
                        f.Closed += (s, args) => this.Close();
                        f.Show();
                    }
                    else
                    {
                        FormReset();

                        lblErrorMessage.Text = "Username or password is incorrect.";
                    }
                }
                else
                {
                    FormReset();

                    lblErrorMessage.Text = "Username or password is incorrect.";
                }
            }
        }

        private void FormReset()
        {
            //txtUsername.Text = "";
            txtPassword.Text = "";
            lblErrorMessage.Text = "";
        }
    }
}
