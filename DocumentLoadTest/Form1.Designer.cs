namespace DocumentLoadTest
{
    partial class Form1
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
            this.lblWorking = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cboPages = new System.Windows.Forms.ComboBox();
            this.rbBW = new System.Windows.Forms.RadioButton();
            this.rbColor = new System.Windows.Forms.RadioButton();
            this.btnScan = new System.Windows.Forms.Button();
            this.txtOBUsername = new System.Windows.Forms.TextBox();
            this.txtOBPassword = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblCopies = new System.Windows.Forms.Label();
            this.txtCopies = new System.Windows.Forms.TextBox();
            this.gbxCommunication = new System.Windows.Forms.GroupBox();
            this.rbREST = new System.Windows.Forms.RadioButton();
            this.rbWCF = new System.Windows.Forms.RadioButton();
            this.pnlPending = new System.Windows.Forms.Panel();
            this.gbxCommunication.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblWorking
            // 
            this.lblWorking.AutoSize = true;
            this.lblWorking.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWorking.Location = new System.Drawing.Point(203, 260);
            this.lblWorking.Name = "lblWorking";
            this.lblWorking.Size = new System.Drawing.Size(74, 20);
            this.lblWorking.TabIndex = 11;
            this.lblWorking.Text = "Working";
            this.lblWorking.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(49, 69);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Pages";
            // 
            // cboPages
            // 
            this.cboPages.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboPages.FormattingEnabled = true;
            this.cboPages.Items.AddRange(new object[] {
            "A",
            "B",
            "C",
            "D",
            "E"});
            this.cboPages.Location = new System.Drawing.Point(92, 66);
            this.cboPages.Name = "cboPages";
            this.cboPages.Size = new System.Drawing.Size(121, 21);
            this.cboPages.TabIndex = 5;
            this.cboPages.SelectedIndexChanged += new System.EventHandler(this.cboPages_SelectedIndexChanged);
            // 
            // rbBW
            // 
            this.rbBW.AutoSize = true;
            this.rbBW.Checked = true;
            this.rbBW.Location = new System.Drawing.Point(297, 64);
            this.rbBW.Name = "rbBW";
            this.rbBW.Size = new System.Drawing.Size(104, 17);
            this.rbBW.TabIndex = 6;
            this.rbBW.TabStop = true;
            this.rbBW.Text = "Black and White";
            this.rbBW.UseVisualStyleBackColor = true;
            // 
            // rbColor
            // 
            this.rbColor.AutoSize = true;
            this.rbColor.Location = new System.Drawing.Point(297, 87);
            this.rbColor.Name = "rbColor";
            this.rbColor.Size = new System.Drawing.Size(49, 17);
            this.rbColor.TabIndex = 7;
            this.rbColor.Text = "Color";
            this.rbColor.UseVisualStyleBackColor = true;
            // 
            // btnScan
            // 
            this.btnScan.Enabled = false;
            this.btnScan.Location = new System.Drawing.Point(202, 153);
            this.btnScan.Name = "btnScan";
            this.btnScan.Size = new System.Drawing.Size(75, 23);
            this.btnScan.TabIndex = 10;
            this.btnScan.Text = "Scan";
            this.btnScan.UseVisualStyleBackColor = true;
            this.btnScan.Click += new System.EventHandler(this.btnScan_Click);
            // 
            // txtOBUsername
            // 
            this.txtOBUsername.Location = new System.Drawing.Point(113, 12);
            this.txtOBUsername.Name = "txtOBUsername";
            this.txtOBUsername.Size = new System.Drawing.Size(100, 20);
            this.txtOBUsername.TabIndex = 1;
            this.txtOBUsername.TextChanged += new System.EventHandler(this.txtOBUsername_TextChanged);
            // 
            // txtOBPassword
            // 
            this.txtOBPassword.Location = new System.Drawing.Point(331, 12);
            this.txtOBPassword.Name = "txtOBPassword";
            this.txtOBPassword.PasswordChar = '*';
            this.txtOBPassword.Size = new System.Drawing.Size(100, 20);
            this.txtOBPassword.TabIndex = 3;
            this.txtOBPassword.TextChanged += new System.EventHandler(this.txtOBPassword_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(34, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "OB Username";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(254, 15);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "OB Password";
            // 
            // lblCopies
            // 
            this.lblCopies.AutoSize = true;
            this.lblCopies.Location = new System.Drawing.Point(47, 109);
            this.lblCopies.Name = "lblCopies";
            this.lblCopies.Size = new System.Drawing.Size(39, 13);
            this.lblCopies.TabIndex = 8;
            this.lblCopies.Text = "Copies";
            // 
            // txtCopies
            // 
            this.txtCopies.Location = new System.Drawing.Point(92, 106);
            this.txtCopies.Name = "txtCopies";
            this.txtCopies.Size = new System.Drawing.Size(100, 20);
            this.txtCopies.TabIndex = 9;
            this.txtCopies.Text = "1";
            this.txtCopies.Enter += new System.EventHandler(this.txtCopies_Enter);
            // 
            // gbxCommunication
            // 
            this.gbxCommunication.Controls.Add(this.rbREST);
            this.gbxCommunication.Controls.Add(this.rbWCF);
            this.gbxCommunication.Location = new System.Drawing.Point(316, 133);
            this.gbxCommunication.Name = "gbxCommunication";
            this.gbxCommunication.Size = new System.Drawing.Size(166, 77);
            this.gbxCommunication.TabIndex = 12;
            this.gbxCommunication.TabStop = false;
            this.gbxCommunication.Text = "Communication";
            // 
            // rbREST
            // 
            this.rbREST.AutoSize = true;
            this.rbREST.Checked = true;
            this.rbREST.Location = new System.Drawing.Point(15, 43);
            this.rbREST.Name = "rbREST";
            this.rbREST.Size = new System.Drawing.Size(54, 17);
            this.rbREST.TabIndex = 1;
            this.rbREST.TabStop = true;
            this.rbREST.Text = "REST";
            this.rbREST.UseVisualStyleBackColor = true;
            // 
            // rbWCF
            // 
            this.rbWCF.AutoSize = true;
            this.rbWCF.Location = new System.Drawing.Point(15, 20);
            this.rbWCF.Name = "rbWCF";
            this.rbWCF.Size = new System.Drawing.Size(49, 17);
            this.rbWCF.TabIndex = 0;
            this.rbWCF.Text = "WCF";
            this.rbWCF.UseVisualStyleBackColor = true;
            // 
            // pnlPending
            // 
            this.pnlPending.BackColor = System.Drawing.Color.Chartreuse;
            this.pnlPending.Location = new System.Drawing.Point(0, 0);
            this.pnlPending.Name = "pnlPending";
            this.pnlPending.Size = new System.Drawing.Size(28, 100);
            this.pnlPending.TabIndex = 13;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(507, 318);
            this.Controls.Add(this.pnlPending);
            this.Controls.Add(this.gbxCommunication);
            this.Controls.Add(this.txtCopies);
            this.Controls.Add(this.lblCopies);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtOBPassword);
            this.Controls.Add(this.txtOBUsername);
            this.Controls.Add(this.btnScan);
            this.Controls.Add(this.rbColor);
            this.Controls.Add(this.rbBW);
            this.Controls.Add(this.cboPages);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblWorking);
            this.Name = "Form1";
            this.Text = "Compass Documents Load Testing";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.gbxCommunication.ResumeLayout(false);
            this.gbxCommunication.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblWorking;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cboPages;
        private System.Windows.Forms.RadioButton rbBW;
        private System.Windows.Forms.RadioButton rbColor;
        private System.Windows.Forms.Button btnScan;
        private System.Windows.Forms.TextBox txtOBUsername;
        private System.Windows.Forms.TextBox txtOBPassword;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblCopies;
        private System.Windows.Forms.TextBox txtCopies;
        private System.Windows.Forms.GroupBox gbxCommunication;
        private System.Windows.Forms.RadioButton rbREST;
        private System.Windows.Forms.RadioButton rbWCF;
        private System.Windows.Forms.Panel pnlPending;
    }
}

