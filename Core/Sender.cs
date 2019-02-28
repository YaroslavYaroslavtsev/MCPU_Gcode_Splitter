﻿/*
 * Created by SharpDevelop.
 * User: yaros
 * Date: 21.02.2019
 * Time: 11:41
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using FtpLib;
using log4net;

namespace GCode_splitter.Core
{
    /// <summary>
    /// Description of Splitter.
    /// </summary>
    public class Sender : SubSystem
    {
        public Sender(string name)
            : base(name)
        {
            _cfg = Config.getEntity();
            _ftp = new FtpConnection(_cfg.read("ftpIP"), _cfg.read("ftpUser"), _cfg.read("ftpPass"));
            _plc = new MxComp();
            _plc.onResult += onPlcResponse;
            _mcpupath = _cfg.read("plcFtpPath") ;
            _useCpu2 = _cfg.read("plcFtpCpu2") == "true" || _cfg.read("plcFtpCpu2") == "1";
             _log = LogManager.GetLogger(name);
             _machine = new StateMachine();
        }
		
        FtpConnection _ftp;
        MxComp _plc;
        Config _cfg;
        ILog _log;
        StateMachine _machine;
        
        enum MachineState{
            STOP = 0,
            SEND = 1,
            WAIT = 2
        } 
        
        MachineState _state = MachineState.STOP;
        string _mcpupath;
        bool _useCpu2;
        int _fileSetToSend;
        int _fileSetTotal;
        
        public override void command(string command, params object[] arg)
        {
            switch (command) {
					
                case "send":
                    {
                        int _fileset = int.Parse(arg[0].ToString());
                       _fileSend(_fileset);
                    }
                    break;
                 case "runplc":
                    {
                        _emitLogger("Switch MCPU to RUN");
                        _plc.Open();
                        _plc.RunPlc();
                        _plc.Close();
                        _emitLogger("MCPU switched to RUN");
                    }
                    break;
                  case "stopplc":
                    {
                        _emitLogger("Switch MCPU to STOP");
                        _plc.Open();
                        _plc.StopPlc();
                        _plc.Close();
                        _emitLogger("MCPU switched to STOP");
                    }
                    break;
					case "run":
                    {
                        int _fileset = int.Parse(arg[0].ToString());
                        int _filesetTotal = int.Parse(arg[1].ToString());
                        _fileSetToSend = _fileset;
                        _fileSetTotal = _filesetTotal;
                        _emitLogger("Automatic file sending is enabled");
                        _emitLogger("Send fileset {0:000} from {1:000}", _fileSetToSend, _fileSetTotal);
                        _fileSend(_fileSetToSend++);
                        _plc.continueProgramExecuting();
                        _state = MachineState.WAIT;
                        
                    }
                    break;
                    case "stop":
                    {
                        _emitLogger("Automatic file sending is disabled");
                        _state = MachineState.STOP;
                    }
                    break;
                    case "check":
                    {
                        switch(_state)
                        {
                            case MachineState.STOP:
                                // do nothing
                                break;
                            case MachineState.SEND:
                                    _emitLogger("Send fileset {0:000} from {1:000}", _fileSetToSend, _fileSetTotal);
                                    _fileSend(_fileSetToSend++);
                                    _plc.continueProgramExecuting();
                                    _state = MachineState.WAIT;
                                break;
                            case MachineState.WAIT:
                                if (_plc.isProgFinished() && _fileSetToSend <= _fileSetTotal)
                                {
                                    _state = MachineState.SEND;
                                } else{
                                    _emitLogger("All filesets are sended");
                                    _state = MachineState.STOP;
                                }
                                break;
                                
                        }
                        
                        _emitStateChange("state", _state != MachineState.STOP );
                        _emitStateChange("mode", getStateDesc(_state));
                        if (_state != MachineState.STOP) _emitStateChange("file", _fileSetToSend - 1 );
                        
                    }
                    break;
            }
        }
	    
        void onPlcResponse(object sender, string Result){
            _emitStateChange("plcerrcode", Result);
        }
     
        string getStateDesc(MachineState state)
        {
            switch(state){
                    case MachineState.SEND: return "SENDING";
                    case MachineState.STOP: return "STOP";
                   case MachineState.WAIT: return "WAITING";
                   default: return "------";
            }
        }
        
        void _fileSend(int fileset)
        {
            _emitLogger("Switch to stop MCPU");
            _plc.Open();
            _plc.StopPlc();
            _emitLogger("Clear space in MCPU");
            deleteFiles();
            _emitLogger("Start write files to MCPU");
            writeFiles(fileset);
            _emitLogger("Write to MCPU successfuly complited");
            _emitLogger("Switch to run MCPU");
            _plc.RunPlc();
            _plc.Close();
        }
        
        void deleteFiles()
        {
            _ftp.Open();
            _ftp.Login();
            if (_useCpu2) _ftp.SendCommand("cpuchg no2");
            try {
            // remove 5 files O001.gcd to O005.gcd
            for (int i = 1; i < 5; i++) {
                string _filename = _cfg.read("plcFtpPath") + "O" + i.ToString("000") + ".gcd";
                _log.Debug("File deleted from mcpu " + _filename);
                _ftp.RemoveFile(_filename);
            }
            } catch (Exception ex) {
            
            }
            _ftp.Close();
            //MessageBox.Show("Send file - OK");
        }
        
        void writeFiles(int fileSetNumber)
        {
            _ftp.Open();
            _ftp.Login();
            if (_useCpu2) _ftp.SendCommand("cpuchg no2");
            // write 4 files O001.gcd to O005.gcd
            for (int i = 1; i <= 5; i++) {
                string _srcfile = string.Format("{0}\\{1:000}\\O{2:000}.gcd", _cfg.read("cmnOutputDir"),fileSetNumber, i);
                if (File.Exists(_srcfile))
                   {
                _log.Debug("File write to mcpu " + _srcfile);
                _ftp.PutFile(_srcfile, _cfg.read("plcFtpPath") + "O" + i.ToString("000") + ".gcd");
            }
                }
            _ftp.Close();
        }
        
        
    }
}
