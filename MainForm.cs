/*
 * Created by SharpDevelop.
 * User: yrslv
 * Date: 18.12.2018
 * Time: 9:43
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Net;
using FtpLib;
using System.IO;
using System.Text;

namespace GCode_splitter
{
	/// <summary>
	/// Description of MainForm.
	/// </summary>
	public partial class MainForm : Form
	{
		public MainForm()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			ftp = new FtpConnection(ip.Text, ftp_login.Text, ftp_pass.Text);
		}
	
		FtpLib.FtpConnection ftp;
		string _file = "";
		string _mcpupath = "4:/$MMTPRJ$/gcode/prog/";
		string _selectedFile = "";
		
		void ButtonSelect(object sender, EventArgs e)
		{
			using (OpenFileDialog ofd = new OpenFileDialog()) {
				ofd.Multiselect = false;
				if (DialogResult.OK == ofd.ShowDialog()) {
					_file = ofd.FileNames[0];
					filename.Text = _file;
					bt_split.Enabled = true;
				}
			}
		}
		
		void deleteFiles()
		{
			ftp.Open();
			ftp.Login();
			ftp.SendCommand("cpuchg no2");
			// remove 4 files O001.gcd to O004.gcd
			for(int i = 1; i < 5; i++){
				try
				{
					ftp.RemoveFile(_mcpupath + "O" + i.ToString("000") + ".gcd");
				}
				catch(Exception err)
				{
					MessageBox.Show(err.Message);
				}
			}
			ftp.Close();
			//MessageBox.Show("Send file - OK");
		}
		
		void writeFiles(string fileName)
		{
			ftp.Open();
			ftp.Login();
			ftp.SendCommand("cpuchg no2");
			// write 4 files O001.gcd to O004.gcd
			for(int i = 1; i < 5; i++){
				try
				{
					ftp.PutFile(fileName + i.ToString() , _mcpupath + "O" + i.ToString("000") + ".gcd");
				}
				catch(Exception err){
					MessageBox.Show(err.Message);
				}
			}
			ftp.Close();
		}
		
		string[] readSourceFile(string fileName)
		{
			var list = new List<string>();
			var fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read);
			using (var streamReader = new StreamReader(fileStream, Encoding.UTF8))
			{
    			string line;
    			while ((line = streamReader.ReadLine()) != null)
    			{
			        list.Add(line);
    			}
			}
			return list.ToArray();
		}
		
		void writeDestFile(string fileName, string[] lines)
		{
			var fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Write);
			using (var streamWriter = new StreamWriter(fileStream, Encoding.UTF8))
			{
				foreach(string line in lines)
				{
					streamWriter.WriteLine(line);
				}
			}
		}
		
		
		/// <summary>
		/// Split file to several .gcd files
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void Bt_splitClick(object sender, EventArgs e)
		{
			string[] lines =  readSourceFile(_file);
			if (!Directory.Exists(".\\Output")) Directory.CreateDirectory(".\\Output");
			
			for (int i = 1; i < 5; i++) {
				
			}
			
			bt_send.Enabled = true;
		}
		
	}
}
