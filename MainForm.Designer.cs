namespace BombPlane
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
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabelLeft = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabelRight = new System.Windows.Forms.ToolStripStatusLabel();
            this.mainTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.leftTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.listView = new System.Windows.Forms.ListView();
            this.UserName = new System.Windows.Forms.ColumnHeader();
            this.IP = new System.Windows.Forms.ColumnHeader();
            this.listViewContext = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.连接ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rightSidePanel = new System.Windows.Forms.Panel();
            this.leftPanel = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.gridNetworkCounterSide = new BombPlane.GridNetwork();
            this.gridNetworkOurSide = new BombPlane.GridNetwork();
            this.MainButtun = new System.Windows.Forms.Button();
            this.statusStrip1.SuspendLayout();
            this.mainTableLayoutPanel.SuspendLayout();
            this.leftTableLayoutPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.listViewContext.SuspendLayout();
            this.rightSidePanel.SuspendLayout();
            this.leftPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.BackColor = System.Drawing.Color.MediumBlue;
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabelLeft,
            this.toolStripStatusLabel2,
            this.toolStripStatusLabelRight});
            this.statusStrip1.Location = new System.Drawing.Point(0, 658);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1245, 26);
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabelLeft
            // 
            this.toolStripStatusLabelLeft.BackColor = System.Drawing.Color.MediumBlue;
            this.toolStripStatusLabelLeft.ForeColor = System.Drawing.SystemColors.Menu;
            this.toolStripStatusLabelLeft.Name = "toolStripStatusLabelLeft";
            this.toolStripStatusLabelLeft.Size = new System.Drawing.Size(117, 20);
            this.toolStripStatusLabelLeft.Text = "NotConnected";
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(1077, 20);
            this.toolStripStatusLabel2.Spring = true;
            // 
            // toolStripStatusLabelRight
            // 
            this.toolStripStatusLabelRight.ForeColor = System.Drawing.SystemColors.Menu;
            this.toolStripStatusLabelRight.Name = "toolStripStatusLabelRight";
            this.toolStripStatusLabelRight.Size = new System.Drawing.Size(36, 20);
            this.toolStripStatusLabelRight.Text = "Idle";
            // 
            // mainTableLayoutPanel
            // 
            this.mainTableLayoutPanel.ColumnCount = 2;
            this.mainTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 350F));
            this.mainTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.mainTableLayoutPanel.Controls.Add(this.leftTableLayoutPanel, 0, 0);
            this.mainTableLayoutPanel.Controls.Add(this.rightSidePanel, 1, 0);
            this.mainTableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainTableLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.mainTableLayoutPanel.Name = "mainTableLayoutPanel";
            this.mainTableLayoutPanel.RowCount = 1;
            this.mainTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.mainTableLayoutPanel.Size = new System.Drawing.Size(1245, 658);
            this.mainTableLayoutPanel.TabIndex = 1;
            // 
            // leftTableLayoutPanel
            // 
            this.leftTableLayoutPanel.ColumnCount = 1;
            this.leftTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.leftTableLayoutPanel.Controls.Add(this.pictureBox1, 0, 0);
            this.leftTableLayoutPanel.Controls.Add(this.listView, 0, 1);
            this.leftTableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.leftTableLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.leftTableLayoutPanel.Margin = new System.Windows.Forms.Padding(0);
            this.leftTableLayoutPanel.Name = "leftTableLayoutPanel";
            this.leftTableLayoutPanel.RowCount = 2;
            this.leftTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 250F));
            this.leftTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.leftTableLayoutPanel.Size = new System.Drawing.Size(350, 658);
            this.leftTableLayoutPanel.TabIndex = 0;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.SystemColors.Window;
            this.pictureBox1.BackgroundImage = global::BombPlane.Properties.Resources.logo;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Location = new System.Drawing.Point(3, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(344, 244);
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // listView
            // 
            this.listView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.UserName,
            this.IP});
            this.listView.ContextMenuStrip = this.listViewContext;
            this.listView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView.FullRowSelect = true;
            this.listView.Location = new System.Drawing.Point(3, 253);
            this.listView.Name = "listView";
            this.listView.Size = new System.Drawing.Size(344, 402);
            this.listView.TabIndex = 2;
            this.listView.UseCompatibleStateImageBehavior = false;
            this.listView.View = System.Windows.Forms.View.Details;
            // 
            // UserName
            // 
            this.UserName.Text = "用户名";
            this.UserName.Width = 57;
            // 
            // IP
            // 
            this.IP.Text = "IP";
            this.IP.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.IP.Width = 252;
            // 
            // listViewContext
            // 
            this.listViewContext.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.listViewContext.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.连接ToolStripMenuItem});
            this.listViewContext.Name = "contextMenuStrip1";
            this.listViewContext.Size = new System.Drawing.Size(109, 28);
            // 
            // 连接ToolStripMenuItem
            // 
            this.连接ToolStripMenuItem.Name = "连接ToolStripMenuItem";
            this.连接ToolStripMenuItem.Size = new System.Drawing.Size(108, 24);
            this.连接ToolStripMenuItem.Text = "连接";
            this.连接ToolStripMenuItem.Click += new System.EventHandler(this.ConnectToolStripMenuItemClick);
            // 
            // rightSidePanel
            // 
            this.rightSidePanel.Controls.Add(this.leftPanel);
            this.rightSidePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rightSidePanel.Location = new System.Drawing.Point(350, 0);
            this.rightSidePanel.Margin = new System.Windows.Forms.Padding(0);
            this.rightSidePanel.Name = "rightSidePanel";
            this.rightSidePanel.Size = new System.Drawing.Size(895, 658);
            this.rightSidePanel.TabIndex = 1;
            // 
            // leftPanel
            // 
            this.leftPanel.Controls.Add(this.label2);
            this.leftPanel.Controls.Add(this.label1);
            this.leftPanel.Controls.Add(this.gridNetworkCounterSide);
            this.leftPanel.Controls.Add(this.gridNetworkOurSide);
            this.leftPanel.Controls.Add(this.MainButtun);
            this.leftPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.leftPanel.Location = new System.Drawing.Point(0, 0);
            this.leftPanel.Margin = new System.Windows.Forms.Padding(0);
            this.leftPanel.Name = "leftPanel";
            this.leftPanel.Size = new System.Drawing.Size(895, 658);
            this.leftPanel.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(658, 428);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 20);
            this.label2.TabIndex = 6;
            this.label2.Text = "对方棋盘";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(180, 428);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 20);
            this.label1.TabIndex = 5;
            this.label1.Text = "我方棋盘";
            // 
            // gridNetworkCounterSide
            // 
            this.gridNetworkCounterSide.Location = new System.Drawing.Point(483, 12);
            this.gridNetworkCounterSide.Name = "gridNetworkCounterSide";
            this.gridNetworkCounterSide.PlaneVisiblibity = false;
            this.gridNetworkCounterSide.Size = new System.Drawing.Size(400, 400);
            this.gridNetworkCounterSide.TabIndex = 4;
            // 
            // gridNetworkOurSide
            // 
            this.gridNetworkOurSide.Location = new System.Drawing.Point(15, 12);
            this.gridNetworkOurSide.Name = "gridNetworkOurSide";
            this.gridNetworkOurSide.PlaneVisiblibity = false;
            this.gridNetworkOurSide.Size = new System.Drawing.Size(400, 400);
            this.gridNetworkOurSide.TabIndex = 3;
            // 
            // MainButtun
            // 
            this.MainButtun.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.MainButtun.Location = new System.Drawing.Point(350, 546);
            this.MainButtun.Name = "MainButtun";
            this.MainButtun.Size = new System.Drawing.Size(189, 61);
            this.MainButtun.TabIndex = 2;
            this.MainButtun.Text = "开始游戏";
            this.MainButtun.UseVisualStyleBackColor = true;
            this.MainButtun.Click += new System.EventHandler(this.MainButtunClick);
            // 
            // MainForm
            // 
            this.ClientSize = new System.Drawing.Size(1245, 684);
            this.Controls.Add(this.mainTableLayoutPanel);
            this.Controls.Add(this.statusStrip1);
            this.Name = "MainForm";
            this.Text = "炸飞机大赛";
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.mainTableLayoutPanel.ResumeLayout(false);
            this.leftTableLayoutPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.listViewContext.ResumeLayout(false);
            this.rightSidePanel.ResumeLayout(false);
            this.leftPanel.ResumeLayout(false);
            this.leftPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private StatusStrip statusStrip1;
        private ToolStripStatusLabel toolStripStatusLabelLeft;
        private TableLayoutPanel mainTableLayoutPanel;
        private TableLayoutPanel leftTableLayoutPanel;
        private PictureBox pictureBox1;
        private Panel rightSidePanel;
        private Panel leftPanel;
        private Button MainButtun;
        private GridNetwork gridNetworkOurSide;
        private GridNetwork gridNetworkCounterSide;
        private Label label2;
        private Label label1;
        private ToolStripStatusLabel toolStripStatusLabel2;
        private ToolStripStatusLabel toolStripStatusLabelRight;
        private ListView listView;
        private ColumnHeader UserName;
        private ColumnHeader IP;
        private ContextMenuStrip listViewContext;
        private ToolStripMenuItem 连接ToolStripMenuItem;
    }
}