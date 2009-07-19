using System;
using System.Collections.Generic;
using System.ServiceProcess;
using System.Text;
using AphelionTrigger.Library;
using AphelionTrigger.Library.Logs;

namespace AphelionTrigger.Services
{
    public class ATTurns : ServiceBase
    {
        private int _errorCount = 0;

        public ATTurns()
		{
            this.ServiceName = "Aphelion Trigger Turn Daemon";
			this.CanStop = true;
			this.CanPauseAndContinue = true;
			this.AutoLog = true;

            System.Environment.CurrentDirectory = AppDomain.CurrentDomain.BaseDirectory;
		}

        public void UpdateTurns( object stateInfo )
        {
            // invalidate chache for repopulation to ensure any changes to the configuration has been picked up
            ATConfiguration config = ATConfiguration.Instance;
            config.InvalidateCache();

            // only execute if no errors
            if ( _errorCount <= 10 )
            {
                try
                {
                    UpdateTurnsCommand.UpdateTurns();
                }
                catch ( Exception e )
                {
                    _errorCount++;

                    SystemLogCommand.Log(
                        AphelionTrigger.Library.Logs.SystemLogType.Error,
                        AphelionTrigger.Library.Logs.SystemLogDestination.Database,
                        "Aphelion Trigger Web",
                        "Aphelion Trigger Turns Daemon: " + e.Source,
                        e.Message,
                        "Stack Trace: " + e.StackTrace );
                }
            }
        }
    }
}
