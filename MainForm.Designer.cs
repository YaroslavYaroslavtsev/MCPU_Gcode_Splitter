/*
 * Created by SharpDevelop.
 * User: yrslv
 * Date: 18.12.2018
 * Time: 9:43
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace GCode_splitter
{
	partial class MainForm
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		
		private System.ComponentModel.IContainer components = null;
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
		private System.Windows.Forms.TextBox filename;
		private System.Windows.Forms.Button bt_select;
		private System.Windows.Forms.Button bt_send;
		private System.Windows.Forms.Button bt_split;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.ListBox l_fileset;
		private GCode_splitter.ScrolledTextBox logWindow;
		private System.Windows.Forms.Button bt_run;
		private System.Windows.Forms.Button bt_stop;
		private System.Windows.Forms.StatusStrip _status;
		private System.Windows.Forms.ToolStripStatusLabel icon;
		private System.Windows.Forms.ToolStripStatusLabel mode;
		private System.Windows.Forms.ToolStripProgressBar progress;
		private System.Windows.Forms.Timer _timer;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.ListBox l_files;
		private System.Windows.Forms.ToolStripSplitButton subMenuButton;
		private System.Windows.Forms.ToolStripMenuItem sTOPMCPUToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem rUNMCPUToolStripMenuItem;
		private System.Windows.Forms.ToolStripStatusLabel plcError;
		
		/// <summary>
		/// Disposes resources used by the form.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}
		
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent()
		{
		    this.components = new System.ComponentModel.Container();
		    System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
		    this.filename = new System.Windows.Forms.TextBox();
		    this.bt_select = new System.Windows.Forms.Button();
		    this.bt_send = new System.Windows.Forms.Button();
		    this.bt_split = new System.Windows.Forms.Button();
		    this.groupBox1 = new System.Windows.Forms.GroupBox();
		    this.groupBox3 = new System.Windows.Forms.GroupBox();
		    this.l_files = new System.Windows.Forms.ListBox();
		    this.l_fileset = new System.Windows.Forms.ListBox();
		    this.label2 = new System.Windows.Forms.Label();
		    this.label1 = new System.Windows.Forms.Label();
		    this.logWindow = new GCode_splitter.ScrolledTextBox();
		    this._timer = new System.Windows.Forms.Timer(this.components);
		    this.bt_run = new System.Windows.Forms.Button();
		    this.bt_stop = new System.Windows.Forms.Button();
		    this._status = new System.Windows.Forms.StatusStrip();
		    this.icon = new System.Windows.Forms.ToolStripStatusLabel();
		    this.mode = new System.Windows.Forms.ToolStripStatusLabel();
		    this.subMenuButton = new System.Windows.Forms.ToolStripSplitButton();
		    this.sTOPMCPUToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		    this.rUNMCPUToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		    this.plcError = new System.Windows.Forms.ToolStripStatusLabel();
		    this.progress = new System.Windows.Forms.ToolStripProgressBar();
		    this.groupBox1.SuspendLayout();
		    this.groupBox3.SuspendLayout();
		    this._status.SuspendLayout();
		    this.SuspendLayout();
		    // 
		    // filename
		    // 
		    this.filename.Location = new System.Drawing.Point(6, 29);
		    this.filename.Name = "filename";
		    this.filename.Size = new System.Drawing.Size(620, 20);
		    this.filename.TabIndex = 0;
		    // 
		    // bt_select
		    // 
		    this.bt_select.Location = new System.Drawing.Point(632, 29);
		    this.bt_select.Name = "bt_select";
		    this.bt_select.Size = new System.Drawing.Size(38, 20);
		    this.bt_select.TabIndex = 3;
		    this.bt_select.Text = "...";
		    this.bt_select.UseVisualStyleBackColor = true;
		    this.bt_select.Click += new System.EventHandler(this.ButtonSelect);
		    // 
		    // bt_send
		    // 
		    this.bt_send.Location = new System.Drawing.Point(560, 196);
		    this.bt_send.Name = "bt_send";
		    this.bt_send.Size = new System.Drawing.Size(111, 39);
		    this.bt_send.TabIndex = 4;
		    this.bt_send.Text = "Send";
		    this.bt_send.UseVisualStyleBackColor = true;
		    this.bt_send.Click += new System.EventHandler(this.Bt_sendClick);
		    // 
		    // bt_split
		    // 
		    this.bt_split.Location = new System.Drawing.Point(560, 138);
		    this.bt_split.Name = "bt_split";
		    this.bt_split.Size = new System.Drawing.Size(111, 39);
		    this.bt_split.TabIndex = 8;
		    this.bt_split.Text = "Split";
		    this.bt_split.UseVisualStyleBackColor = true;
		    this.bt_split.Click += new System.EventHandler(this.Bt_splitClick);
		    // 
		    // groupBox1
		    // 
		    this.groupBox1.Controls.Add(this.bt_select);
		    this.groupBox1.Controls.Add(this.filename);
		    this.groupBox1.Location = new System.Drawing.Point(13, 13);
		    this.groupBox1.Name = "groupBox1";
		    this.groupBox1.Size = new System.Drawing.Size(685, 73);
		    this.groupBox1.TabIndex = 9;
		    this.groupBox1.TabStop = false;
		    this.groupBox1.Text = "G-Code file";
		    // 
		    // groupBox3
		    // 
		    this.groupBox3.Controls.Add(this.l_files);
		    this.groupBox3.Controls.Add(this.l_fileset);
		    this.groupBox3.Controls.Add(this.label2);
		    this.groupBox3.Controls.Add(this.label1);
		    this.groupBox3.Location = new System.Drawing.Point(13, 93);
		    this.groupBox3.Name = "groupBox3";
		    this.groupBox3.Size = new System.Drawing.Size(509, 369);
		    this.groupBox3.TabIndex = 11;
		    this.groupBox3.TabStop = false;
		    this.groupBox3.Text = "Output";
		    // 
		    // l_files
		    // 
		    this.l_files.BackColor = System.Drawing.Color.White;
		    this.l_files.Enabled = false;
		    this.l_files.FormattingEnabled = true;
		    this.l_files.Location = new System.Drawing.Point(264, 45);
		    this.l_files.Name = "l_files";
		    this.l_files.Size = new System.Drawing.Size(231, 303);
		    this.l_files.TabIndex = 17;
		    // 
		    // l_fileset
		    // 
		    this.l_fileset.FormattingEnabled = true;
		    this.l_fileset.Location = new System.Drawing.Point(17, 45);
		    this.l_fileset.Name = "l_fileset";
		    this.l_fileset.Size = new System.Drawing.Size(231, 303);
		    this.l_fileset.TabIndex = 16;
		    // 
		    // label2
		    // 
		    this.label2.Location = new System.Drawing.Point(264, 19);
		    this.label2.Name = "label2";
		    this.label2.Size = new System.Drawing.Size(100, 23);
		    this.label2.TabIndex = 3;
		    this.label2.Text = "Files:";
		    // 
		    // label1
		    // 
		    this.label1.Location = new System.Drawing.Point(17, 20);
		    this.label1.Name = "label1";
		    this.label1.Size = new System.Drawing.Size(100, 23);
		    this.label1.TabIndex = 2;
		    this.label1.Text = "File sets:";
		    // 
		    // logWindow
		    // 
		    this.logWindow.BackColor = System.Drawing.Color.White;
		    this.logWindow.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
		    this.logWindow.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
		    this.logWindow.ForeColor = System.Drawing.Color.Black;
		    this.logWindow.Location = new System.Drawing.Point(13, 472);
		    this.logWindow.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
		    this.logWindow.Name = "logWindow";
		    this.logWindow.Size = new System.Drawing.Size(685, 148);
		    this.logWindow.TabIndex = 12;
		    // 
		    // _timer
		    // 
		    this._timer.Enabled = true;
		    this._timer.Interval = 1000;
		    this._timer.Tick += new System.EventHandler(this._timerTick);
		    // 
		    // bt_run
		    // 
		    this.bt_run.Location = new System.Drawing.Point(560, 255);
		    this.bt_run.Name = "bt_run";
		    this.bt_run.Size = new System.Drawing.Size(111, 39);
		    this.bt_run.TabIndex = 14;
		    this.bt_run.Text = "Run";
		    this.bt_run.UseVisualStyleBackColor = true;
		    this.bt_run.Click += new System.EventHandler(this.Bt_runClick);
		    // 
		    // bt_stop
		    // 
		    this.bt_stop.Location = new System.Drawing.Point(560, 313);
		    this.bt_stop.Name = "bt_stop";
		    this.bt_stop.Size = new System.Drawing.Size(111, 39);
		    this.bt_stop.TabIndex = 13;
		    this.bt_stop.Text = "Stop";
		    this.bt_stop.UseVisualStyleBackColor = true;
		    this.bt_stop.Click += new System.EventHandler(this.Bt_stopClick);
		    // 
		    // _status
		    // 
		    this._status.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.icon,
            this.mode,
            this.subMenuButton,
            this.plcError,
            this.progress});
		    this._status.Location = new System.Drawing.Point(0, 627);
		    this._status.Name = "_status";
		    this._status.ShowItemToolTips = true;
		    this._status.Size = new System.Drawing.Size(710, 22);
		    this._status.TabIndex = 15;
		    this._status.Text = "statusStrip1";
		    this._status.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this._statusItemClicked);
		    // 
		    // icon
		    // 
		    this.icon.AutoSize = false;
		    this.icon.AutoToolTip = true;
		    this.icon.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		    this.icon.Image = ((System.Drawing.Image)(resources.GetObject("icon.Image")));
		    this.icon.Name = "icon";
		    this.icon.Size = new System.Drawing.Size(17, 17);
		    this.icon.ToolTipText = "Automatic file sending state";
		    // 
		    // mode
		    // 
		    this.mode.AutoSize = false;
		    this.mode.Name = "mode";
		    this.mode.Size = new System.Drawing.Size(100, 17);
		    this.mode.Text = "STOP";
		    // 
		    // subMenuButton
		    // 
		    this.subMenuButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		    this.subMenuButton.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sTOPMCPUToolStripMenuItem,
            this.rUNMCPUToolStripMenuItem});
		    this.subMenuButton.Image = ((System.Drawing.Image)(resources.GetObject("subMenuButton.Image")));
		    this.subMenuButton.ImageTransparentColor = System.Drawing.Color.Magenta;
		    this.subMenuButton.Name = "subMenuButton";
		    this.subMenuButton.Size = new System.Drawing.Size(32, 20);
		    this.subMenuButton.Text = "toolStripSplitButton1";
		    this.subMenuButton.ButtonClick += new System.EventHandler(this.ToolStripSplitButton1ButtonClick);
		    // 
		    // sTOPMCPUToolStripMenuItem
		    // 
		    this.sTOPMCPUToolStripMenuItem.Name = "sTOPMCPUToolStripMenuItem";
		    this.sTOPMCPUToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
		    this.sTOPMCPUToolStripMenuItem.Text = "STOP MCPU";
		    this.sTOPMCPUToolStripMenuItem.Click += new System.EventHandler(this.STOPMCPUToolStripMenuItemClick);
		    // 
		    // rUNMCPUToolStripMenuItem
		    // 
		    this.rUNMCPUToolStripMenuItem.Name = "rUNMCPUToolStripMenuItem";
		    this.rUNMCPUToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
		    this.rUNMCPUToolStripMenuItem.Text = "RUN MCPU";
		    this.rUNMCPUToolStripMenuItem.Click += new System.EventHandler(this.RUNMCPUToolStripMenuItemClick);
		    // 
		    // plcError
		    // 
		    this.plcError.AutoSize = false;
		    this.plcError.Name = "plcError";
		    this.plcError.Size = new System.Drawing.Size(100, 17);
		    // 
		    // progress
		    // 
		    this.progress.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
		    this.progress.AutoSize = false;
		    this.progress.Name = "progress";
		    this.progress.Size = new System.Drawing.Size(100, 16);
		    this.progress.Visible = false;
		    // 
		    // MainForm
		    // 
		    this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
		    this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		    this.ClientSize = new System.Drawing.Size(710, 649);
		    this.Controls.Add(this._status);
		    this.Controls.Add(this.bt_run);
		    this.Controls.Add(this.bt_stop);
		    this.Controls.Add(this.logWindow);
		    this.Controls.Add(this.bt_split);
		    this.Controls.Add(this.groupBox3);
		    this.Controls.Add(this.bt_send);
		    this.Controls.Add(this.groupBox1);
		    this.Name = "MainForm";
		    this.Text = "G-Code split&sender";
		    this.groupBox1.ResumeLayout(false);
		    this.groupBox1.PerformLayout();
		    this.groupBox3.ResumeLayout(false);
		    this._status.ResumeLayout(false);
		    this._status.PerformLayout();
		    this.ResumeLayout(false);
		    this.PerformLayout();

		}
	}
		
}

