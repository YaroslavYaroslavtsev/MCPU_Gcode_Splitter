/*
 * Created by SharpDevelop.
 * User: yaroslav
 * Date: 20.02.2019
 * Time: 22:26
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;

namespace GCode_splitter.Core
{
    /// <summary>
    /// Description of Core.
    /// </summary>
    public class Controller
    {
        private Controller()
        {
            _subs = new List<SubSystem>();
            _subs.Add(new Splitter("SPLITTER"));
            _subs.Add(new Sender("SENDER"));
			
            foreach (SubSystem _item in _subs) {
                _item.stateChange += new SubSystem.stateDelegate(emitStateChange);
            }
        }
		
        static Controller _instance = null;
		
        public static Controller getEntity()
        {
            if (_instance == null)
                _instance = new Controller();
            return _instance;
        }
		
        List<SubSystem> _subs = new List<SubSystem>();
		
        public string[] getSubsystems()
        {
            var _result = new List<string>();
            foreach (SubSystem _item in _subs) {
                _result.Add(_item.Name);
            }
            return _result.ToArray();
        }
		
        // event
        public event SubSystem.stateDelegate stateChange;
		
        void emitStateChange(string subsystem, string item, object state)
        {
            if (stateChange != null)
                stateChange(subsystem, item, state);
        }
		
		
        // methods
        public void command(string command, params object[] arg)
        {
            string[] _splittedCommand = command.Split(new string[] { ":" }, StringSplitOptions.RemoveEmptyEntries);
            
            string _command = (_splittedCommand.Length > 1) ? _splittedCommand[1] : command;
            string _reciever = (_splittedCommand.Length > 1) ? _splittedCommand[0] : null;
            
            foreach (SubSystem _item in _subs) {
                if (_reciever != null) {
                    if (_item.Name == _reciever)
                        _item.command(_command, arg);
                } 
            }
		
        }
		
        public object getState(string subsystem, string item)
        {
            object _result = new object();
            
            foreach (SubSystem _item in _subs) {
                if (subsystem != null) {
                    if (_item.Name == subsystem) {
                        _result = _item.getState(item);
                        break;
                    }
                } 
            }
        
            return _result;
        }
		
		
		
		
		
    }
}
