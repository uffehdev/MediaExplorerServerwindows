namespace MediaExplorerServer
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
            this.lstMediaFiles = new System.Windows.Forms.ListView();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // lstMediaFiles
            // 
            this.lstMediaFiles.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.lstMediaFiles.FullRowSelect = true;
            this.lstMediaFiles.Location = new System.Drawing.Point(12, 12);
            this.lstMediaFiles.Name = "lstMediaFiles";
            this.lstMediaFiles.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lstMediaFiles.Size = new System.Drawing.Size(683, 405);
            this.lstMediaFiles.TabIndex = 0;
            this.lstMediaFiles.UseCompatibleStateImageBehavior = false;
            // 
            // btnUpdate
            // 
            this.btnUpdate.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnUpdate.Location = new System.Drawing.Point(13, 424);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(91, 44);
            this.btnUpdate.TabIndex = 1;
            this.btnUpdate.Text = "Update";
            this.btnUpdate.UseCompatibleTextRendering = true;
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(731, 551);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.lstMediaFiles);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView lstMediaFiles;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.ColumnHeader columnHeader1;
    }
}

