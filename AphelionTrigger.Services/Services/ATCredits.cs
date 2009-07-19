using System;
using System.Collections.Generic;
using System.ServiceProcess;
using System.Text;
using AphelionTrigger.Library;
using AphelionTrigger.Library.Logs;

namespace AphelionTrigger.Services
{
    public class ATCredits : ServiceBase
    {
        private int _errorCount = 0;

        public ATCredits()
		{
			this.ServiceName = "Aphelion Trigger Credits Daemon";
			this.CanStop = true;
			this.CanPauseAndContinue = true;
			this.AutoLog = true;

            System.Environment.CurrentDirectory = AppDomain.CurrentDomain.BaseDirectory;
		}

        public void UpdateCredits( object stateInfo )
        {
            // invalidate chache for repopulation to ensure any changes to the configuration has been picked up
            ATConfiguration config = ATConfiguration.Instance;
            config.InvalidateCache();

            // only execute if no errors
            if ( _errorCount <= 10 )
            {
                try
                {
                    UpdateCreditsCommand.UpdateCredits();
                }
                catch ( Exception e )
                {
                    _errorCount++;

                    string details =
                       "Stack Trace: " + e.StackTrace;

                    SystemLogCommand.Log(
                        AphelionTrigger.Library.Logs.SystemLogType.Error,
                        AphelionTrigger.Library.Logs.SystemLogDestination.Database,
                        "Aphelion Trigger Web",
                        "Aphelion Trigger Credits Daemon: " + e.Source,
                        e.Message,
                        details );
                }
            }
        }
    }
}
