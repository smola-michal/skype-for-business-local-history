namespace SkyForBusLH
{
    partial class MainForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this._textAll = new System.Windows.Forms.TextBox();
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.menuItemOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemOpenAllInOne = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemOpenFolder = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemProgram = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemExit = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // _textAll
            // 
            this._textAll.Dock = System.Windows.Forms.DockStyle.Fill;
            this._textAll.Location = new System.Drawing.Point(0, 24);
            this._textAll.Multiline = true;
            this._textAll.Name = "_textAll";
            this._textAll.ReadOnly = true;
            this._textAll.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this._textAll.Size = new System.Drawing.Size(444, 438);
            this._textAll.TabIndex = 0;
            // 
            // notifyIcon
            // 
            this.notifyIcon.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.notifyIcon.BalloonTipText = "Minimized";
            this.notifyIcon.BalloonTipTitle = "SfB history";
            this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
            this.notifyIcon.Text = "Skype for Business Local History";
            this.notifyIcon.Click += new System.EventHandler(this.notifyIcon_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemOpen,
            this.menuItemProgram});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(444, 24);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // menuItemOpen
            // 
            this.menuItemOpen.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemOpenAllInOne,
            this.menuItemOpenFolder});
            this.menuItemOpen.Name = "menuItemOpen";
            this.menuItemOpen.Size = new System.Drawing.Size(48, 20);
            this.menuItemOpen.Text = "Open";
            // 
            // menuItemOpenAllInOne
            // 
            this.menuItemOpenAllInOne.Name = "menuItemOpenAllInOne";
            this.menuItemOpenAllInOne.Size = new System.Drawing.Size(124, 22);
            this.menuItemOpenAllInOne.Text = "All in one";
            this.menuItemOpenAllInOne.Click += new System.EventHandler(this.menuItemOpenAll_Click);
            // 
            // menuItemOpenFolder
            // 
            this.menuItemOpenFolder.Name = "menuItemOpenFolder";
            this.menuItemOpenFolder.Size = new System.Drawing.Size(124, 22);
            this.menuItemOpenFolder.Text = "Folder";
            this.menuItemOpenFolder.Click += new System.EventHandler(this.menuItemOpenFolder_Click);
            // 
            // menuItemProgram
            // 
            this.menuItemProgram.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemExit});
            this.menuItemProgram.Name = "menuItemProgram";
            this.menuItemProgram.Size = new System.Drawing.Size(65, 20);
            this.menuItemProgram.Text = "Program";
            // 
            // menuItemExit
            // 
            this.menuItemExit.Name = "menuItemExit";
            this.menuItemExit.Size = new System.Drawing.Size(92, 22);
            this.menuItemExit.Text = "Exit";
            this.menuItemExit.Click += new System.EventHandler(this.menuItemExit_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(444, 462);
            this.Controls.Add(this._textAll);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "Skype for Business Local History";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Resize += new System.EventHandler(this.Main_Resize);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox _textAll;
        private System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem menuItemOpen;
        private System.Windows.Forms.ToolStripMenuItem menuItemProgram;
        private System.Windows.Forms.ToolStripMenuItem menuItemExit;
        private System.Windows.Forms.ToolStripMenuItem menuItemOpenAllInOne;
        private System.Windows.Forms.ToolStripMenuItem menuItemOpenFolder;
    }
}

