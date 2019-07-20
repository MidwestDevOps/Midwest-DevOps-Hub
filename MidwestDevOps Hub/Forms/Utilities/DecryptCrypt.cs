using BusinessLogicLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MidwestDevOps_Hub.Forms.Utilities
{
    public partial class DecryptCrypt : Form
    {
        public Hub MainForm = null;

        public DecryptCrypt(Hub hub)
        {
            MainForm = hub;

            InitializeComponent();
        }

        private void btnCrypt_Click(object sender, EventArgs e)
        {
            var s = TextHasher.Crypt(rtbInput.Text);
            rtbOutput.Text = s;
        }

        private void btnDecrypt_Click(object sender, EventArgs e)
        {
            var s = TextHasher.Decrypt(rtbInput.Text);
            rtbOutput.Text = s;
        }
    }
}
