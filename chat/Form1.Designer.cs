namespace chat
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.colorsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.backgroundColorsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.buttonColorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.textColorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.secondaryTextColorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.notificationsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.noneToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rumbleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.blinkToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hideShowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.enablePopupsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fileSettingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.setFilePathToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.displayTimeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ghostModeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mountToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.enableQuickImageViewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.runOnStartupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.displayNameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sketchToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.maskedTextBox1 = new System.Windows.Forms.MaskedTextBox();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyToolStripMenuItem,
            this.optionsToolStripMenuItem,
            this.displayNameToolStripMenuItem,
            this.helpToolStripMenuItem,
            this.sketchToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            resources.ApplyResources(this.contextMenuStrip1, "contextMenuStrip1");
            this.contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip1_Opening_1);
            // 
            // copyToolStripMenuItem
            // 
            this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            resources.ApplyResources(this.copyToolStripMenuItem, "copyToolStripMenuItem");
            this.copyToolStripMenuItem.Click += new System.EventHandler(this.copyToolStripMenuItem_Click);
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.colorsToolStripMenuItem,
            this.notificationsToolStripMenuItem,
            this.fileSettingsToolStripMenuItem,
            this.displayTimeToolStripMenuItem,
            this.ghostModeToolStripMenuItem,
            this.mountToolStripMenuItem,
            this.enableQuickImageViewToolStripMenuItem,
            this.runOnStartupToolStripMenuItem});
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            resources.ApplyResources(this.optionsToolStripMenuItem, "optionsToolStripMenuItem");
            this.optionsToolStripMenuItem.Click += new System.EventHandler(this.optionsToolStripMenuItem_Click);
            // 
            // colorsToolStripMenuItem
            // 
            this.colorsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.backgroundColorsToolStripMenuItem,
            this.buttonColorToolStripMenuItem,
            this.textColorToolStripMenuItem,
            this.secondaryTextColorToolStripMenuItem});
            this.colorsToolStripMenuItem.Name = "colorsToolStripMenuItem";
            resources.ApplyResources(this.colorsToolStripMenuItem, "colorsToolStripMenuItem");
            // 
            // backgroundColorsToolStripMenuItem
            // 
            this.backgroundColorsToolStripMenuItem.Name = "backgroundColorsToolStripMenuItem";
            resources.ApplyResources(this.backgroundColorsToolStripMenuItem, "backgroundColorsToolStripMenuItem");
            this.backgroundColorsToolStripMenuItem.Click += new System.EventHandler(this.backgroundColorsToolStripMenuItem_Click);
            // 
            // buttonColorToolStripMenuItem
            // 
            this.buttonColorToolStripMenuItem.Name = "buttonColorToolStripMenuItem";
            resources.ApplyResources(this.buttonColorToolStripMenuItem, "buttonColorToolStripMenuItem");
            this.buttonColorToolStripMenuItem.Click += new System.EventHandler(this.buttonColorToolStripMenuItem_Click);
            // 
            // textColorToolStripMenuItem
            // 
            this.textColorToolStripMenuItem.Name = "textColorToolStripMenuItem";
            resources.ApplyResources(this.textColorToolStripMenuItem, "textColorToolStripMenuItem");
            this.textColorToolStripMenuItem.Click += new System.EventHandler(this.textColorToolStripMenuItem_Click);
            // 
            // secondaryTextColorToolStripMenuItem
            // 
            this.secondaryTextColorToolStripMenuItem.Name = "secondaryTextColorToolStripMenuItem";
            resources.ApplyResources(this.secondaryTextColorToolStripMenuItem, "secondaryTextColorToolStripMenuItem");
            this.secondaryTextColorToolStripMenuItem.Click += new System.EventHandler(this.secondaryTextColorToolStripMenuItem_Click);
            // 
            // notificationsToolStripMenuItem
            // 
            this.notificationsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.noneToolStripMenuItem,
            this.rumbleToolStripMenuItem,
            this.blinkToolStripMenuItem,
            this.hideShowToolStripMenuItem,
            this.toolStripMenuItem2,
            this.enablePopupsToolStripMenuItem});
            this.notificationsToolStripMenuItem.Name = "notificationsToolStripMenuItem";
            resources.ApplyResources(this.notificationsToolStripMenuItem, "notificationsToolStripMenuItem");
            this.notificationsToolStripMenuItem.Click += new System.EventHandler(this.notificationsToolStripMenuItem_Click);
            // 
            // noneToolStripMenuItem
            // 
            this.noneToolStripMenuItem.CheckOnClick = true;
            this.noneToolStripMenuItem.Name = "noneToolStripMenuItem";
            resources.ApplyResources(this.noneToolStripMenuItem, "noneToolStripMenuItem");
            this.noneToolStripMenuItem.Click += new System.EventHandler(this.noneToolStripMenuItem_Click);
            // 
            // rumbleToolStripMenuItem
            // 
            this.rumbleToolStripMenuItem.CheckOnClick = true;
            this.rumbleToolStripMenuItem.Name = "rumbleToolStripMenuItem";
            resources.ApplyResources(this.rumbleToolStripMenuItem, "rumbleToolStripMenuItem");
            this.rumbleToolStripMenuItem.Click += new System.EventHandler(this.rumbleToolStripMenuItem_Click);
            // 
            // blinkToolStripMenuItem
            // 
            this.blinkToolStripMenuItem.Checked = true;
            this.blinkToolStripMenuItem.CheckOnClick = true;
            this.blinkToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.blinkToolStripMenuItem.Name = "blinkToolStripMenuItem";
            resources.ApplyResources(this.blinkToolStripMenuItem, "blinkToolStripMenuItem");
            this.blinkToolStripMenuItem.Click += new System.EventHandler(this.blinkToolStripMenuItem_Click);
            // 
            // hideShowToolStripMenuItem
            // 
            this.hideShowToolStripMenuItem.CheckOnClick = true;
            this.hideShowToolStripMenuItem.Name = "hideShowToolStripMenuItem";
            resources.ApplyResources(this.hideShowToolStripMenuItem, "hideShowToolStripMenuItem");
            this.hideShowToolStripMenuItem.Click += new System.EventHandler(this.hideShowToolStripMenuItem_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            resources.ApplyResources(this.toolStripMenuItem2, "toolStripMenuItem2");
            // 
            // enablePopupsToolStripMenuItem
            // 
            this.enablePopupsToolStripMenuItem.Checked = true;
            this.enablePopupsToolStripMenuItem.CheckOnClick = true;
            this.enablePopupsToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.enablePopupsToolStripMenuItem.Name = "enablePopupsToolStripMenuItem";
            resources.ApplyResources(this.enablePopupsToolStripMenuItem, "enablePopupsToolStripMenuItem");
            // 
            // fileSettingsToolStripMenuItem
            // 
            this.fileSettingsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.setFilePathToolStripMenuItem});
            this.fileSettingsToolStripMenuItem.Name = "fileSettingsToolStripMenuItem";
            resources.ApplyResources(this.fileSettingsToolStripMenuItem, "fileSettingsToolStripMenuItem");
            // 
            // setFilePathToolStripMenuItem
            // 
            this.setFilePathToolStripMenuItem.Name = "setFilePathToolStripMenuItem";
            resources.ApplyResources(this.setFilePathToolStripMenuItem, "setFilePathToolStripMenuItem");
            this.setFilePathToolStripMenuItem.Click += new System.EventHandler(this.setFilePathToolStripMenuItem_Click);
            // 
            // displayTimeToolStripMenuItem
            // 
            this.displayTimeToolStripMenuItem.Checked = true;
            this.displayTimeToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.displayTimeToolStripMenuItem.Name = "displayTimeToolStripMenuItem";
            resources.ApplyResources(this.displayTimeToolStripMenuItem, "displayTimeToolStripMenuItem");
            this.displayTimeToolStripMenuItem.Click += new System.EventHandler(this.displayTimeToolStripMenuItem_Click);
            // 
            // ghostModeToolStripMenuItem
            // 
            this.ghostModeToolStripMenuItem.CheckOnClick = true;
            this.ghostModeToolStripMenuItem.Name = "ghostModeToolStripMenuItem";
            resources.ApplyResources(this.ghostModeToolStripMenuItem, "ghostModeToolStripMenuItem");
            this.ghostModeToolStripMenuItem.Click += new System.EventHandler(this.ghostModeToolStripMenuItem_Click);
            // 
            // mountToolStripMenuItem
            // 
            this.mountToolStripMenuItem.Name = "mountToolStripMenuItem";
            resources.ApplyResources(this.mountToolStripMenuItem, "mountToolStripMenuItem");
            this.mountToolStripMenuItem.Click += new System.EventHandler(this.mountToolStripMenuItem_Click);
            // 
            // enableQuickImageViewToolStripMenuItem
            // 
            this.enableQuickImageViewToolStripMenuItem.Checked = true;
            this.enableQuickImageViewToolStripMenuItem.CheckOnClick = true;
            this.enableQuickImageViewToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.enableQuickImageViewToolStripMenuItem.Name = "enableQuickImageViewToolStripMenuItem";
            resources.ApplyResources(this.enableQuickImageViewToolStripMenuItem, "enableQuickImageViewToolStripMenuItem");
            this.enableQuickImageViewToolStripMenuItem.Click += new System.EventHandler(this.enableQuickImageViewToolStripMenuItem_Click);
            // 
            // runOnStartupToolStripMenuItem
            // 
            resources.ApplyResources(this.runOnStartupToolStripMenuItem, "runOnStartupToolStripMenuItem");
            this.runOnStartupToolStripMenuItem.Name = "runOnStartupToolStripMenuItem";
            this.runOnStartupToolStripMenuItem.Click += new System.EventHandler(this.runOnStartupToolStripMenuItem_Click);
            // 
            // displayNameToolStripMenuItem
            // 
            this.displayNameToolStripMenuItem.Name = "displayNameToolStripMenuItem";
            resources.ApplyResources(this.displayNameToolStripMenuItem, "displayNameToolStripMenuItem");
            this.displayNameToolStripMenuItem.Click += new System.EventHandler(this.displayNameToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            resources.ApplyResources(this.helpToolStripMenuItem, "helpToolStripMenuItem");
            this.helpToolStripMenuItem.Click += new System.EventHandler(this.helpToolStripMenuItem_Click);
            // 
            // sketchToolStripMenuItem
            // 
            this.sketchToolStripMenuItem.Name = "sketchToolStripMenuItem";
            resources.ApplyResources(this.sketchToolStripMenuItem, "sketchToolStripMenuItem");
            this.sketchToolStripMenuItem.Click += new System.EventHandler(this.sketchToolStripMenuItem_Click);
            // 
            // textBox1
            // 
            resources.ApplyResources(this.textBox1, "textBox1");
            this.textBox1.Name = "textBox1";
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            this.textBox1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox1_KeyDown);
            // 
            // button1
            // 
            this.button1.AllowDrop = true;
            resources.ApplyResources(this.button1, "button1");
            this.button1.Name = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            this.button1.DragDrop += new System.Windows.Forms.DragEventHandler(this.Form1_DragDrop);
            this.button1.DragEnter += new System.Windows.Forms.DragEventHandler(this.Form1_DragEnter);
            // 
            // notifyIcon1
            // 
            resources.ApplyResources(this.notifyIcon1, "notifyIcon1");
            this.notifyIcon1.BalloonTipClicked += new System.EventHandler(this.notifyIcon1_BalloonTipClicked);
            this.notifyIcon1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_MouseDoubleClick);
            // 
            // richTextBox1
            // 
            resources.ApplyResources(this.richTextBox1, "richTextBox1");
            this.richTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox1.HideSelection = false;
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ReadOnly = true;
            this.richTextBox1.TabStop = false;
            this.richTextBox1.LinkClicked += new System.Windows.Forms.LinkClickedEventHandler(this.richTextBox1_LinkClicked);
            this.richTextBox1.Click += new System.EventHandler(this.richTextBox1_Click);
            this.richTextBox1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.richTextBox1_MouseClick);
            this.richTextBox1.TextChanged += new System.EventHandler(this.richTextBox1_TextChanged);
            this.richTextBox1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.richTextBox1_KeyDown);
            this.richTextBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.richTextBox1_MouseDown);
            // 
            // maskedTextBox1
            // 
            this.maskedTextBox1.AllowDrop = true;
            resources.ApplyResources(this.maskedTextBox1, "maskedTextBox1");
            this.maskedTextBox1.Name = "maskedTextBox1";
            this.maskedTextBox1.PasswordChar = '•';
            this.maskedTextBox1.DragDrop += new System.Windows.Forms.DragEventHandler(this.Form1_DragDrop);
            this.maskedTextBox1.DragEnter += new System.Windows.Forms.DragEventHandler(this.Form1_DragEnter);
            this.maskedTextBox1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.maskedTextBox1_KeyDown);
            // 
            // folderBrowserDialog1
            // 
            resources.ApplyResources(this.folderBrowserDialog1, "folderBrowserDialog1");
            // 
            // Form1
            // 
            this.AcceptButton = this.button1;
            this.AllowDrop = true;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.MidnightBlue;
            this.ContextMenuStrip = this.contextMenuStrip1;
            this.Controls.Add(this.maskedTextBox1);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textBox1);
            this.KeyPreview = true;
            this.Name = "Form1";
            this.Activated += new System.EventHandler(this.Form1_Activated);
            this.Deactivate += new System.EventHandler(this.Form1_Deactivate);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResizeEnd += new System.EventHandler(this.Form1_ResizeEnd);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.Form1_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.Form1_DragEnter);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Form1_KeyPress);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.ToolStripMenuItem notificationsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem displayNameToolStripMenuItem;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.MaskedTextBox maskedTextBox1;
        private System.Windows.Forms.ToolStripMenuItem displayTimeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ghostModeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mountToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem noneToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem rumbleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem blinkToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem hideShowToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem enablePopupsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fileSettingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem setFilePathToolStripMenuItem;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.ToolStripMenuItem colorsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem backgroundColorsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem buttonColorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem textColorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem secondaryTextColorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem enableQuickImageViewToolStripMenuItem;
        private System.Windows.Forms.BindingSource bindingSource1;
        private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sketchToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem runOnStartupToolStripMenuItem;
    }
}

