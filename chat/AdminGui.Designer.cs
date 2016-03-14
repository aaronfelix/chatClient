namespace chat
{
    partial class AdminGui
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
            this.Target = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.kickButton = new System.Windows.Forms.Button();
            this.muteButton = new System.Windows.Forms.Button();
            this.sayButton = new System.Windows.Forms.Button();
            this.toggleTopmostButton = new System.Windows.Forms.Button();
            this.lockButton = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.unswearButton = new System.Windows.Forms.Button();
            this.swearButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Target
            // 
            this.Target.Location = new System.Drawing.Point(68, 8);
            this.Target.Name = "Target";
            this.Target.Size = new System.Drawing.Size(100, 20);
            this.Target.TabIndex = 0;
            this.Target.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Target";
            // 
            // kickButton
            // 
            this.kickButton.Location = new System.Drawing.Point(12, 89);
            this.kickButton.Name = "kickButton";
            this.kickButton.Size = new System.Drawing.Size(75, 23);
            this.kickButton.TabIndex = 2;
            this.kickButton.Text = "&Kick $target";
            this.kickButton.UseVisualStyleBackColor = true;
            this.kickButton.Click += new System.EventHandler(this.kickButton_Click);
            this.kickButton.KeyDown += new System.Windows.Forms.KeyEventHandler(this.AdminGui_KeyDown);
            // 
            // muteButton
            // 
            this.muteButton.Location = new System.Drawing.Point(93, 89);
            this.muteButton.Name = "muteButton";
            this.muteButton.Size = new System.Drawing.Size(75, 23);
            this.muteButton.TabIndex = 3;
            this.muteButton.Text = "&Mute $target";
            this.muteButton.UseVisualStyleBackColor = true;
            this.muteButton.Click += new System.EventHandler(this.muteButton_Click);
            this.muteButton.KeyDown += new System.Windows.Forms.KeyEventHandler(this.AdminGui_KeyDown);
            // 
            // sayButton
            // 
            this.sayButton.Location = new System.Drawing.Point(12, 118);
            this.sayButton.Name = "sayButton";
            this.sayButton.Size = new System.Drawing.Size(156, 23);
            this.sayButton.TabIndex = 4;
            this.sayButton.Text = "Make the server &say $target";
            this.sayButton.UseVisualStyleBackColor = true;
            this.sayButton.Click += new System.EventHandler(this.button1_Click);
            this.sayButton.KeyDown += new System.Windows.Forms.KeyEventHandler(this.AdminGui_KeyDown);
            // 
            // toggleTopmostButton
            // 
            this.toggleTopmostButton.Location = new System.Drawing.Point(234, 12);
            this.toggleTopmostButton.Name = "toggleTopmostButton";
            this.toggleTopmostButton.Size = new System.Drawing.Size(115, 23);
            this.toggleTopmostButton.TabIndex = 6;
            this.toggleTopmostButton.Text = "Toggle Force &Top";
            this.toggleTopmostButton.UseVisualStyleBackColor = true;
            this.toggleTopmostButton.Click += new System.EventHandler(this.toggleTopmostButton_Click);
            this.toggleTopmostButton.KeyDown += new System.Windows.Forms.KeyEventHandler(this.AdminGui_KeyDown);
            // 
            // lockButton
            // 
            this.lockButton.Location = new System.Drawing.Point(234, 41);
            this.lockButton.Name = "lockButton";
            this.lockButton.Size = new System.Drawing.Size(115, 41);
            this.lockButton.TabIndex = 8;
            this.lockButton.Text = "&Lock their computer (like win+L)";
            this.lockButton.UseVisualStyleBackColor = true;
            this.lockButton.Click += new System.EventHandler(this.lockButton_Click);
            this.lockButton.KeyDown += new System.Windows.Forms.KeyEventHandler(this.AdminGui_KeyDown);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(234, 89);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(115, 23);
            this.button1.TabIndex = 9;
            this.button1.Text = "Notepadify target";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // unswearButton
            // 
            this.unswearButton.Location = new System.Drawing.Point(12, 226);
            this.unswearButton.Name = "unswearButton";
            this.unswearButton.Size = new System.Drawing.Size(94, 26);
            this.unswearButton.TabIndex = 10;
            this.unswearButton.Text = "&Hide swear jar";
            this.unswearButton.UseVisualStyleBackColor = true;
            this.unswearButton.Click += new System.EventHandler(this.unswearButton_Click);
            // 
            // swearButton
            // 
            this.swearButton.Location = new System.Drawing.Point(112, 226);
            this.swearButton.Name = "swearButton";
            this.swearButton.Size = new System.Drawing.Size(117, 26);
            this.swearButton.TabIndex = 11;
            this.swearButton.Text = "&Show swear jar";
            this.swearButton.UseVisualStyleBackColor = true;
            this.swearButton.Click += new System.EventHandler(this.swearButton_Click);
            // 
            // AdminGui
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(357, 264);
            this.Controls.Add(this.swearButton);
            this.Controls.Add(this.unswearButton);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.lockButton);
            this.Controls.Add(this.toggleTopmostButton);
            this.Controls.Add(this.sayButton);
            this.Controls.Add(this.muteButton);
            this.Controls.Add(this.kickButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Target);
            this.Name = "AdminGui";
            this.Text = "AdminGui";
            this.Load += new System.EventHandler(this.AdminGui_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.AdminGui_KeyDown);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.AdminGui_KeyPress);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox Target;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button kickButton;
        private System.Windows.Forms.Button muteButton;
        private System.Windows.Forms.Button sayButton;
        private System.Windows.Forms.Button toggleTopmostButton;
        private System.Windows.Forms.Button lockButton;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button unswearButton;
        private System.Windows.Forms.Button swearButton;
    }
}