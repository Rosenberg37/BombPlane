namespace BombPlane
{
    partial class BombPLaneForm
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
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabelLeft = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabelRight = new System.Windows.Forms.ToolStripStatusLabel();
            this.mainTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.leftTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.listView = new System.Windows.Forms.ListView();
            this.UserName = new System.Windows.Forms.ColumnHeader();
            this.IP = new System.Windows.Forms.ColumnHeader();
            this.Port = new System.Windows.Forms.ColumnHeader();
            this.listViewContext = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.连接ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.刷新ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rightSidePanel = new System.Windows.Forms.Panel();
            this.leftPanel = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.gridNetworkCounterSide = new BombPlane.GridNetwork();
            this.gridNetworkOurSide = new BombPlane.GridNetwork();
            this.MainButtun = new System.Windows.Forms.Button();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.设置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.用户名设置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.遍历端口设置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.监听端口设置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.口令设置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.连接口令提示设置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.控制ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.刷新对手列表ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.保持对手列表刷新ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.游戏ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.帮助ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.网络前缀设置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip.SuspendLayout();
            this.mainTableLayoutPanel.SuspendLayout();
            this.leftTableLayoutPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.listViewContext.SuspendLayout();
            this.rightSidePanel.SuspendLayout();
            this.leftPanel.SuspendLayout();
            this.menuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip
            // 
            this.statusStrip.BackColor = System.Drawing.Color.MediumBlue;
            this.statusStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabelLeft,
            this.toolStripStatusLabel2,
            this.toolStripStatusLabelRight});
            this.statusStrip.Location = new System.Drawing.Point(0, 658);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(1245, 26);
            this.statusStrip.TabIndex = 0;
            this.statusStrip.Text = "statusStrip1";
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
            this.mainTableLayoutPanel.Location = new System.Drawing.Point(0, 28);
            this.mainTableLayoutPanel.Name = "mainTableLayoutPanel";
            this.mainTableLayoutPanel.RowCount = 1;
            this.mainTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.mainTableLayoutPanel.Size = new System.Drawing.Size(1245, 630);
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
            this.leftTableLayoutPanel.Size = new System.Drawing.Size(350, 630);
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
            this.IP,
            this.Port});
            this.listView.ContextMenuStrip = this.listViewContext;
            this.listView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView.FullRowSelect = true;
            this.listView.Location = new System.Drawing.Point(3, 253);
            this.listView.MultiSelect = false;
            this.listView.Name = "listView";
            this.listView.Size = new System.Drawing.Size(344, 374);
            this.listView.TabIndex = 2;
            this.listView.UseCompatibleStateImageBehavior = false;
            this.listView.View = System.Windows.Forms.View.Details;
            // 
            // UserName
            // 
            this.UserName.Text = "用户名";
            this.UserName.Width = 100;
            // 
            // IP
            // 
            this.IP.Text = "IP";
            this.IP.Width = 150;
            // 
            // Port
            // 
            this.Port.Text = "端口";
            this.Port.Width = 252;
            // 
            // listViewContext
            // 
            this.listViewContext.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.listViewContext.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.连接ToolStripMenuItem,
            this.刷新ToolStripMenuItem});
            this.listViewContext.Name = "contextMenuStrip1";
            this.listViewContext.Size = new System.Drawing.Size(109, 52);
            // 
            // 连接ToolStripMenuItem
            // 
            this.连接ToolStripMenuItem.Name = "连接ToolStripMenuItem";
            this.连接ToolStripMenuItem.Size = new System.Drawing.Size(108, 24);
            this.连接ToolStripMenuItem.Text = "连接";
            this.连接ToolStripMenuItem.Click += new System.EventHandler(this.ConnectToolStripMenuItemClick);
            // 
            // 刷新ToolStripMenuItem
            // 
            this.刷新ToolStripMenuItem.Name = "刷新ToolStripMenuItem";
            this.刷新ToolStripMenuItem.Size = new System.Drawing.Size(108, 24);
            this.刷新ToolStripMenuItem.Text = "刷新";
            this.刷新ToolStripMenuItem.Click += new System.EventHandler(this.FlushToolStripMenuItemClick);
            // 
            // rightSidePanel
            // 
            this.rightSidePanel.Controls.Add(this.leftPanel);
            this.rightSidePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rightSidePanel.Location = new System.Drawing.Point(350, 0);
            this.rightSidePanel.Margin = new System.Windows.Forms.Padding(0);
            this.rightSidePanel.Name = "rightSidePanel";
            this.rightSidePanel.Size = new System.Drawing.Size(895, 630);
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
            this.leftPanel.Size = new System.Drawing.Size(895, 630);
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
            this.MainButtun.Location = new System.Drawing.Point(350, 518);
            this.MainButtun.Name = "MainButtun";
            this.MainButtun.Size = new System.Drawing.Size(189, 61);
            this.MainButtun.TabIndex = 2;
            this.MainButtun.Text = "开始游戏";
            this.MainButtun.UseVisualStyleBackColor = true;
            this.MainButtun.Click += new System.EventHandler(this.MainButtunClick);
            // 
            // menuStrip
            // 
            this.menuStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.设置ToolStripMenuItem,
            this.控制ToolStripMenuItem,
            this.游戏ToolStripMenuItem,
            this.帮助ToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(1245, 28);
            this.menuStrip.TabIndex = 2;
            this.menuStrip.Text = "menuStrip1";
            // 
            // 设置ToolStripMenuItem
            // 
            this.设置ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.用户名设置ToolStripMenuItem,
            this.遍历端口设置ToolStripMenuItem,
            this.监听端口设置ToolStripMenuItem,
            this.网络前缀设置ToolStripMenuItem,
            this.口令设置ToolStripMenuItem,
            this.连接口令提示设置ToolStripMenuItem});
            this.设置ToolStripMenuItem.Name = "设置ToolStripMenuItem";
            this.设置ToolStripMenuItem.Size = new System.Drawing.Size(53, 24);
            this.设置ToolStripMenuItem.Text = "设置";
            // 
            // 用户名设置ToolStripMenuItem
            // 
            this.用户名设置ToolStripMenuItem.Name = "用户名设置ToolStripMenuItem";
            this.用户名设置ToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.用户名设置ToolStripMenuItem.Text = "用户名设置";
            this.用户名设置ToolStripMenuItem.Click += new System.EventHandler(this.UserNameSettingToolStripMenuItemClick);
            // 
            // 遍历端口设置ToolStripMenuItem
            // 
            this.遍历端口设置ToolStripMenuItem.Name = "遍历端口设置ToolStripMenuItem";
            this.遍历端口设置ToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.遍历端口设置ToolStripMenuItem.Text = "遍历端口设置";
            this.遍历端口设置ToolStripMenuItem.Click += new System.EventHandler(this.TravelPortSettingToolStripMenuItemClick);
            // 
            // 监听端口设置ToolStripMenuItem
            // 
            this.监听端口设置ToolStripMenuItem.Name = "监听端口设置ToolStripMenuItem";
            this.监听端口设置ToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.监听端口设置ToolStripMenuItem.Text = "监听端口设置";
            this.监听端口设置ToolStripMenuItem.Click += new System.EventHandler(this.ListenPortSettingToolStripMenuItemClick);
            // 
            // 口令设置ToolStripMenuItem
            // 
            this.口令设置ToolStripMenuItem.Name = "口令设置ToolStripMenuItem";
            this.口令设置ToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.口令设置ToolStripMenuItem.Text = "连接口令设置";
            this.口令设置ToolStripMenuItem.Click += new System.EventHandler(this.WatchwordSettingToolStripMenuItemClick);
            // 
            // 连接口令提示设置ToolStripMenuItem
            // 
            this.连接口令提示设置ToolStripMenuItem.Name = "连接口令提示设置ToolStripMenuItem";
            this.连接口令提示设置ToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.连接口令提示设置ToolStripMenuItem.Text = "连接口令提示设置";
            this.连接口令提示设置ToolStripMenuItem.Click += new System.EventHandler(this.WatchwordHintSettingToolStripMenuItemClick);
            // 
            // 控制ToolStripMenuItem
            // 
            this.控制ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.刷新对手列表ToolStripMenuItem,
            this.保持对手列表刷新ToolStripMenuItem});
            this.控制ToolStripMenuItem.Name = "控制ToolStripMenuItem";
            this.控制ToolStripMenuItem.Size = new System.Drawing.Size(53, 24);
            this.控制ToolStripMenuItem.Text = "控制";
            // 
            // 刷新对手列表ToolStripMenuItem
            // 
            this.刷新对手列表ToolStripMenuItem.Name = "刷新对手列表ToolStripMenuItem";
            this.刷新对手列表ToolStripMenuItem.Size = new System.Drawing.Size(212, 26);
            this.刷新对手列表ToolStripMenuItem.Text = "刷新对手列表";
            this.刷新对手列表ToolStripMenuItem.Click += new System.EventHandler(this.FlushRivalListToolStripMenuItemClick);
            // 
            // 保持对手列表刷新ToolStripMenuItem
            // 
            this.保持对手列表刷新ToolStripMenuItem.Name = "保持对手列表刷新ToolStripMenuItem";
            this.保持对手列表刷新ToolStripMenuItem.Size = new System.Drawing.Size(212, 26);
            this.保持对手列表刷新ToolStripMenuItem.Text = "保持对手列表刷新";
            this.保持对手列表刷新ToolStripMenuItem.Click += new System.EventHandler(this.KeepRivalListFlushToolStripMenuItemClick);
            // 
            // 游戏ToolStripMenuItem
            // 
            this.游戏ToolStripMenuItem.Name = "游戏ToolStripMenuItem";
            this.游戏ToolStripMenuItem.Size = new System.Drawing.Size(53, 24);
            this.游戏ToolStripMenuItem.Text = "游戏";
            // 
            // 帮助ToolStripMenuItem
            // 
            this.帮助ToolStripMenuItem.Name = "帮助ToolStripMenuItem";
            this.帮助ToolStripMenuItem.Size = new System.Drawing.Size(53, 24);
            this.帮助ToolStripMenuItem.Text = "帮助";
            // 
            // 网络前缀设置ToolStripMenuItem
            // 
            this.网络前缀设置ToolStripMenuItem.Name = "网络前缀设置ToolStripMenuItem";
            this.网络前缀设置ToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.网络前缀设置ToolStripMenuItem.Text = "网络前缀设置";
            this.网络前缀设置ToolStripMenuItem.Click += new System.EventHandler(this.NetworkPrefixSettingToolStripMenuItemClick);
            // 
            // MainForm
            // 
            this.ClientSize = new System.Drawing.Size(1245, 684);
            this.Controls.Add(this.mainTableLayoutPanel);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.menuStrip);
            this.MainMenuStrip = this.menuStrip;
            this.Name = "MainForm";
            this.Text = "炸飞机大赛";
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.mainTableLayoutPanel.ResumeLayout(false);
            this.leftTableLayoutPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.listViewContext.ResumeLayout(false);
            this.rightSidePanel.ResumeLayout(false);
            this.leftPanel.ResumeLayout(false);
            this.leftPanel.PerformLayout();
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private StatusStrip statusStrip;
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
        private MenuStrip menuStrip;
        private ToolStripMenuItem 设置ToolStripMenuItem;
        private ToolStripMenuItem 用户名设置ToolStripMenuItem;
        private ToolStripMenuItem 监听端口设置ToolStripMenuItem;
        private ToolStripMenuItem 口令设置ToolStripMenuItem;
        private ColumnHeader Port;
        private ToolStripMenuItem 控制ToolStripMenuItem;
        private ToolStripMenuItem 刷新对手列表ToolStripMenuItem;
        private ToolStripMenuItem 保持对手列表刷新ToolStripMenuItem;
        private ToolStripMenuItem 刷新ToolStripMenuItem;
        private ToolStripMenuItem 连接口令提示设置ToolStripMenuItem;
        private ToolStripMenuItem 游戏ToolStripMenuItem;
        private ToolStripMenuItem 帮助ToolStripMenuItem;
        private ToolStripMenuItem 遍历端口设置ToolStripMenuItem;
        private ToolStripMenuItem 网络前缀设置ToolStripMenuItem;
    }
}