/*
 * Created by SharpDevelop.
 * User: yaros
 * Date: 14.01.2019
 * Time: 15:30
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;

namespace GCode_splitter
{
	/// <summary>
	/// Description of GCodePrg.
	/// </summary>
	public class GCodePrg
	{
		static List<String> _prg = new List<string>();
		
		private GCodePrg()
		{
		}
		
		public static string Create(string comment, string body)
		{
		        // sub program
				_prg.Clear();
				_prg.Add("%");
				_prg.Add(String.Format("({0})", comment));
				_prg.Add(body.Trim(new []{'\r','\n'}));
				_prg.Add("M02");
				_prg.Add("%");
			
			
			return String.Join("\r\n", _prg.ToArray());
		}
		
	
		public static string CreateMain(string comment, int subfilesnum)
		{
			
				// main program	
				_prg.Clear();
				_prg.Add("%");
				_prg.Add(String.Format("({0})", comment));
				for (int i = 2; i < subfilesnum + 2; i++) _prg.Add("M98 P" + i.ToString("000"));
				_prg.Add("M02");
				_prg.Add("%");
			
			
			return String.Join("\r\n", _prg.ToArray());
		}
		
		
	}
}
