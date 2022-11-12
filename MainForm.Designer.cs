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
            this.specialToolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabelRight = new System.Windows.Forms.ToolStripStatusLabel();
            this.mainTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.leftTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.PictureBox = new System.Windows.Forms.PictureBox();
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
            this.gridNetworkCounterSide = new BombPlane.GridView();
            this.gridNetworkOurSide = new BombPlane.GridView();
            this.MainButton = new System.Windows.Forms.Button();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.设置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.用户名设置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.网络前缀设置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.遍历端口设置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.监听端口设置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.口令设置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.连接口令提示设置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SetDelegateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.控制ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.刷新对手列表ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.KeepFlushToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DisconnectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.游戏ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.StartGameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.FinishPrepareToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.InitializePlanesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DelegateInitializeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.BombToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AssistToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.代理轰炸ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DelegateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.帮助ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.用户名说明ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.端口及网络设置说明ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.游戏流程说明ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip.SuspendLayout();
            this.mainTableLayoutPanel.SuspendLayout();
            this.leftTableLayoutPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox)).BeginInit();
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
            this.specialToolStripStatusLabel,
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
            this.toolStripStatusLabelLeft.Size = new System.Drawing.Size(54, 20);
            this.toolStripStatusLabelLeft.Text = "未连接";
            // 
            // specialToolStripStatusLabel
            // 
            this.specialToolStripStatusLabel.Name = "specialToolStripStatusLabel";
            this.specialToolStripStatusLabel.Size = new System.Drawing.Size(1098, 20);
            this.specialToolStripStatusLabel.Spring = true;
            // 
            // toolStripStatusLabelRight
            // 
            this.toolStripStatusLabelRight.ForeColor = System.Drawing.SystemColors.Menu;
            this.toolStripStatusLabelRight.Name = "toolStripStatusLabelRight";
            this.toolStripStatusLabelRight.Size = new System.Drawing.Size(39, 20);
            this.toolStripStatusLabelRight.Text = "闲置";
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
            this.leftTableLayoutPanel.Controls.Add(this.PictureBox, 0, 0);
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
            // PictureBox
            // 
            this.PictureBox.BackColor = System.Drawing.SystemColors.Window;
            this.PictureBox.BackgroundImage = global::BombPlane.Properties.Resources.logo;
            this.PictureBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.PictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PictureBox.Location = new System.Drawing.Point(3, 3);
            this.PictureBox.Name = "PictureBox";
            this.PictureBox.Size = new System.Drawing.Size(344, 244);
            this.PictureBox.TabIndex = 1;
            this.PictureBox.TabStop = false;
            this.PictureBox.DoubleClick += new System.EventHandler(this.PictureBoxDoubleClick);
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
            this.leftPanel.Controls.Add(this.MainButton);
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
            this.gridNetworkCounterSide.IsPlaneVisible = false;
            this.gridNetworkCounterSide.Location = new System.Drawing.Point(483, 12);
            this.gridNetworkCounterSide.Name = "gridNetworkCounterSide";
            this.gridNetworkCounterSide.Planes = null;
            this.gridNetworkCounterSide.Size = new System.Drawing.Size(400, 400);
            this.gridNetworkCounterSide.TabIndex = 4;
            // 
            // gridNetworkOurSide
            // 
            this.gridNetworkOurSide.IsPlaneVisible = false;
            this.gridNetworkOurSide.Location = new System.Drawing.Point(15, 12);
            this.gridNetworkOurSide.Name = "gridNetworkOurSide";
            this.gridNetworkOurSide.Planes = null;
            this.gridNetworkOurSide.Size = new System.Drawing.Size(400, 400);
            this.gridNetworkOurSide.TabIndex = 3;
            // 
            // MainButton
            // 
            this.MainButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.MainButton.Enabled = false;
            this.MainButton.Location = new System.Drawing.Point(350, 518);
            this.MainButton.Name = "MainButton";
            this.MainButton.Size = new System.Drawing.Size(189, 61);
            this.MainButton.TabIndex = 2;
            this.MainButton.Text = "开始游戏";
            this.MainButton.UseVisualStyleBackColor = true;
            this.MainButton.Click += new System.EventHandler(this.MainButtunClick);
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
            this.网络前缀设置ToolStripMenuItem,
            this.遍历端口设置ToolStripMenuItem,
            this.监听端口设置ToolStripMenuItem,
            this.口令设置ToolStripMenuItem,
            this.连接口令提示设置ToolStripMenuItem,
            this.SetDelegateToolStripMenuItem});
            this.设置ToolStripMenuItem.Name = "设置ToolStripMenuItem";
            this.设置ToolStripMenuItem.Size = new System.Drawing.Size(53, 24);
            this.设置ToolStripMenuItem.Text = "设置";
            // 
            // 用户名设置ToolStripMenuItem
            // 
            this.用户名设置ToolStripMenuItem.Name = "用户名设置ToolStripMenuItem";
            this.用户名设置ToolStripMenuItem.Size = new System.Drawing.Size(212, 26);
            this.用户名设置ToolStripMenuItem.Text = "用户名设置";
            this.用户名设置ToolStripMenuItem.Click += new System.EventHandler(this.UserNameSettingToolStripMenuItemClick);
            // 
            // 网络前缀设置ToolStripMenuItem
            // 
            this.网络前缀设置ToolStripMenuItem.Name = "网络前缀设置ToolStripMenuItem";
            this.网络前缀设置ToolStripMenuItem.Size = new System.Drawing.Size(212, 26);
            this.网络前缀设置ToolStripMenuItem.Text = "网络前缀设置";
            this.网络前缀设置ToolStripMenuItem.Click += new System.EventHandler(this.NetworkPrefixSettingToolStripMenuItemClick);
            // 
            // 遍历端口设置ToolStripMenuItem
            // 
            this.遍历端口设置ToolStripMenuItem.Name = "遍历端口设置ToolStripMenuItem";
            this.遍历端口设置ToolStripMenuItem.Size = new System.Drawing.Size(212, 26);
            this.遍历端口设置ToolStripMenuItem.Text = "遍历端口设置";
            this.遍历端口设置ToolStripMenuItem.Click += new System.EventHandler(this.TravelPortSettingToolStripMenuItemClick);
            // 
            // 监听端口设置ToolStripMenuItem
            // 
            this.监听端口设置ToolStripMenuItem.Name = "监听端口设置ToolStripMenuItem";
            this.监听端口设置ToolStripMenuItem.Size = new System.Drawing.Size(212, 26);
            this.监听端口设置ToolStripMenuItem.Text = "监听端口设置";
            this.监听端口设置ToolStripMenuItem.Click += new System.EventHandler(this.ListenPortSettingToolStripMenuItemClick);
            // 
            // 口令设置ToolStripMenuItem
            // 
            this.口令设置ToolStripMenuItem.Name = "口令设置ToolStripMenuItem";
            this.口令设置ToolStripMenuItem.Size = new System.Drawing.Size(212, 26);
            this.口令设置ToolStripMenuItem.Text = "连接口令设置";
            this.口令设置ToolStripMenuItem.Click += new System.EventHandler(this.WatchwordSettingToolStripMenuItemClick);
            // 
            // 连接口令提示设置ToolStripMenuItem
            // 
            this.连接口令提示设置ToolStripMenuItem.Name = "连接口令提示设置ToolStripMenuItem";
            this.连接口令提示设置ToolStripMenuItem.Size = new System.Drawing.Size(212, 26);
            this.连接口令提示设置ToolStripMenuItem.Text = "连接口令提示设置";
            this.连接口令提示设置ToolStripMenuItem.Click += new System.EventHandler(this.WatchwordHintSettingToolStripMenuItemClick);
            // 
            // SetDelegateToolStripMenuItem
            // 
            this.SetDelegateToolStripMenuItem.Name = "SetDelegateToolStripMenuItem";
            this.SetDelegateToolStripMenuItem.Size = new System.Drawing.Size(212, 26);
            this.SetDelegateToolStripMenuItem.Text = "设置程序代理";
            this.SetDelegateToolStripMenuItem.Click += new System.EventHandler(this.SetDelegateToolStripMenuItemClick);
            // 
            // 控制ToolStripMenuItem
            // 
            this.控制ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.刷新对手列表ToolStripMenuItem,
            this.KeepFlushToolStripMenuItem,
            this.DisconnectToolStripMenuItem});
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
            // KeepFlushToolStripMenuItem
            // 
            this.KeepFlushToolStripMenuItem.Name = "KeepFlushToolStripMenuItem";
            this.KeepFlushToolStripMenuItem.Size = new System.Drawing.Size(212, 26);
            this.KeepFlushToolStripMenuItem.Text = "保持对手列表刷新";
            this.KeepFlushToolStripMenuItem.Click += new System.EventHandler(this.KeepRivalListFlushToolStripMenuItemClick);
            // 
            // DisconnectToolStripMenuItem
            // 
            this.DisconnectToolStripMenuItem.Enabled = false;
            this.DisconnectToolStripMenuItem.Name = "DisconnectToolStripMenuItem";
            this.DisconnectToolStripMenuItem.Size = new System.Drawing.Size(212, 26);
            this.DisconnectToolStripMenuItem.Text = "断开连接";
            this.DisconnectToolStripMenuItem.Click += new System.EventHandler(this.DisconnectToolStripMenuItemClick);
            // 
            // 游戏ToolStripMenuItem
            // 
            this.游戏ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.StartGameToolStripMenuItem,
            this.FinishPrepareToolStripMenuItem,
            this.BombToolStripMenuItem,
            this.DelegateToolStripMenuItem});
            this.游戏ToolStripMenuItem.Name = "游戏ToolStripMenuItem";
            this.游戏ToolStripMenuItem.Size = new System.Drawing.Size(53, 24);
            this.游戏ToolStripMenuItem.Text = "游戏";
            // 
            // StartGameToolStripMenuItem
            // 
            this.StartGameToolStripMenuItem.Enabled = false;
            this.StartGameToolStripMenuItem.Name = "StartGameToolStripMenuItem";
            this.StartGameToolStripMenuItem.Size = new System.Drawing.Size(152, 26);
            this.StartGameToolStripMenuItem.Text = "开始游戏";
            this.StartGameToolStripMenuItem.Click += new System.EventHandler(this.StartGameToolStripMenuItemClick);
            // 
            // FinishPrepareToolStripMenuItem
            // 
            this.FinishPrepareToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.InitializePlanesToolStripMenuItem,
            this.DelegateInitializeToolStripMenuItem});
            this.FinishPrepareToolStripMenuItem.Enabled = false;
            this.FinishPrepareToolStripMenuItem.Name = "FinishPrepareToolStripMenuItem";
            this.FinishPrepareToolStripMenuItem.Size = new System.Drawing.Size(152, 26);
            this.FinishPrepareToolStripMenuItem.Text = "完成准备";
            this.FinishPrepareToolStripMenuItem.Click += new System.EventHandler(this.FinishPrepareToolStripMenuItemClick);
            // 
            // InitializePlanesToolStripMenuItem
            // 
            this.InitializePlanesToolStripMenuItem.Enabled = false;
            this.InitializePlanesToolStripMenuItem.Name = "InitializePlanesToolStripMenuItem";
            this.InitializePlanesToolStripMenuItem.Size = new System.Drawing.Size(197, 26);
            this.InitializePlanesToolStripMenuItem.Text = "随机初始化飞机";
            this.InitializePlanesToolStripMenuItem.Click += new System.EventHandler(this.InitializePlanesToolStripMenuItemClick);
            // 
            // DelegateInitializeToolStripMenuItem
            // 
            this.DelegateInitializeToolStripMenuItem.Enabled = false;
            this.DelegateInitializeToolStripMenuItem.Name = "DelegateInitializeToolStripMenuItem";
            this.DelegateInitializeToolStripMenuItem.Size = new System.Drawing.Size(197, 26);
            this.DelegateInitializeToolStripMenuItem.Text = "代理初始化";
            this.DelegateInitializeToolStripMenuItem.Click += new System.EventHandler(this.DelegateInitializeToolStripMenuItemClick);
            // 
            // BombToolStripMenuItem
            // 
            this.BombToolStripMenuItem.BackColor = System.Drawing.SystemColors.Control;
            this.BombToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AssistToolStripMenuItem,
            this.代理轰炸ToolStripMenuItem});
            this.BombToolStripMenuItem.Enabled = false;
            this.BombToolStripMenuItem.Name = "BombToolStripMenuItem";
            this.BombToolStripMenuItem.Size = new System.Drawing.Size(152, 26);
            this.BombToolStripMenuItem.Text = "轰炸";
            this.BombToolStripMenuItem.Click += new System.EventHandler(this.BombToolStripMenuItemClick);
            // 
            // AssistToolStripMenuItem
            // 
            this.AssistToolStripMenuItem.Enabled = false;
            this.AssistToolStripMenuItem.Name = "AssistToolStripMenuItem";
            this.AssistToolStripMenuItem.Size = new System.Drawing.Size(182, 26);
            this.AssistToolStripMenuItem.Text = "敌机辅助显示";
            this.AssistToolStripMenuItem.Click += new System.EventHandler(this.AssistToolStripMenuItemClick);
            // 
            // 代理轰炸ToolStripMenuItem
            // 
            this.代理轰炸ToolStripMenuItem.Enabled = false;
            this.代理轰炸ToolStripMenuItem.Name = "代理轰炸ToolStripMenuItem";
            this.代理轰炸ToolStripMenuItem.Size = new System.Drawing.Size(182, 26);
            this.代理轰炸ToolStripMenuItem.Text = "代理轰炸";
            this.代理轰炸ToolStripMenuItem.Click += new System.EventHandler(this.DelegateBombToolStripMenuItemClick);
            // 
            // DelegateToolStripMenuItem
            // 
            this.DelegateToolStripMenuItem.DoubleClickEnabled = true;
            this.DelegateToolStripMenuItem.Enabled = false;
            this.DelegateToolStripMenuItem.Name = "DelegateToolStripMenuItem";
            this.DelegateToolStripMenuItem.Size = new System.Drawing.Size(152, 26);
            this.DelegateToolStripMenuItem.Text = "托管";
            this.DelegateToolStripMenuItem.Click += new System.EventHandler(this.DelegateHostToolStripMenuItemClick);
            // 
            // 帮助ToolStripMenuItem
            // 
            this.帮助ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.用户名说明ToolStripMenuItem,
            this.端口及网络设置说明ToolStripMenuItem,
            this.游戏流程说明ToolStripMenuItem});
            this.帮助ToolStripMenuItem.Name = "帮助ToolStripMenuItem";
            this.帮助ToolStripMenuItem.Size = new System.Drawing.Size(53, 24);
            this.帮助ToolStripMenuItem.Text = "帮助";
            // 
            // 用户名说明ToolStripMenuItem
            // 
            this.用户名说明ToolStripMenuItem.Name = "用户名说明ToolStripMenuItem";
            this.用户名说明ToolStripMenuItem.Size = new System.Drawing.Size(227, 26);
            this.用户名说明ToolStripMenuItem.Text = "用户名说明";
            this.用户名说明ToolStripMenuItem.Click += new System.EventHandler(this.UserNameHelpToolStripMenuItemClick);
            // 
            // 端口及网络设置说明ToolStripMenuItem
            // 
            this.端口及网络设置说明ToolStripMenuItem.Name = "端口及网络设置说明ToolStripMenuItem";
            this.端口及网络设置说明ToolStripMenuItem.Size = new System.Drawing.Size(227, 26);
            this.端口及网络设置说明ToolStripMenuItem.Text = "端口及网络设置说明";
            this.端口及网络设置说明ToolStripMenuItem.Click += new System.EventHandler(this.PortAndNetworkHelpToolStripMenuItemClick);
            // 
            // 游戏流程说明ToolStripMenuItem
            // 
            this.游戏流程说明ToolStripMenuItem.Name = "游戏流程说明ToolStripMenuItem";
            this.游戏流程说明ToolStripMenuItem.Size = new System.Drawing.Size(227, 26);
            this.游戏流程说明ToolStripMenuItem.Text = "游戏流程说明";
            this.游戏流程说明ToolStripMenuItem.Click += new System.EventHandler(this.GameProcedureHelpToolStripMenuItemClick);
            // 
            // BombPLaneForm
            // 
            this.ClientSize = new System.Drawing.Size(1245, 684);
            this.Controls.Add(this.mainTableLayoutPanel);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.menuStrip);
            this.MainMenuStrip = this.menuStrip;
            this.Name = "BombPLaneForm";
            this.Text = "炸飞机大赛";
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.mainTableLayoutPanel.ResumeLayout(false);
            this.leftTableLayoutPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox)).EndInit();
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
        private PictureBox PictureBox;
        private Panel rightSidePanel;
        private Panel leftPanel;
        private Button MainButton;
        private GridView gridNetworkOurSide;
        private GridView gridNetworkCounterSide;
        private Label label2;
        private Label label1;
        private ToolStripStatusLabel specialToolStripStatusLabel;
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
        private ToolStripMenuItem KeepFlushToolStripMenuItem;
        private ToolStripMenuItem 刷新ToolStripMenuItem;
        private ToolStripMenuItem 连接口令提示设置ToolStripMenuItem;
        private ToolStripMenuItem 游戏ToolStripMenuItem;
        private ToolStripMenuItem 帮助ToolStripMenuItem;
        private ToolStripMenuItem 遍历端口设置ToolStripMenuItem;
        private ToolStripMenuItem 网络前缀设置ToolStripMenuItem;
        private ToolStripMenuItem 用户名说明ToolStripMenuItem;
        private ToolStripMenuItem 端口及网络设置说明ToolStripMenuItem;
        private ToolStripMenuItem 游戏流程说明ToolStripMenuItem;
        private ToolStripMenuItem StartGameToolStripMenuItem;
        private ToolStripMenuItem FinishPrepareToolStripMenuItem;
        private ToolStripMenuItem InitializePlanesToolStripMenuItem;
        private ToolStripMenuItem BombToolStripMenuItem;
        private ToolStripMenuItem DisconnectToolStripMenuItem;
        private ToolStripMenuItem AssistToolStripMenuItem;
        private ToolStripMenuItem SetDelegateToolStripMenuItem;
        private ToolStripMenuItem DelegateInitializeToolStripMenuItem;
        private ToolStripMenuItem 代理轰炸ToolStripMenuItem;
        private ToolStripMenuItem DelegateToolStripMenuItem;
    }
}