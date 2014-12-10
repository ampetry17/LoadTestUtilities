namespace LoadTestDocumentPurger
{
    partial class DriveSpaceIndicator
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblDriveName = new System.Windows.Forms.Label();
            this.lblFreeSpace = new System.Windows.Forms.Label();
            this.pbSpace = new System.Windows.Forms.ProgressBar();
            this.SuspendLayout();
            // 
            // lblDriveName
            // 
            this.lblDriveName.AutoSize = true;
            this.lblDriveName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDriveName.Location = new System.Drawing.Point(15, 4);
            this.lblDriveName.Name = "lblDriveName";
            this.lblDriveName.Size = new System.Drawing.Size(41, 13);
            this.lblDriveName.TabIndex = 0;
            this.lblDriveName.Text = "label1";
            // 
            // lblFreeSpace
            // 
            this.lblFreeSpace.AutoSize = true;
            this.lblFreeSpace.Location = new System.Drawing.Point(18, 38);
            this.lblFreeSpace.Name = "lblFreeSpace";
            this.lblFreeSpace.Size = new System.Drawing.Size(35, 13);
            this.lblFreeSpace.TabIndex = 1;
            this.lblFreeSpace.Text = "label1";
            // 
            // pbSpace
            // 
            this.pbSpace.Location = new System.Drawing.Point(18, 20);
            this.pbSpace.Name = "pbSpace";
            this.pbSpace.Size = new System.Drawing.Size(261, 15);
            this.pbSpace.TabIndex = 2;
            // 
            // DriveSpaceIndicator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.pbSpace);
            this.Controls.Add(this.lblFreeSpace);
            this.Controls.Add(this.lblDriveName);
            this.Name = "DriveSpaceIndicator";
            this.Size = new System.Drawing.Size(298, 58);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblDriveName;
        private System.Windows.Forms.Label lblFreeSpace;
        private System.Windows.Forms.ProgressBar pbSpace;
    }
}
