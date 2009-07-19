using System;
using System.Net.Mail;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using Csla;

namespace AphelionTrigger.Library.Logs
{
    [Serializable()]
    public class SystemLogCommand : CommandBase
    {
        #region Business Methods
        private string _application;
        private string _source;
        private string _message;
        private string _details;

        private SystemLogDestination _destination;
        private SystemLogType _systemLogType;
        #endregion

        #region Factory Methods
        private SystemLogCommand( SystemLogType systemLogType, SystemLogDestination destination, string application, string source, string message, string details )
        {
            _systemLogType = systemLogType;
            _destination = destination;

            _application = application;
            _source = source;
            _message = message;
            _details = details;
        }


        private SystemLogCommand( SystemLogType systemLogType, SystemLogDestination destination, string application, Exception e )
        {
            _systemLogType = systemLogType;
            _destination = destination;
            
            _application = application;
            _source = e.Source;
            _message = e.Message;
            _details = e.StackTrace;
        }

        public static void Log( SystemLogType systemLogType, SystemLogDestination destination, string application, string source, string message, string details )
        {
            SystemLogCommand command;
            command = DataPortal.Execute<SystemLogCommand>( new SystemLogCommand( systemLogType, destination, application, source, message, details ) );
        }

        public static void Log( SystemLogType systemLogType, SystemLogDestination destination, string application, Exception e )
        {
            SystemLogCommand command;
            command = DataPortal.Execute<SystemLogCommand>( new SystemLogCommand( systemLogType, destination, application, e ) );
        }
        #endregion

        #region Data Access
        protected override void DataPortal_Execute()
        {
            switch ( _destination )
            {
                case SystemLogDestination.Database:
                    LogToDataBase();
                    break;
                case SystemLogDestination.WinidowsEventLog:
                    LogToWindowsEventLog();
                    break;
                case SystemLogDestination.Email:
                    LogToEmail();
                    break;
                case SystemLogDestination.TextFile:
                    LogToTextFile();
                    break;

            }
        }

        private void LogToDataBase()
        {
            // create a system log business object and save to the database
            SystemLog log = new SystemLog( _systemLogType, _application, _source, _message, _details ).Save();
        }

        private void LogToWindowsEventLog()
        {
            string logName = "Aphelion Trigger";
            EventLog log = new EventLog();

            // map the system log type against the event log type
            EventLogEntryType type = EventLogEntryType.FailureAudit;
            switch (_systemLogType)
            {
                case SystemLogType.Debug:
                    type = EventLogEntryType.SuccessAudit;
                    break;
                case SystemLogType.Error:
                    type = EventLogEntryType.Error;
                    break;
                case SystemLogType.Information:
                    type = EventLogEntryType.Information;
                    break;
                case SystemLogType.Warning:
                    type = EventLogEntryType.Warning;
                    break;
            }

            // if necessary, register the application as an event source
            if ( !EventLog.SourceExists( logName ) ) EventLog.CreateEventSource( _application, logName );
            
            log.Source = logName;
            string message = _message + System.Environment.NewLine + System.Environment.NewLine + _details;

            log.WriteEntry( message, type );
        }

        private void LogToEmail()
        {
            UserList recipients = UserList.GetUserSystemLogRecipients( (int)_systemLogType );

            foreach ( User user in recipients )
            {
                try
                {
                    if (user.Email.Length == 0) continue;
                    
                    MailAddress from = new MailAddress( "system@apheliontrigger.com", "Aphelion Trigger" );
                    MailAddress to = new MailAddress( user.Email, user.Username );

                    MailMessage email = new MailMessage( from, to );

                    email.Subject = "System Log (" + _systemLogType.ToString() + "): " + _source;
                    email.Body = _source 
                        + System.Environment.NewLine 
                        + System.Environment.NewLine 
                        + _message 
                        + System.Environment.NewLine 
                        + System.Environment.NewLine 
                        + _details;

                    SmtpClient smtp = new SmtpClient();

                    smtp.Send( email );
                }
                catch
                {
                    // continue trying to notify recipients
                }
            }

        }

        private void LogToTextFile()
        {
        }
        #endregion
    }
}
