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
using AMS.Profile;

namespace GCode_splitter
{
    /// <summary>
    /// Description of Config.
    /// </summary>
    public class Config
    {
        private Config()
        {
            //_map.Add("ftpIP", "192.168.1.27");
            //_map.Add("ftpUser", "RJ71EN71");
            //_map.Add("ftpPass", "RJ71EN71");
            //_map.Add("cmnOutputDir", ".\\Output");
            //_map.Add("cmnProgSize", "480000");
			//_map.Add("plcFtpPath", "4:/$MMTPRJ$/gcode/prog/");
			//_map.Add("plcFtpPath", "/");
			//_map.Add("plcFtpCpu2", "0");
            config_core = new AMS.Profile.Xml("settings.xml");
            config_core.SetValue("Splitter","version","1.0");
        }
		
        static Config _instance = null;
       
        readonly IProfile config_core;
        
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
            if (config_core.HasEntry("Splitter", name)) {
                return config_core.GetValue("Splitter", name).ToString().Replace("\r\n","").Trim();
            } else {
                config_core.SetValue("Splitter", name, defaultValue);
                return defaultValue;
            }
        }
        
        public void write(string name, string value)
        {
            config_core.SetValue("Splitter", name, value);
        }
        
        public string[] getSections(){
            return config_core.GetSectionNames();
        }
        
        public string[] getEntities(string section){
            return config_core.GetEntryNames(section);
        }
        
        
    }
}
