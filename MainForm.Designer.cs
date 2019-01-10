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
		private System.Windows.Forms.TextBox filename;
		private System.Windows.Forms.TextBox ftp_login;
		private System.Windows.Forms.TextBox ftp_pass;
		private System.Windows.Forms.Button bt_select;
		private System.Windows.Forms.Button bt_send;
		private System.Windows.Forms.TextBox dst_filename;
		private System.Windows.Forms.NumericUpDown numericUpDown1;
		private System.Windows.Forms.TextBox ip;
		private System.Windows.Forms.Button bt_split;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.ListView listView2;
		private System.Windows.Forms.ListView listView1;
		
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
			this.filename = new System.Windows.Forms.TextBox();
			this.ftp_login = new System.Windows.Forms.TextBox();
			this.ftp_pass = new System.Windows.Forms.TextBox();
			this.bt_select = new System.Windows.Forms.Button();
			this.bt_send = new System.Windows.Forms.Button();
			this.dst_filename = new System.Windows.Forms.TextBox();
			this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
			this.ip = new System.Windows.Forms.TextBox();
			this.bt_split = new System.Windows.Forms.Button();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.listView2 = new System.Windows.Forms.ListView();
			this.listView1 = new System.Windows.Forms.ListView();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.groupBox3.SuspendLayout();
			this.SuspendLayout();
			// 
			// filename
			// 
			this.filename.Location = new System.Drawing.Point(6, 29);
			this.filename.Name = "filename";
			this.filename.Size = new System.Drawing.Size(620, 20);
			this.filename.TabIndex = 0;
			// 
			// ftp_login
			// 
			this.ftp_login.Location = new System.Drawing.Point(64, 66);
			this.ftp_login.Name = "ftp_login";
			this.ftp_login.Size = new System.Drawing.Size(90, 20);
			this.ftp_login.TabIndex = 1;
			this.ftp_login.Text = "RJ71EN71";
			// 
			// ftp_pass
			// 
			this.ftp_pass.Location = new System.Drawing.Point(64, 102);
			this.ftp_pass.Name = "ftp_pass";
			this.ftp_pass.Size = new System.Drawing.Size(90, 20);
			this.ftp_pass.TabIndex = 2;
			this.ftp_pass.Text = "RJ71EN71";
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
			this.bt_send.Enabled = false;
			this.bt_send.Location = new System.Drawing.Point(571, 423);
			this.bt_send.Name = "bt_send";
			this.bt_send.Size = new System.Drawing.Size(111, 39);
			this.bt_send.TabIndex = 4;
			this.bt_send.Text = "Send";
			this.bt_send.UseVisualStyleBackColor = true;
			// 
			// dst_filename
			// 
			this.dst_filename.Location = new System.Drawing.Point(549, 262);
			this.dst_filename.Name = "dst_filename";
			this.dst_filename.ReadOnly = true;
			this.dst_filename.Size = new System.Drawing.Size(116, 20);
			this.dst_filename.TabIndex = 5;
			// 
			// numericUpDown1
			// 
			this.numericUpDown1.Location = new System.Drawing.Point(545, 288);
			this.numericUpDown1.Name = "numericUpDown1";
			this.numericUpDown1.Size = new System.Drawing.Size(120, 20);
			this.numericUpDown1.TabIndex = 6;
			// 
			// ip
			// 
			this.ip.Location = new System.Drawing.Point(64, 30);
			this.ip.Name = "ip";
			this.ip.Size = new System.Drawing.Size(90, 20);
			this.ip.TabIndex = 7;
			this.ip.Text = "192.168.1.16";
			// 
			// bt_split
			// 
			this.bt_split.Enabled = false;
			this.bt_split.Location = new System.Drawing.Point(571, 365);
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
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.ftp_pass);
			this.groupBox2.Controls.Add(this.label3);
			this.groupBox2.Controls.Add(this.label2);
			this.groupBox2.Controls.Add(this.label1);
			this.groupBox2.Controls.Add(this.ip);
			this.groupBox2.Controls.Add(this.ftp_login);
			this.groupBox2.Location = new System.Drawing.Point(528, 92);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(170, 147);
			this.groupBox2.TabIndex = 10;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "MCPU FTP";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(7, 105);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(60, 20);
			this.label3.TabIndex = 11;
			this.label3.Text = "Password:";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(7, 69);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(52, 17);
			this.label2.TabIndex = 9;
			this.label2.Text = "User:";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(7, 33);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(52, 17);
			this.label1.TabIndex = 8;
			this.label1.Text = "IP:";
			// 
			// groupBox3
			// 
			this.groupBox3.Controls.Add(this.listView2);
			this.groupBox3.Controls.Add(this.listView1);
			this.groupBox3.Location = new System.Drawing.Point(13, 93);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(509, 369);
			this.groupBox3.TabIndex = 11;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "Output";
			// 
			// listView2
			// 
			this.listView2.Location = new System.Drawing.Point(264, 29);
			this.listView2.Name = "listView2";
			this.listView2.Size = new System.Drawing.Size(225, 321);
			this.listView2.TabIndex = 1;
			this.listView2.UseCompatibleStateImageBehavior = false;
			// 
			// listView1
			// 
			this.listView1.Location = new System.Drawing.Point(17, 29);
			this.listView1.Name = "listView1";
			this.listView1.Size = new System.Drawing.Size(225, 321);
			this.listView1.TabIndex = 0;
			this.listView1.UseCompatibleStateImageBehavior = false;
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(710, 474);
			this.Controls.Add(this.bt_split);
			this.Controls.Add(this.groupBox3);
			this.Controls.Add(this.bt_send);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.numericUpDown1);
			this.Controls.Add(this.dst_filename);
			this.Name = "MainForm";
			this.Text = "G-Code file splitter";
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			this.groupBox3.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}
	}
}
