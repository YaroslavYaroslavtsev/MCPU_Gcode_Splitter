/*
 * Created by SharpDevelop.
 * User: yaros
 * Date: 27.02.2019
 * Time: 14:43
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace GCode_splitter.Core
{
    /// <summary>
    /// Description of StateMachine.
    /// </summary>
    public class StateMachine
    {
        public StateMachine()
        {
        }

        enum MachineState
        {
            STOP = 0,
            SEND = 1,
            WAIT = 2
        }

        int FileNumber { get; set; }

        MxComp mx { get; set; }


        MachineState _state = MachineState.STOP;


        public void run(int fileNumber)
        {
            FileNumber = fileNumber;
            _state = MachineState.SEND;
        }

        public void stop()
        {

        }

        public void check()
        {
            switch (_state)
            {
                case MachineState.STOP:
                    //do nothing
                    break;

                case MachineState.WAIT:
                    if (mx.isProgFinished())
                    {

                    }
                    break;
            }
        }

        public delegate void ActionDelegate();

        public event ActionDelegate DoAction;

        void raiseAction()
        {
            if (DoAction != null) DoAction();
        }

    }
}
