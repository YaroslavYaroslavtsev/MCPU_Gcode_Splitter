/*
 * Created by SharpDevelop.
 * User: yaros
 * Date: 21.02.2019
 * Time: 9:23
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace GCode_splitter
{
	/// <summary>
	/// Description of Messages.
	/// </summary>
	public static class Messages
	{
	    
		public const string LAUNCH_FILE_SENDING = "Launch file sending?";
		public const string STOP_FILE_SENDING = "Stop file sending?";
		public const string FILE_SELECTED = "{0} file is selected";
		public const string CONFIRM_OPERATION = "Confirm {0} operation";
		public const string SPLIT_SELECTED_FILE = "Split {0} file?";
		public const string SEND_FILE_SET = "Send {0} file set?";
		public const string SEND_FILE = "Send {0} file to MCPU?";
        public const string WAIT_FILE_SENDING = "Turn on file part complete waiting?";
		public const string FILE_SPLIT_STARTED = "File {0} splitting is started.";
		public const string FILE_SPLIT_COMPLETED = "File {0} splitting is complete";
	}
	
	public static class Errors
	{
	    public const string FILE_NOT_SELECTED = "File is not selected";
        public const string FILE_SET_NOT_SELECTED = "File set is not selected";
        public const string FILE_NOT_SPECIFIED = "File is not specified";
        public const string FILE_SENDING_ALREADY_RAN = "Files sending are already ran";
        public const string FILE_SENDING_ALREADY_STOPED = "Files sending are already stoped";
        public const string SEND_NOT_AVAILABLE = "Manual send is not avalible during automatic sending";
        public const string CANNOT_SPLIT_FILE = "Can not split file with specified part size.";
        public const string OPERATION_NOT_AVAILABLE = "Operation not available now";
        
             
	}
}
