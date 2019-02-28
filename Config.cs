/*
 * Created by SharpDevelop.
 * User: yaros
 * Date: 20.02.2019
 * Time: 14:10
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
	

namespace GCode_splitter
{
    /// <summary>
    /// Description of Config.
    /// </summary>
    public class Config
    {
        private Config()
        {
            _map.Add("ftpIP", "192.168.1.27");
            _map.Add("ftpUser", "RJ71EN71");
            _map.Add("ftpPass", "RJ71EN71");
            _map.Add("cmnOutputDir", ".\\Output");
            _map.Add("cmnProgSize", "480000");
			//_map.Add("plcFtpPath", "4:/$MMTPRJ$/gcode/prog/");
			_map.Add("plcFtpPath", "/");
			_map.Add("plcFtpCpu2", "0");
            
        }
		
        static Config _instance = null;
        readonly Dictionary<string, string> _map = new Dictionary<string, string>();
			
        static public Config getEntity()
        {
            if (_instance == null)
                _instance = new Config();
            return _instance;
        }
		
        public int readInt(string name)
        {
            bool _flag;
            int _result;
			
            _flag = Int32.TryParse(read(name), out _result);
            if (_flag)
                return _result;
            throw new Exception("Param value incorrect");
        }
		
		
        public string read(string name, string defaultValue = "")
        {
            string _result;
            bool _flag;
            _flag = _map.TryGetValue(name, out _result);
            if (_flag)
                return _result;
            return defaultValue;
        }
    }
}
