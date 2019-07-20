namespace MidwestDevOps_Hub.Forms.FirstTimeSetup
{
    partial class FirstTimeSetUp
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FirstTimeSetUp));
            this.cbCompanyList = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnCompanySelection = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // cbCompanyList
            // 
            this.cbCompanyList.FormattingEnabled = true;
            this.cbCompanyList.Location = new System.Drawing.Point(18, 129);
            this.cbCompanyList.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cbCompanyList.Name = "cbCompanyList";
            this.cbCompanyList.Size = new System.Drawing.Size(264, 28);
            this.cbCompanyList.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(13, 9);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(155, 25);
            this.label1.TabIndex = 1;
            this.label1.Text = "First Time Setup";
            // 
            // btnCompanySelection
            // 
            this.btnCompanySelection.Location = new System.Drawing.Point(101, 182);
            this.btnCompanySelection.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnCompanySelection.Name = "btnCompanySelection";
            this.btnCompanySelection.Size = new System.Drawing.Size(100, 35);
            this.btnCompanySelection.TabIndex = 2;
            this.btnCompanySelection.Text = "Next";
            this.btnCompanySelection.UseVisualStyleBackColor = true;
            this.btnCompanySelection.Click += new System.EventHandler(this.btnCompanySelection_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(50, 73);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(194, 20);
            this.label2.TabIndex = 3;
            this.label2.Text = "Please select your company:";
            // 
            // FirstTimeSetUp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(295, 231);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnCompanySelection);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbCompanyList);
            this.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "FirstTimeSetUp";
            this.Text = "First Time Setup";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbCompanyList;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnCompanySelection;
        private System.Windows.Forms.Label label2;
    }
}