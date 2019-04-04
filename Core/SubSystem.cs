/*
 * Created by SharpDevelop.
 * User: yaros
 * Date: 21.02.2019
 * Time: 12:48
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace GCode_splitter.Core
{
	/// <summary>
	/// Description of SubSystem.
	/// </summary>
	public abstract class SubSystem
	{
		protected SubSystem()
		{
			_name = "";
		}
		
		protected SubSystem(string name)
		{
			_name = name;
		}
		
		string _name;
		
		public string Name {
			get { return _name; }
		}
		
		abstract public void command(string command, params object[] arg);
		
		virtual public object getState(string item)
		{
		    return new object();
		}
		// event
		public delegate void stateDelegate(string subsystem, string item, object state);
		
		public event stateDelegate stateChange;
		
		protected void _emitStateChange(string item, object state)
		{
			if (stateChange != null)
				stateChange(Name, item, state);
		}
		
		protected void _emitLogger(string text, params object[] arg)
		{
			_emitStateChange("log", string.Format(text, arg));
		}
		
	}
}
