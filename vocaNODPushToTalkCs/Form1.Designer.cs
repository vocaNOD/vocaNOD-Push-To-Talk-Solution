namespace vocaNODPushToTalkCs
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
            this.assignKeyButton = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.menuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.assignKeyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpAndAboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stateLabel = new System.Windows.Forms.Label();
            this.keyAssignedLabel = new System.Windows.Forms.Label();
            this.DotConnection = new System.Windows.Forms.PictureBox();
            this.pttLogo = new System.Windows.Forms.PictureBox();
            this.vocaNODLogo = new System.Windows.Forms.PictureBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DotConnection)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pttLogo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.vocaNODLogo)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // assignKeyButton
            // 
            this.assignKeyButton.Font = new System.Drawing.Font("Archivo Narrow", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.assignKeyButton.Location = new System.Drawing.Point(5, 122);
            this.assignKeyButton.Name = "assignKeyButton";
            this.assignKeyButton.Size = new System.Drawing.Size(210, 27);
            this.assignKeyButton.TabIndex = 0;
            this.assignKeyButton.Text = "Assign key";
            this.assignKeyButton.UseVisualStyleBackColor = true;
            this.assignKeyButton.Click += new System.EventHandler(this.button1_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.BackColor = System.Drawing.SystemColors.Control;
            this.menuStrip1.Size = new System.Drawing.Size(221, 24);
            this.menuStrip1.TabIndex = 3;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // menuToolStripMenuItem
            // 
            this.menuToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.assignKeyToolStripMenuItem,
            this.helpAndAboutToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.menuToolStripMenuItem.Name = "menuToolStripMenuItem";
            this.menuToolStripMenuItem.Size = new System.Drawing.Size(50, 20);
            this.menuToolStripMenuItem.Text = "Menu";
            // 
            // assignKeyToolStripMenuItem
            // 
            this.assignKeyToolStripMenuItem.Name = "assignKeyToolStripMenuItem";
            this.assignKeyToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.assignKeyToolStripMenuItem.Text = "Assign key";
            this.assignKeyToolStripMenuItem.Click += new System.EventHandler(this.assignKeyToolStripMenuItem_Click);
            // 
            // helpAndAboutToolStripMenuItem
            // 
            this.helpAndAboutToolStripMenuItem.Name = "helpAndAboutToolStripMenuItem";
            this.helpAndAboutToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.helpAndAboutToolStripMenuItem.Text = "Help and About";
            this.helpAndAboutToolStripMenuItem.Click += new System.EventHandler(this.helpAndAboutToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // stateLabel
            // 
            this.stateLabel.Location = new System.Drawing.Point(2, 172);
            this.stateLabel.Name = "stateLabel";
            this.stateLabel.Size = new System.Drawing.Size(39, 20);
            this.stateLabel.TabIndex = 5;
            this.stateLabel.Text = "State :";
            // 
            // keyAssignedLabel
            // 
            this.keyAssignedLabel.Location = new System.Drawing.Point(68, 172);
            this.keyAssignedLabel.Name = "keyAssignedLabel";
            this.keyAssignedLabel.Size = new System.Drawing.Size(153, 33);
            this.keyAssignedLabel.TabIndex = 6;
            this.keyAssignedLabel.Text = "Key assigned :   ";
            // 
            // DotConnection
            // 
            this.DotConnection.Image = global::vocaNODPushToTalkCs.Properties.Resources.red;
            this.DotConnection.Location = new System.Drawing.Point(38, 171);
            this.DotConnection.Name = "DotConnection";
            this.DotConnection.Size = new System.Drawing.Size(24, 16);
            this.DotConnection.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.DotConnection.TabIndex = 7;
            this.DotConnection.TabStop = false;
            // 
            // pttLogo
            // 
            this.pttLogo.Image = global::vocaNODPushToTalkCs.Properties.Resources.ptt;
            this.pttLogo.Location = new System.Drawing.Point(65, 74);
            this.pttLogo.Name = "pttLogo";
            this.pttLogo.Size = new System.Drawing.Size(94, 24);
            this.pttLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pttLogo.TabIndex = 2;
            this.pttLogo.TabStop = false;
            // 
            // vocaNODLogo
            // 
            this.vocaNODLogo.Image = global::vocaNODPushToTalkCs.Properties.Resources.vocaNOD;
            this.vocaNODLogo.Location = new System.Drawing.Point(21, 27);
            this.vocaNODLogo.Name = "vocaNODLogo";
            this.vocaNODLogo.Size = new System.Drawing.Size(179, 32);
            this.vocaNODLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.vocaNODLogo.TabIndex = 1;
            this.vocaNODLogo.TabStop = false;
            // 
            // statusStrip1
            // 
            this.statusStrip1.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 205);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(221, 22);
            this.statusStrip1.TabIndex = 8;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(0, 17);
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.notifyIcon1.BalloonTipText = "Click to re-open it";
            this.notifyIcon1.BalloonTipTitle = "vocaNOD Push To Talk running in notification tray";
            this.notifyIcon1.Icon = global::vocaNODPushToTalkCs.Properties.Resources.favicon1;
            this.notifyIcon1.Text = "notifyIcon1";
            this.notifyIcon1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(221, 227);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.DotConnection);
            this.Controls.Add(this.keyAssignedLabel);
            this.Controls.Add(this.stateLabel);
            this.Controls.Add(this.pttLogo);
            this.Controls.Add(this.vocaNODLogo);
            this.Controls.Add(this.assignKeyButton);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = global::vocaNODPushToTalkCs.Properties.Resources.favicon1;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "vocaNOD Push To Talk";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DotConnection)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pttLogo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.vocaNODLogo)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Button assignKeyButton;
        private System.Windows.Forms.PictureBox vocaNODLogo;
        private System.Windows.Forms.PictureBox pttLogo;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem menuToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem assignKeyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpAndAboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.Label stateLabel;
        private System.Windows.Forms.Label keyAssignedLabel;
        private System.Windows.Forms.PictureBox DotConnection;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
    }
}

