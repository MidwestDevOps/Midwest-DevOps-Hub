namespace MidwestDevOps_Hub.Forms.Utilities
{
    partial class DecryptCrypt
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DecryptCrypt));
            this.label1 = new System.Windows.Forms.Label();
            this.btnDecrypt = new System.Windows.Forms.Button();
            this.rtbInput = new System.Windows.Forms.RichTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.rtbOutput = new System.Windows.Forms.RichTextBox();
            this.btnCrypt = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 243);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Output:";
            // 
            // btnDecrypt
            // 
            this.btnDecrypt.Location = new System.Drawing.Point(419, 237);
            this.btnDecrypt.Name = "btnDecrypt";
            this.btnDecrypt.Size = new System.Drawing.Size(75, 32);
            this.btnDecrypt.TabIndex = 1;
            this.btnDecrypt.Text = "Decrypt";
            this.btnDecrypt.UseVisualStyleBackColor = true;
            this.btnDecrypt.Click += new System.EventHandler(this.btnDecrypt_Click);
            // 
            // rtbInput
            // 
            this.rtbInput.Location = new System.Drawing.Point(16, 82);
            this.rtbInput.Name = "rtbInput";
            this.rtbInput.Size = new System.Drawing.Size(478, 149);
            this.rtbInput.TabIndex = 2;
            this.rtbInput.Text = "";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(156, 25);
            this.label2.TabIndex = 3;
            this.label2.Text = "Decrypt / Crypt:";
            // 
            // rtbOutput
            // 
            this.rtbOutput.Location = new System.Drawing.Point(16, 275);
            this.rtbOutput.Name = "rtbOutput";
            this.rtbOutput.Size = new System.Drawing.Size(478, 149);
            this.rtbOutput.TabIndex = 4;
            this.rtbOutput.Text = "";
            // 
            // btnCrypt
            // 
            this.btnCrypt.Location = new System.Drawing.Point(338, 237);
            this.btnCrypt.Name = "btnCrypt";
            this.btnCrypt.Size = new System.Drawing.Size(75, 32);
            this.btnCrypt.TabIndex = 5;
            this.btnCrypt.Text = "Crypt";
            this.btnCrypt.UseVisualStyleBackColor = true;
            this.btnCrypt.Click += new System.EventHandler(this.btnCrypt_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 48);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(46, 20);
            this.label3.TabIndex = 6;
            this.label3.Text = "Input:";
            // 
            // DecryptCrypt
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(515, 441);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnCrypt);
            this.Controls.Add(this.rtbOutput);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.rtbInput);
            this.Controls.Add(this.btnDecrypt);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "DecryptCrypt";
            this.Text = "Decrypt / Crypt";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnDecrypt;
        private System.Windows.Forms.RichTextBox rtbInput;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RichTextBox rtbOutput;
        private System.Windows.Forms.Button btnCrypt;
        private System.Windows.Forms.Label label3;
    }
}