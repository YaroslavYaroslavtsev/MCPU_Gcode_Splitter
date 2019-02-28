/*
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
using log4net;

namespace GCode_splitter.Core
{
    /// <summary>
    /// Description of Splitter.
    /// </summary>
    public class Splitter : SubSystem
    {
        public Splitter(string name)
            : base(name)
        {
            _cfg = Config.getEntity();
            _outputFolder = _cfg.read("cmnOutputDir");
            _chunkSize = int.Parse(_cfg.read("cmnProgSize"));
            _log = LogManager.GetLogger(name);
        }
		
        ILog _log;
        
        Config _cfg;
        int _chunkSize;
        public string _outputFolder { get; set; }
		
        List<string[]> fileset = new List<string[]>();
		
        public override void command(string command, params object[] arg)
        {
            switch (command) {
					
                case "split":
                    {
                        _log.Info("Command: " + command);
                        string _filename = arg[0].ToString();
                        split(_filename);
                    }
                    break;
					
            }
        }
	    

        string[] _split(string data, int size)
        {
            int idx_start = 0;
            int idx_end = size;
			
            List<string> _result = new List<string>();
            while (true) {
                // find index of \n
                while (idx_end > idx_start && data[idx_end--] != '\n') {
                }
                if (idx_end <= idx_start)
                    throw new Exception(Errors.CANNOT_SPLIT_FILE);
                _result.Add(data.Substring(idx_start, idx_end - idx_start + 1));
                idx_start = idx_end + 2;
                idx_end = idx_start + size;
                if (idx_end >= data.Length) {
                    _result.Add(data.Substring(idx_start, data.Length - idx_start));
                    break;
                }
            }
            return _result.ToArray();
        }
	
		
        public void split(string filename)
        {
            int _filesetnumber = 1;
            int _filenumber = 0;
			
            if (filename == "") {
                _log.Debug(Errors.FILE_NOT_SPECIFIED);
                _emitLogger(Errors.FILE_NOT_SPECIFIED);
            }
			
            fileset.Clear();
			
            _emitLogger(Messages.FILE_SPLIT_STARTED, filename);
            string lines = readSourceFile(filename);
			
            if (Directory.Exists(_outputFolder))
                Directory.Delete(_outputFolder, true);
            Directory.CreateDirectory(_outputFolder);
			
            string[] parts = _split(lines, _chunkSize);
            List<string> _currentfiles = new List<string>();
			
			
            for (int i = 0; i < parts.Length; i++) {
                string _filename;
                string _filebody;
                // write sub program
                _filename = string.Format("{0}\\{1:000}\\O{2:000}.gcd", _outputFolder, _filesetnumber, _filenumber + 2);
                Directory.CreateDirectory(string.Format("{0}\\{1:000}", _outputFolder, _filesetnumber));
                _filebody = GCodePrg.Create(string.Format("Part_{0:000}_{1:0}", _filesetnumber, _filenumber + 1), parts[i]);
                writeDestFile(_filename, _filebody);
                _currentfiles.Add(string.Format("O{0:000}.gcd", _filenumber + 2));
								
                if (_filenumber++ >= 3 || (i + 1) >= parts.Length) {
                    // write launcher program
                    _filename = string.Format("{0}\\{1:000}\\O001.gcd", _outputFolder, _filesetnumber);
                    _filebody = GCodePrg.CreateMain(string.Format("Part_{0:000}", _filesetnumber), _filenumber);
                    writeDestFile(_filename, _filebody);
                    _currentfiles.Add("O001.gcd");
                    _filenumber = 0;
                    _filesetnumber++;
                    _currentfiles.Sort();
                    fileset.Add(_currentfiles.ToArray());
                    _currentfiles.Clear();
                }
                _emitStateChange("progress", (i + 1) * 100 / parts.Length);
            }
            _emitStateChange("fileset", fileset);
            _emitLogger(Messages.FILE_SPLIT_COMPLETED, filename);
        }
		
		
		
		
		
		
		
		
		
		
		
        string readSourceFile(string fileName)
        {
            string content;
            var fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            using (var streamReader = new StreamReader(fileStream, Encoding.UTF8)) {
                content = streamReader.ReadToEnd();
            }
            return content;
        }
		
        void writeDestFile(string fileName, string body)
        {
            var fileStream = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.Write);
            using (var streamWriter = new StreamWriter(fileStream, Encoding.UTF8)) {
                streamWriter.WriteLine(body);
            }
        }
		
    }
}
