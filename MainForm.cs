/*
 * Created by SharpDevelop.
 * User: yrslv
 * Date: 18.12.2018
 * Time: 9:43
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using System.Net;
using FtpLib;
using System.IO;
using System.Text;


using GCode_splitter.Core;



namespace GCode_splitter
{
	/// <summary>
	/// Description of MainForm.
	/// </summary>
	public partial class MainForm : Form
	{
		public MainForm()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			
			InitializeComponent();
            			
			_core.stateChange += updateState;
			config = Config.getEntity();
			//debug
			_file = config.read("lastFileName");
			filename.Text = _file ; //"G:\\finial_cds-Ultimaker-2-and-2-PLA-normal.gcode";
			

		}
        
		Config config;
		bool _flag;
		
		Controller _core = Controller.getEntity();
		
		// selected source file
		string _file = "";
		
		void updateState(string subsystem, string item, object state)
		{
			// common handler
			if (item == "log") {
				logWindow.append("[" + subsystem + "]" + state.ToString());
				Application.DoEvents();
			}
			if (item == "progress") {		
				int _state;
				if (int.TryParse(state.ToString(), out _state)) {
					progress.Visible = _state > 0 && _state < 100;
					progress.Value = _state;
				}
				Application.DoEvents();
														
			}
			
			switch (subsystem) {
				case "SPLITTER":
					switch (item) {
						case "fileset":
							var files = state as List<string[]>;
							if (files != null) {
							    l_fileset.Items.Clear();
							    for (int i = 0; i < files.Count; i++) {
									l_fileset.Items.Add(String.Format("{0:000}", i + 1));
									l_fileset.Tag = files;
								}
								l_fileset.SelectedIndex = 0;
							}
							break;
					}
					break;
				case "SENDER":
					switch (item) {
						case "state":
							bool _state;
							if (bool.TryParse(state.ToString(), out _state)) {
								icon.Image = _state  ?
								    this.icon.Image = images.Images[0] :
								    this.icon.Image = images.Images[1];
								
								//btn_runplc.Enabled = !_state;
								//btn_stopplc.Enabled = !_state;
								//btn_runcnc.Enabled = !_state;
								//btn_stopcnc.Enabled = !_state;
								bt_run.Enabled = !_state;
								bt_stop.Enabled = _state;
							}
							break;
						case "mode":
							mode.Text = state.ToString();
							break;
			            case "plcerrcode":
							plcError.Text = state.ToString();
							
						break;
						case "file":
                            int _file;
                            if (int.TryParse(state.ToString(), out _file)) {
                            l_fileset.SelectedIndex = _file - 1;
                            }    
                            
                        break;
					}
					break;
				default:
					break;
			}
			Application.DoEvents();
		
		}
			
		void ButtonSelect(object sender, EventArgs e)
		{
			using (OpenFileDialog ofd = new OpenFileDialog()) {
				ofd.Multiselect = false;
				if (DialogResult.OK == ofd.ShowDialog()) {
					_file = ofd.FileNames[0];
					filename.Text = _file;
					config.write("lastFileName",_file);

					logWindow.append(Messages.FILE_SELECTED, _file);
				}
			}
		}
	
		void Bt_runClick(object sender, EventArgs e)
		{
			if (_core.getState("SENDER", "state").ToString() == "run") {
				Dialogs.warning(Errors.FILE_SENDING_ALREADY_RAN);
				return;
			}
			if (l_fileset.SelectedItems.Count == 0) {
				Dialogs.warning(Errors.FILE_SET_NOT_SELECTED);
				return;
			}
			try {
				if (Dialogs.confirmation(Messages.LAUNCH_FILE_SENDING)) {
					_core.command("SENDER:run", l_fileset.SelectedIndex + 1, l_fileset.Items.Count );

				}
			} catch (Exception ex) {
				Dialogs.error(ex.Message);
			}
		}
		

	
		void Bt_stopClick(object sender, EventArgs e)
		{
			if (_core.getState("SENDER", "state").ToString() != "run") {
				Dialogs.warning(Errors.FILE_SENDING_ALREADY_STOPED);
				return;
			}
			
			try {
				if (Dialogs.confirmation(Messages.STOP_FILE_SENDING)) {
					_core.command("SENDER:stop");

				}
			} catch (Exception ex) {
				Dialogs.error(ex.Message);
			}
		}
		
		void Bt_splitClick(object sender, EventArgs e)
		{
			if (_file == "") {
				Dialogs.warning(Errors.FILE_NOT_SELECTED);
				return;
			}
			
			try {
				if (Dialogs.confirmation(Messages.SPLIT_SELECTED_FILE, _file)) {
					_core.command("SPLITTER:split", _file);
				}
			} catch (Exception ex) {
				Dialogs.error(ex.Message);
			}
		}
		
		void Bt_sendClick(object sender, EventArgs e)
		{
			if (l_fileset.SelectedItems.Count == 0) {
				Dialogs.warning(Errors.FILE_SET_NOT_SELECTED);
				return;
			}
			try {
				if (Dialogs.confirmation(Messages.SEND_FILE_SET, l_fileset.SelectedItems[0])) {
		            List<string[]> selectedSet = l_fileset.Tag as List<string[]>;
		            _core.command("SENDER:send", l_fileset.SelectedIndex + 1, selectedSet[l_fileset.SelectedIndex].Length);
				}
			} catch (Exception ex) {
				Dialogs.error(ex.Message);
			}
		}

		void L_filesetSelectedIndexChanged(object sender, EventArgs e)
		{
			var files = l_fileset.Tag as List<string[]>;
			if (files != null) {
				l_files.Items.Clear();
				l_files.Items.AddRange(files[l_fileset.SelectedIndex]);
			}
		}
		
        
        void Btn_stopClick(object sender, EventArgs e)
        {
          if (_core.getState("SENDER", "state").ToString() == "run") {
                Dialogs.warning(Errors.OPERATION_NOT_AVAILABLE);
                return;
            }
            try {
                if (Dialogs.confirmation(Messages.CONFIRM_OPERATION, "STOP MCPU")) {
                    _core.command("SENDER:stopplc");
                }
            } catch (Exception ex) {
                Dialogs.error(ex.Message);
            }
        }
        
        void Btn_runClick(object sender, EventArgs e)
        {
          if (_core.getState("SENDER", "state").ToString() == "run") {
                Dialogs.warning(Errors.OPERATION_NOT_AVAILABLE);
                return;
            }
            try {
                if (Dialogs.confirmation(Messages.CONFIRM_OPERATION, "RUN MCPU")) {
                    _core.command("SENDER:runplc");
                }
            } catch (Exception ex) {
                Dialogs.error(ex.Message);
            }
        }
        
        
        void _timerTick(object sender, EventArgs e)
        {
            if (!_flag) { 
                _flag = true;
                try {
                _core.command("SENDER:check");
                } catch (Exception ex) {
                Dialogs.error(ex.Message);
                }
                
                _flag = false;
            }
        }
        
        void Btn_runcncClick(object sender, EventArgs e)
        {
          if (_core.getState("SENDER", "state").ToString() == "run") {
                Dialogs.warning(Errors.OPERATION_NOT_AVAILABLE);
                return;
            }
            try {
                if (Dialogs.confirmation(Messages.CONFIRM_OPERATION, "RUN CNC")) {
                    _core.command("SENDER:runcnc");
                }
            } catch (Exception ex) {
                Dialogs.error(ex.Message);
            }
        }
        
        void Btn_stopcncClick(object sender, EventArgs e)
        {
           if (_core.getState("SENDER", "state").ToString() == "run") {
                Dialogs.warning(Errors.OPERATION_NOT_AVAILABLE);
                return;
            }
            try {
                if (Dialogs.confirmation(Messages.CONFIRM_OPERATION, "STOP CNC")) {
                    _core.command("SENDER:stopcnc");
                }
            } catch (Exception ex) {
                Dialogs.error(ex.Message);
            }
        }
       
        
	}
}
