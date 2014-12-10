namespace LoadTestDocumentPurger
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
            this.btnPurge = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.lblPurgeCount = new System.Windows.Forms.Label();
            this.pnlDrives = new System.Windows.Forms.FlowLayoutPanel();
            this.lblPurgeRate = new System.Windows.Forms.Label();
            this.btnUserSetup = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnPurge
            // 
            this.btnPurge.Location = new System.Drawing.Point(12, 12);
            this.btnPurge.Name = "btnPurge";
            this.btnPurge.Size = new System.Drawing.Size(217, 23);
            this.btnPurge.TabIndex = 0;
            this.btnPurge.Text = "Start purging";
            this.btnPurge.UseVisualStyleBackColor = true;
            this.btnPurge.Click += new System.EventHandler(this.btnPurge_Click);
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(12, 41);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(217, 23);
            this.btnStop.TabIndex = 1;
            this.btnStop.Text = "Stop purging";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // lblPurgeCount
            // 
            this.lblPurgeCount.AutoSize = true;
            this.lblPurgeCount.Location = new System.Drawing.Point(9, 79);
            this.lblPurgeCount.Name = "lblPurgeCount";
            this.lblPurgeCount.Size = new System.Drawing.Size(110, 13);
            this.lblPurgeCount.TabIndex = 2;
            this.lblPurgeCount.Text = "Documents Purged: 0";
            // 
            // pnlDrives
            // 
            this.pnlDrives.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlDrives.Location = new System.Drawing.Point(13, 129);
            this.pnlDrives.Name = "pnlDrives";
            this.pnlDrives.Size = new System.Drawing.Size(307, 320);
            this.pnlDrives.TabIndex = 3;
            // 
            // lblPurgeRate
            // 
            this.lblPurgeRate.AutoSize = true;
            this.lblPurgeRate.Location = new System.Drawing.Point(9, 101);
            this.lblPurgeRate.Name = "lblPurgeRate";
            this.lblPurgeRate.Size = new System.Drawing.Size(168, 13);
            this.lblPurgeRate.TabIndex = 4;
            this.lblPurgeRate.Text = "Purge Rate: 0 documents/second";
            // 
            // btnUserSetup
            // 
            this.btnUserSetup.Location = new System.Drawing.Point(248, 11);
            this.btnUserSetup.Name = "btnUserSetup";
            this.btnUserSetup.Size = new System.Drawing.Size(75, 53);
            this.btnUserSetup.TabIndex = 5;
            this.btnUserSetup.Text = "Set up users";
            this.btnUserSetup.UseVisualStyleBackColor = true;
            this.btnUserSetup.Click += new System.EventHandler(this.btnUserSetup_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(332, 461);
            this.Controls.Add(this.btnUserSetup);
            this.Controls.Add(this.lblPurgeRate);
            this.Controls.Add(this.pnlDrives);
            this.Controls.Add(this.lblPurgeCount);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.btnPurge);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.Text = "Load Testing Document Purger";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnPurge;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Label lblPurgeCount;
        private System.Windows.Forms.FlowLayoutPanel pnlDrives;
        private System.Windows.Forms.Label lblPurgeRate;
        private System.Windows.Forms.Button btnUserSetup;
    }
}

