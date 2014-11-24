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
            this.btnLaunchPurgers = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnPurge
            // 
            this.btnPurge.Location = new System.Drawing.Point(12, 67);
            this.btnPurge.Name = "btnPurge";
            this.btnPurge.Size = new System.Drawing.Size(180, 23);
            this.btnPurge.TabIndex = 0;
            this.btnPurge.Text = "Start purging";
            this.btnPurge.UseVisualStyleBackColor = true;
            this.btnPurge.Click += new System.EventHandler(this.btnPurge_Click);
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(12, 111);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(180, 23);
            this.btnStop.TabIndex = 1;
            this.btnStop.Text = "Stop purging";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // lblPurgeCount
            // 
            this.lblPurgeCount.AutoSize = true;
            this.lblPurgeCount.Location = new System.Drawing.Point(31, 152);
            this.lblPurgeCount.Name = "lblPurgeCount";
            this.lblPurgeCount.Size = new System.Drawing.Size(110, 13);
            this.lblPurgeCount.TabIndex = 2;
            this.lblPurgeCount.Text = "Documents Purged: 0";
            // 
            // btnLaunchPurgers
            // 
            this.btnLaunchPurgers.Location = new System.Drawing.Point(12, 25);
            this.btnLaunchPurgers.Name = "btnLaunchPurgers";
            this.btnLaunchPurgers.Size = new System.Drawing.Size(180, 23);
            this.btnLaunchPurgers.TabIndex = 3;
            this.btnLaunchPurgers.Text = "Launch Purgers";
            this.btnLaunchPurgers.UseVisualStyleBackColor = true;
            this.btnLaunchPurgers.Click += new System.EventHandler(this.btnLaunchPurgers_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(241, 214);
            this.Controls.Add(this.btnLaunchPurgers);
            this.Controls.Add(this.lblPurgeCount);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.btnPurge);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.Text = "Load Testing Document Purger";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnPurge;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Label lblPurgeCount;
        private System.Windows.Forms.Button btnLaunchPurgers;
    }
}

