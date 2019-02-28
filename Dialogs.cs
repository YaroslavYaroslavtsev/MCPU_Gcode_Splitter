/*
 * Created by SharpDevelop.
 * User: yaros
 * Date: 21.02.2019
 * Time: 9:10
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Windows.Forms;

namespace GCode_splitter
{
	/// <summary>
	/// Description of MessageBox.
	/// </summary>
	static public class Dialogs
	{
		
		static DialogResult generic(string caption, string text, MessageBoxButtons buttons, MessageBoxIcon icon)
		{
			return MessageBox.Show(text, caption, buttons, icon);
		}
		
		public static void information(string text, params object[] arg)
		{
			generic("Information", String.Format(text, arg), MessageBoxButtons.OK, MessageBoxIcon.Information);
		}
		
		public static void warning(string text, params object[] arg)
		{
			generic("Warning", String.Format(text, arg), MessageBoxButtons.OK, MessageBoxIcon.Warning);
		}
		
		public static void error(string text, params object[] arg)
		{
			generic("Error", String.Format(text, arg), MessageBoxButtons.OK, MessageBoxIcon.Error);
		}
		
		public static bool confirmation(string text, params object[] arg)
		{
			var _result = generic("Confirmation", String.Format(text, arg), MessageBoxButtons.YesNo, MessageBoxIcon.Question);
			return  (_result == DialogResult.Yes);
		}
	}
}
