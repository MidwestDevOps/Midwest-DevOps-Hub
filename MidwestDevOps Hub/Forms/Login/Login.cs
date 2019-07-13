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
                        activityLogBLL.SaveActivityLog(new DataEntities.ActivityLog() { Action = "Login Success", Value = txtUsername.Text, ApplicationLID = (int)DataEntities.Lookup.Application.WINDOWSNET, CreatedDate = DateTime.Now, ReturnedLID = (int)DataEntities.Lookup.ActivityLog.SUCCESS, IP = ip });

                        var userSessionBLL = new BusinessLogicLayer.UserSessions(userBLL.GetConnection());

                        HubModels.UserSessionModel userSessionModel = new HubModels.UserSessionModel();
                        userSessionModel.GUID = Guid.NewGuid().ToString();
                        userSessionModel.UserID = user.UserID.Value;
                        userSessionModel.StatusLID = (int)DataEntities.Lookup.UserSession.ACTIVE;
                        userSessionModel.CreatedDate = DateTime.Now;

                        long? userSessionID = userSessionBLL.SaveUserSession(userSessionModel.ConvertToEntity());

                        if (userSessionID != null)
                        {
                            userSessionModel.UserSessionID = Convert.ToInt32(userSessionID);

                            this.Hide();
                            Hub f = new Hub(userSessionModel);
                            f.Closed += (s, args) => this.Close();
                            f.Show();
                        }
                    }
                    else
                    {
                        activityLogBLL.SaveActivityLog(new DataEntities.ActivityLog() { Action = "Login Password Incorrect", Value = txtUsername.Text + ": " + txtPassword.Text, ApplicationLID = (int)DataEntities.Lookup.Application.WINDOWSNET, CreatedDate = DateTime.Now, ReturnedLID = (int)DataEntities.Lookup.ActivityLog.FAILED, IP = ip });

                        FormReset();
                        lblErrorMessage.Text = "Username or password is incorrect.";
                    }
                }
                else
                {
                    activityLogBLL.SaveActivityLog(new DataEntities.ActivityLog() { Action = "Login Username NotFound", Value = txtUsername.Text, ApplicationLID = (int)DataEntities.Lookup.Application.WINDOWSNET, CreatedDate = DateTime.Now, ReturnedLID = (int)DataEntities.Lookup.ActivityLog.FAILED, IP = ip });

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
