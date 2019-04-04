/*
 * Created by SharpDevelop.
 * User: yaros
 * Date: 14.01.2019
 * Time: 15:13
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Text;
using System.Windows.Forms;
using ActUtlTypeLib;
	
namespace GCode_splitter
{
	/// <summary>
	/// Description of MxComp.
	/// </summary>
	public class MxComp
	{
		public MxComp()
		{
		}
		
		private  ActUtlTypeLib.ActUtlTypeClass axActUtlType1 = new ActUtlTypeLib.ActUtlTypeClass();
		
		public String ResultCode{get; set;}
		public int Result{get; set;}
		public int StationNumber{get; set;}
		
		public delegate void onResultDelegate(object sender, string Result);
		public event onResultDelegate onResult;
		
		
		public void Open()
        {
            int iReturnCode;				//Return code
            
            //
            //Processing of Open method
            //
            try
            {
                   //Set the value of 'LogicalStationNumber' to the property.
                    axActUtlType1.ActLogicalStationNumber = StationNumber;

                   
                    //Set the value of 'Password'.
                    axActUtlType1.ActPassword = "";

                    //The Open method is executed.
                    iReturnCode = axActUtlType1.Open();
                    
                    //When the Open method is succeeded, disable the TextBox of 'LogocalStationNumber'.
                    //When the Open method is succeeded, make the EventHandler of ActUtlType Controle.
                    if (iReturnCode == 0)
                    {
                      //  txt_LogicalStationNumber.Enabled = false;
                    }
            }
                  //Exception processing
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message,
                                  "MXComp", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //The return code of the method is displayed by the hexadecimal.
            ResultCode = String.Format("0x{0:x8} [HEX]", iReturnCode);
       		_raiseOnResult(ResultCode);
       }
		
		public void Close()
		{
			 int iReturnCode;				//Return code
            
            //
            //Processing of Close method
            //
            try
            {
                   //The Open method is executed.
                    iReturnCode = axActUtlType1.Close();
                    
                    //When the Open method is succeeded, disable the TextBox of 'LogocalStationNumber'.
                    //When the Open method is succeeded, make the EventHandler of ActUtlType Controle.
                    if (iReturnCode == 0)
                    {
                      //  txt_LogicalStationNumber.Enabled = false;
                    }
            }
                  //Exception processing
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message,
                                  "MXComp", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //The return code of the method is displayed by the hexadecimal.
            ResultCode = String.Format("0x{0:x8} [HEX]", iReturnCode);
            _raiseOnResult(ResultCode);
		}
		
		
		public void RunPlc()
		{
		    _writeCpu(0);
		}
		
		public void StopPlc()
		{
		    _writeCpu(1);
		}
		
		public bool isProgFinished()
        {
             int iReturnCode;                //Return code
             string deviceLabel = "M200";
             int dataArray = 0;
             
            //
            //Processing of Close method
            //
            try
            {
                   //The SetCpuStatus method is executed.
                   iReturnCode = axActUtlType1.GetDevice(deviceLabel, out dataArray);
             }
                  //Exception processing
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message,
                                  "MXComp", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            //The return code of the method is displayed by the hexadecimal.
            ResultCode = String.Format("0x{0:x8} [HEX]", iReturnCode);
            _raiseOnResult(ResultCode);
            return (iReturnCode == 0 && dataArray > 0);
        }
        	
		public void StartCNCprogram(int cmd){
                _writeRegister("M40", cmd);
             }
		
		public void StopCNCprogram(int cmd){
                _writeRegister("M41", cmd);
             }
		
		public void resetProgramFinishFlag(){
		          _writeRegister("M200", 0);
            }
		
		private void _writeRegister(string label, int value) {
		    int iReturnCode;                //Return code
            
		    try
            {
                iReturnCode = axActUtlType1.SetDevice(label, value);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message,
                                  "MXComp", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //The return code of the method is displayed by the hexadecimal.
            ResultCode = String.Format("0x{0:x8} [HEX]", iReturnCode);
            _raiseOnResult(ResultCode);
		}
		
		int _readRegister(string label) {
		     int iReturnCode;                //Return code
             int dataArray = 0;
            try
            {
                   iReturnCode = axActUtlType1.GetDevice(label, out dataArray);
             }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message,
                                  "MXComp", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return 0;
            }

            //The return code of the method is displayed by the hexadecimal.
            ResultCode = String.Format("0x{0:x8} [HEX]", iReturnCode);
            _raiseOnResult(ResultCode);
            return dataArray;
		}
		
		public void _writeCpu(int status)
        {
            int iReturnCode;                //Return code
            try
            {
                    iReturnCode = axActUtlType1.SetCpuStatus(status);
            }
                  //Exception processing
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message,
                                  "MXComp", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //The return code of the method is displayed by the hexadecimal.
            ResultCode = String.Format("0x{0:x8} [HEX]", iReturnCode);
            _raiseOnResult(ResultCode);
        }
		
		
		
		private void _raiseOnResult(string result)
		{
			if (onResult != null) onResult(this, result);
		}
		
		
	}
}
