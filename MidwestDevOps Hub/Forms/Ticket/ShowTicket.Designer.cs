namespace MidwestDevOps_Hub.Forms.Ticket
{
    partial class ShowTicket
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ShowTicket));
            this.lblPriority = new System.Windows.Forms.Label();
            this.lblTicketID = new System.Windows.Forms.Label();
            this.lblCategory = new System.Windows.Forms.Label();
            this.lblProject = new System.Windows.Forms.Label();
            this.lblSubject = new System.Windows.Forms.Label();
            this.lblIssue = new System.Windows.Forms.Label();
            this.rtbIssue = new System.Windows.Forms.RichTextBox();
            this.txtSubject = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.lblTicketIDDisplay = new System.Windows.Forms.Label();
            this.cbPriority = new System.Windows.Forms.ComboBox();
            this.cbProject = new System.Windows.Forms.ComboBox();
            this.cbCategory = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // lblPriority
            // 
            this.lblPriority.AutoSize = true;
            this.lblPriority.Location = new System.Drawing.Point(29, 69);
            this.lblPriority.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPriority.Name = "lblPriority";
            this.lblPriority.Size = new System.Drawing.Size(59, 20);
            this.lblPriority.TabIndex = 0;
            this.lblPriority.Text = "Priority:";
            // 
            // lblTicketID
            // 
            this.lblTicketID.AutoSize = true;
            this.lblTicketID.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTicketID.Location = new System.Drawing.Point(13, 9);
            this.lblTicketID.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTicketID.Name = "lblTicketID";
            this.lblTicketID.Size = new System.Drawing.Size(95, 25);
            this.lblTicketID.TabIndex = 1;
            this.lblTicketID.Text = "Ticket ID:";
            // 
            // lblCategory
            // 
            this.lblCategory.AutoSize = true;
            this.lblCategory.Location = new System.Drawing.Point(16, 137);
            this.lblCategory.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCategory.Name = "lblCategory";
            this.lblCategory.Size = new System.Drawing.Size(72, 20);
            this.lblCategory.TabIndex = 2;
            this.lblCategory.Text = "Category:";
            // 
            // lblProject
            // 
            this.lblProject.AutoSize = true;
            this.lblProject.Location = new System.Drawing.Point(30, 103);
            this.lblProject.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblProject.Name = "lblProject";
            this.lblProject.Size = new System.Drawing.Size(58, 20);
            this.lblProject.TabIndex = 3;
            this.lblProject.Text = "Project:";
            // 
            // lblSubject
            // 
            this.lblSubject.AutoSize = true;
            this.lblSubject.Location = new System.Drawing.Point(240, 27);
            this.lblSubject.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSubject.Name = "lblSubject";
            this.lblSubject.Size = new System.Drawing.Size(61, 20);
            this.lblSubject.TabIndex = 4;
            this.lblSubject.Text = "Subject:";
            // 
            // lblIssue
            // 
            this.lblIssue.AutoSize = true;
            this.lblIssue.Location = new System.Drawing.Point(257, 64);
            this.lblIssue.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblIssue.Name = "lblIssue";
            this.lblIssue.Size = new System.Drawing.Size(44, 20);
            this.lblIssue.TabIndex = 5;
            this.lblIssue.Text = "Issue:";
            // 
            // rtbIssue
            // 
            this.rtbIssue.Location = new System.Drawing.Point(308, 61);
            this.rtbIssue.Name = "rtbIssue";
            this.rtbIssue.Size = new System.Drawing.Size(134, 96);
            this.rtbIssue.TabIndex = 6;
            this.rtbIssue.Text = "";
            // 
            // txtSubject
            // 
            this.txtSubject.Location = new System.Drawing.Point(308, 24);
            this.txtSubject.Name = "txtSubject";
            this.txtSubject.Size = new System.Drawing.Size(134, 27);
            this.txtSubject.TabIndex = 7;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(306, 170);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(136, 33);
            this.btnSave.TabIndex = 8;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // lblTicketIDDisplay
            // 
            this.lblTicketIDDisplay.AutoSize = true;
            this.lblTicketIDDisplay.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTicketIDDisplay.Location = new System.Drawing.Point(116, 9);
            this.lblTicketIDDisplay.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTicketIDDisplay.Name = "lblTicketIDDisplay";
            this.lblTicketIDDisplay.Size = new System.Drawing.Size(77, 25);
            this.lblTicketIDDisplay.TabIndex = 12;
            this.lblTicketIDDisplay.Text = "Priority:";
            // 
            // cbPriority
            // 
            this.cbPriority.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbPriority.FormattingEnabled = true;
            this.cbPriority.Location = new System.Drawing.Point(95, 66);
            this.cbPriority.Name = "cbPriority";
            this.cbPriority.Size = new System.Drawing.Size(121, 28);
            this.cbPriority.TabIndex = 13;
            // 
            // cbProject
            // 
            this.cbProject.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbProject.FormattingEnabled = true;
            this.cbProject.Location = new System.Drawing.Point(95, 100);
            this.cbProject.Name = "cbProject";
            this.cbProject.Size = new System.Drawing.Size(121, 28);
            this.cbProject.TabIndex = 14;
            // 
            // cbCategory
            // 
            this.cbCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCategory.FormattingEnabled = true;
            this.cbCategory.Location = new System.Drawing.Point(95, 134);
            this.cbCategory.Name = "cbCategory";
            this.cbCategory.Size = new System.Drawing.Size(121, 28);
            this.cbCategory.TabIndex = 15;
            // 
            // ShowTicket
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(454, 208);
            this.Controls.Add(this.cbCategory);
            this.Controls.Add(this.cbProject);
            this.Controls.Add(this.cbPriority);
            this.Controls.Add(this.lblTicketIDDisplay);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.txtSubject);
            this.Controls.Add(this.rtbIssue);
            this.Controls.Add(this.lblIssue);
            this.Controls.Add(this.lblSubject);
            this.Controls.Add(this.lblProject);
            this.Controls.Add(this.lblCategory);
            this.Controls.Add(this.lblTicketID);
            this.Controls.Add(this.lblPriority);
            this.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "ShowTicket";
            this.Text = "Create Ticket";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblPriority;
        private System.Windows.Forms.Label lblTicketID;
        private System.Windows.Forms.Label lblCategory;
        private System.Windows.Forms.Label lblProject;
        private System.Windows.Forms.Label lblSubject;
        private System.Windows.Forms.Label lblIssue;
        private System.Windows.Forms.RichTextBox rtbIssue;
        private System.Windows.Forms.TextBox txtSubject;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label lblTicketIDDisplay;
        private System.Windows.Forms.ComboBox cbPriority;
        private System.Windows.Forms.ComboBox cbProject;
        private System.Windows.Forms.ComboBox cbCategory;
    }
}