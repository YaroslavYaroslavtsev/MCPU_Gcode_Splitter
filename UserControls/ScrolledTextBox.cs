using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Drawing;

namespace GCode_splitter
{
	public partial class ScrolledTextBox : UserControl
	{
		public ScrolledTextBox()
		{
			InitializeComponent();
		}
		
		private string _temp;
		private int cnt;
			
		private string InputString {
			set {
				if (value != _temp && value != "") {
					cnt++;
					// limit rows
					if (cnt > 5000)
						richTextBox1.Text = "";
					_temp = value;
					richTextBox1.AppendText("[" + DateTime.Now + "] " + value + Environment.NewLine);
					ScrollTextBoxToEnd(richTextBox1.Handle, richTextBox1.Height);
				}
			}
			get { return _temp; }
		}
		
		public void append(string text, params object[] arg)
		{
			InputString = String.Format(text, arg);
		}

		void ScrollTextBoxToEnd(IntPtr handle, int height)
		{
			if (handle.Equals(IntPtr.Zero))
				return;
			SendMessage(handle, 0x115, 7, IntPtr.Zero);
		}
	
		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		static extern IntPtr SendMessage(IntPtr hWnd, UInt32 Msg, int wParam, IntPtr lParam);
	}
}
