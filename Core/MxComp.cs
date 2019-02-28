/*
 * Created by SharpDevelop.
 * User: yaros
 * Date: 14.01.2019
 * Time: 15:13
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Windows.Forms;
using MITSUBISHI.Component;
	
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
		
		private DotUtlType axActUtlType1 = new DotUtlType();
		
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
                   // axActUtlType1.ActPassword = txt_Password.Text;

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
			 int iReturnCode;				//Return code
            
            //
            //Processing of Close method
            //
            try
            {
                   //The SetCpuStatus method is executed.
                    iReturnCode = axActUtlType1.SetCpuStatus(0);
                    
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
		
		public void StopPlc()
		{
			 int iReturnCode;				//Return code
            
            //
            //Processing of Close method
            //
            try
            {
                   //The SetCpuStatus method is executed.
                    iReturnCode = axActUtlType1.SetCpuStatus(1);
                    
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
		
		public bool isProgFinished()
        {
             int iReturnCode;                //Return code
             string deviceLabel = "M";
             int[] dataArray = new int[1];
             
            //
            //Processing of Close method
            //
            try
            {
                   //The SetCpuStatus method is executed.
                   iReturnCode = axActUtlType1.ReadDeviceBlock(ref deviceLabel, 1, ref dataArray);
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
            return (iReturnCode == 0 && dataArray[0] > 0);
        }
        	
		public void continueProgramExecuting(){
		      int iReturnCode;                //Return code
             string deviceLabel = "M";
             short[] dataArray = new short[1];
             
            //
            //Processing of Write method
            //
            try
            {
                dataArray[0] = 1;
                iReturnCode = axActUtlType1.WriteDeviceBlock2(ref deviceLabel, 1, dataArray);
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
