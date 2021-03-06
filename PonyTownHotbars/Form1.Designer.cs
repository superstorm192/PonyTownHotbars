namespace PonyTownHotbars
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
            this.fileSystemWatcher1 = new System.IO.FileSystemWatcher();
            this.filePath = new System.Windows.Forms.TextBox();
            this.folder = new System.Windows.Forms.Button();
            this.hotbarPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.emoteTooltips = new System.Windows.Forms.ToolTip(this.components);
            this.debugMode = new System.Windows.Forms.CheckBox();
            this.emoteContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.copyNewFormatToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyOldFormatToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusVersion = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.reload = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcher1)).BeginInit();
            this.emoteContextMenu.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // fileSystemWatcher1
            // 
            this.fileSystemWatcher1.EnableRaisingEvents = true;
            this.fileSystemWatcher1.SynchronizingObject = this;
            // 
            // filePath
            // 
            this.filePath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.filePath.Location = new System.Drawing.Point(12, 12);
            this.filePath.Name = "filePath";
            this.filePath.Size = new System.Drawing.Size(832, 20);
            this.filePath.TabIndex = 0;
            this.filePath.TextChanged += new System.EventHandler(this.filePath_TextChanged);
            // 
            // folder
            // 
            this.folder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.folder.Location = new System.Drawing.Point(850, 11);
            this.folder.Name = "folder";
            this.folder.Size = new System.Drawing.Size(86, 22);
            this.folder.TabIndex = 1;
            this.folder.Text = "Select Folder";
            this.folder.UseVisualStyleBackColor = true;
            this.folder.Click += new System.EventHandler(this.button1_Click);
            // 
            // hotbarPanel
            // 
            this.hotbarPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.hotbarPanel.AutoScroll = true;
            this.hotbarPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.hotbarPanel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.hotbarPanel.Location = new System.Drawing.Point(12, 39);
            this.hotbarPanel.Name = "hotbarPanel";
            this.hotbarPanel.Size = new System.Drawing.Size(1060, 412);
            this.hotbarPanel.TabIndex = 3;
            this.hotbarPanel.WrapContents = false;
            // 
            // debugMode
            // 
            this.debugMode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.debugMode.Location = new System.Drawing.Point(1010, 11);
            this.debugMode.Name = "debugMode";
            this.debugMode.Size = new System.Drawing.Size(62, 22);
            this.debugMode.TabIndex = 6;
            this.debugMode.Text = "Debug";
            this.debugMode.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.emoteTooltips.SetToolTip(this.debugMode, "\r\n");
            this.toolTip1.SetToolTip(this.debugMode, resources.GetString("debugMode.ToolTip"));
            this.debugMode.UseVisualStyleBackColor = true;
            this.debugMode.CheckedChanged += new System.EventHandler(this.debugView_CheckedChanged);
            // 
            // emoteContextMenu
            // 
            this.emoteContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyNewFormatToolStripMenuItem,
            this.copyOldFormatToolStripMenuItem});
            this.emoteContextMenu.Name = "emoteContextMenu";
            this.emoteContextMenu.Size = new System.Drawing.Size(171, 48);
            // 
            // copyNewFormatToolStripMenuItem
            // 
            this.copyNewFormatToolStripMenuItem.Name = "copyNewFormatToolStripMenuItem";
            this.copyNewFormatToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.copyNewFormatToolStripMenuItem.Text = "Copy New Format";
            // 
            // copyOldFormatToolStripMenuItem
            // 
            this.copyOldFormatToolStripMenuItem.Name = "copyOldFormatToolStripMenuItem";
            this.copyOldFormatToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.copyOldFormatToolStripMenuItem.Text = "Copy Old Format";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusVersion,
            this.statusLabel,
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 454);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.ShowItemToolTips = true;
            this.statusStrip1.Size = new System.Drawing.Size(1084, 22);
            this.statusStrip1.TabIndex = 5;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusVersion
            // 
            this.toolStripStatusVersion.IsLink = true;
            this.toolStripStatusVersion.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.toolStripStatusVersion.Name = "toolStripStatusVersion";
            this.toolStripStatusVersion.Size = new System.Drawing.Size(65, 17);
            this.toolStripStatusVersion.Text = "Version #.#";
            this.toolStripStatusVersion.ToolTipText = "https://github.com/superstorm192/PonyTownHotbars";
            this.toolStripStatusVersion.Click += new System.EventHandler(this.toolStripStatusLabel1_Click);
            // 
            // statusLabel
            // 
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(132, 17);
            this.statusLabel.Text = "Loaded ## bars in 0 ms.";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(107, 17);
            this.toolStripStatusLabel1.Text = "No Emote Selected";
            this.toolStripStatusLabel1.Visible = false;
            // 
            // reload
            // 
            this.reload.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.reload.Location = new System.Drawing.Point(942, 11);
            this.reload.Name = "reload";
            this.reload.Size = new System.Drawing.Size(62, 22);
            this.reload.TabIndex = 7;
            this.reload.Text = "Reload";
            this.reload.UseVisualStyleBackColor = true;
            this.reload.Click += new System.EventHandler(this.reload_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1084, 476);
            this.Controls.Add(this.reload);
            this.Controls.Add(this.debugMode);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.hotbarPanel);
            this.Controls.Add(this.folder);
            this.Controls.Add(this.filePath);
            this.Name = "Form1";
            this.Text = "PonyTown Hotbar Viewer";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResizeEnd += new System.EventHandler(this.Form1_ResizeEnd);
            ((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcher1)).EndInit();
            this.emoteContextMenu.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.IO.FileSystemWatcher fileSystemWatcher1;
        private System.Windows.Forms.TextBox filePath;
        private System.Windows.Forms.Button folder;
        private System.Windows.Forms.FlowLayoutPanel hotbarPanel;
        private System.Windows.Forms.ToolTip emoteTooltips;
        private System.Windows.Forms.ContextMenuStrip emoteContextMenu;
        private System.Windows.Forms.ToolStripMenuItem copyNewFormatToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyOldFormatToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel statusLabel;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusVersion;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.Button reload;
        private System.Windows.Forms.CheckBox debugMode;
    }
}

