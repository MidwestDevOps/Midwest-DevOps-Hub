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
            string ip = Utility.GetIPAddress();

            using (var userBLL = new BusinessLogicLayer.Users(Utility.GetConnectionString()))
            {
                var activityLogBLL = new BusinessLogicLayer.ActivityLogs(userBLL.GetConnection());

                var user = userBLL.GetUserByUsername(txtUsername.Text);

                if (user != null)
                {
                    if (userBLL.VerifyPassword(user, txtPassword.Text))
                    {
                        activityLogBLL.SaveActivityLog(new DataEntities.ActivityLog() { Action = "LoginSuccess", Value = txtUsername.Text, CreatedDate = DateTime.Now, ReturnedLID = (int)DataEntities.Lookup.ActivityLog.SUCCESS, IP = ip });

                        this.Hide();
                        Hub f = new Hub();
                        f.Closed += (s, args) => this.Close();
                        f.Show();
                    }
                    else
                    {
                        activityLogBLL.SaveActivityLog(new DataEntities.ActivityLog() { Action = "LoginPasswordIncorrect", Value = txtPassword.Text, CreatedDate = DateTime.Now, ReturnedLID = (int)DataEntities.Lookup.ActivityLog.FAILED, IP = ip });

                        FormReset();
                        lblErrorMessage.Text = "Username or password is incorrect.";
                    }
                }
                else
                {
                    activityLogBLL.SaveActivityLog(new DataEntities.ActivityLog() { Action = "LoginUsernameNotFound", Value = txtUsername.Text, CreatedDate = DateTime.Now, ReturnedLID = (int)DataEntities.Lookup.ActivityLog.FAILED, IP = ip });

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
