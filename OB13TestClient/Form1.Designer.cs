namespace OB13TestClient
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
            this.btnOpen = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnStoreWithFile = new System.Windows.Forms.Button();
            this.btnStoreBytes = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnOpen
            // 
            this.btnOpen.Location = new System.Drawing.Point(32, 12);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(75, 23);
            this.btnOpen.TabIndex = 0;
            this.btnOpen.Text = "Open";
            this.btnOpen.UseVisualStyleBackColor = true;
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(32, 62);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnStoreWithFile
            // 
            this.btnStoreWithFile.Location = new System.Drawing.Point(222, 12);
            this.btnStoreWithFile.Name = "btnStoreWithFile";
            this.btnStoreWithFile.Size = new System.Drawing.Size(131, 23);
            this.btnStoreWithFile.TabIndex = 2;
            this.btnStoreWithFile.Text = "Store with file";
            this.btnStoreWithFile.UseVisualStyleBackColor = true;
            this.btnStoreWithFile.Click += new System.EventHandler(this.btnStoreWithFile_Click);
            // 
            // btnStoreBytes
            // 
            this.btnStoreBytes.Location = new System.Drawing.Point(222, 62);
            this.btnStoreBytes.Name = "btnStoreBytes";
            this.btnStoreBytes.Size = new System.Drawing.Size(131, 23);
            this.btnStoreBytes.TabIndex = 3;
            this.btnStoreBytes.Text = "Store Bytes";
            this.btnStoreBytes.UseVisualStyleBackColor = true;
            this.btnStoreBytes.Click += new System.EventHandler(this.btnStoreBytes_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(473, 332);
            this.Controls.Add(this.btnStoreBytes);
            this.Controls.Add(this.btnStoreWithFile);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnOpen);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnOpen;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnStoreWithFile;
        private System.Windows.Forms.Button btnStoreBytes;
    }
}

