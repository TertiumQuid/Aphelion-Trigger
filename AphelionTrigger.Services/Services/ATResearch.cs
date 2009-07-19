using System;
using System.Collections.Generic;
using System.ServiceProcess;
using System.Text;
using AphelionTrigger.Library;
using AphelionTrigger.Library.Logs;

namespace AphelionTrigger.Services
{
    public class ATResearch : ServiceBase
    {
        private int _errorCount = 0;

        public ATResearch()
		{
			this.ServiceName = "Aphelion Trigger Research Daemon";
			this.CanStop = true;
			this.CanPauseAndContinue = true;
			this.AutoLog = true;

            System.Environment.CurrentDirectory = AppDomain.CurrentDomain.BaseDirectory;
        }

        public void UpdateResearch( object stateInfo )
        {
            // only execute if no errors
            if ( _errorCount <= 10 )
            {
                try
                {
                    UpdateResearchCommand.UpdateResearch();
                }
                catch ( Exception e )
                {
                    _errorCount++;

                    SystemLogCommand.Log(
                        AphelionTrigger.Library.Logs.SystemLogType.Error,
                        AphelionTrigger.Library.Logs.SystemLogDestination.Database,
                        "Aphelion Trigger Web",
                        "Aphelion Trigger Research Daemon: " + e.Source,
                        e.Message,
                        "Stack Trace: " + e.StackTrace );
                }
            }
        }
    }
}
