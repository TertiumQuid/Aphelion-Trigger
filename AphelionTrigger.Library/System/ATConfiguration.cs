using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using Csla;
using Csla.Data;
using Csla.Validation;
using AphelionTrigger.Library.Security;

namespace AphelionTrigger.Library
{
    [Serializable()]
    public sealed class ATConfiguration
    {
        #region Business Methods
        public bool RegistrationActive
        {
            get { bool x = false; return bool.TryParse( List["RegistrationActive"].ToString(), out x ) ? Convert.ToBoolean( List["RegistrationActive"] ) : x; }
        }

        public double Version
        {
            get { double x = 0; return double.TryParse( List["Version"].ToString(), out x ) ? Convert.ToDouble( List["Version"] ) : x; }
        }

        public string WelcomeText
        {
            get { return List["WelcomeText"].ToString(); }
        }

        public string RegistrationActiveText
        {
            get { return List["RegistrationActiveText"].ToString(); }
        }

        public string RegistrationInactiveText
        {
            get { return List["RegistrationInactiveText"].ToString(); }
        }

        public string PlayerProtocols
        {
            get { return List["PlayerProtocols"].ToString(); }
        }

        public int LevelCap
        {
            get { int x = 0; return int.TryParse( List["LevelCap"].ToString(), out x ) ? Convert.ToInt32( List["LevelCap"] ) : x; }
        }

        public int AttackDelay
        {
            get { int x = 0; return int.TryParse( List["AttackDelay"].ToString(), out x ) ? Convert.ToInt32( List["AttackDelay"] ) : x; }
        }

        public int TurnUnitStep
        {
            get { int x = 0; return int.TryParse( List["TurnUnitStep"].ToString(), out x ) ? Convert.ToInt32( List["TurnUnitStep"] ) : x; }
        }

        public int TurnUnitCap
        {
            get { int x = 0; return int.TryParse( List["TurnUnitCap"].ToString(), out x ) ? Convert.ToInt32( List["TurnUnitCap"] ) : x; }
        }

        public double TurnFactor
        {
            get { double x = 0; return double.TryParse( List["TurnFactor"].ToString(), out x ) ? Convert.ToDouble( List["TurnFactor"] ) : x; }
        }

        public double CreditsFactor
        {
            get { double x = 0; return double.TryParse( List["CreditsFactor"].ToString(), out x ) ? Convert.ToDouble( List["CreditsFactor"] ) : x; }
        }

        public double CaptureCap
        {
            get { double x = 0; return double.TryParse( List["CaptureCap"].ToString(), out x ) ? Convert.ToDouble( List["CaptureCap"] ) : x; }
        }

        public double CaptureFactor
        {
            get { double x = 0; return double.TryParse( List["CaptureFactor"].ToString(), out x ) ? Convert.ToDouble( List["CaptureFactor"] ) : x; }
        }

        public int CaptureDivisor
        {
            get { int x = 0; return int.TryParse( List["CaptureDivisor"].ToString(), out x ) ? Convert.ToInt32( List["CaptureDivisor"] ) : x; }
        }

        public int StartingCredits
        {
            get { int x = 0; return int.TryParse( List["StartingCredits"].ToString(), out x ) ? Convert.ToInt32( List["StartingCredits"] ) : x; }
        }

        public int StartingTurns
        {
            get { int x = 0; return int.TryParse( List["StartingTurns"].ToString(), out x ) ? Convert.ToInt32( List["StartingTurns"] ) : x; }
        }

        public int ReportsRefreshRate
        {
            get { int x = 0; return int.TryParse( List["ReportsRefreshRate"].ToString(), out x ) ? Convert.ToInt32( List["ReportsRefreshRate"] ) : x; }
        }

        public int MessagesRefreshRate
        {
            get { int x = 0; return int.TryParse( List["MessagesRefreshRate"].ToString(), out x ) ? Convert.ToInt32( List["MessagesRefreshRate"] ) : x; }
        }

        public int AttacksRefreshRate
        {
            get { int x = 0; return int.TryParse( List["AttacksRefreshRate"].ToString(), out x ) ? Convert.ToInt32( List["AttacksRefreshRate"] ) : x; }
        }

        public double CasualtyFactor
        {
            get { double x = 0; return double.TryParse( List["CasualtyFactor"].ToString(), out x ) ? Convert.ToDouble( List["CasualtyFactor"] ) : x; }
        }

        public double FactionLeaderBonus
        {
            get { double x = 0; return double.TryParse( List["FactionLeaderBonus"].ToString(), out x ) ? Convert.ToDouble( List["FactionLeaderBonus"] ) : x; }
        }

        public double ContingencyFactor
        {
            get { double x = 0; return double.TryParse( List["ContingencyFactor"].ToString(), out x ) ? Convert.ToDouble( List["ContingencyFactor"] ) : x; }
        }
        #endregion

        #region Factory Methods

        private Hashtable _list;
        internal static readonly ATConfiguration instance = new ATConfiguration();

        private ATConfiguration() 
        {
            Fetch();
        }

        public static ATConfiguration Instance
        {
            get
            {
                return instance;
            }
        }

        private Hashtable List
        {
            get
            {
                if (_list == null) Fetch();
                return _list;
            }
        }

        /// <summary>
        /// Clears the List that's been cached; do this if you update configuration information in the database
        /// </summary>
        public void InvalidateCache()
        {
            _list = null;
        }

        #endregion

        #region Data Access
        private void Fetch()
        {
            using (SqlConnection cn = new SqlConnection( Database.AphelionTriggerConnection ))
            {
                cn.Open();
                using (SqlCommand cm = cn.CreateCommand())
                {
                    cm.CommandType = CommandType.StoredProcedure;
                    cm.CommandText = "GetConfiguration";
                    using (SafeDataReader dr = new SafeDataReader( cm.ExecuteReader() ))
                    {
                        _list = new Hashtable();
                        while (dr.Read())
                        {
                            _list.Add( dr.GetString( "Key" ), dr.GetString( "Value" ) );
                        }
                    }
                }
            }
        }
        #endregion

        #region Updating Configuration Settings
        public static void UpdateConfiguration( string key, string value )
        {
            DataPortal.Execute<UpdateConfigurationCommand>( new UpdateConfigurationCommand( key, value ) );
        }

        [Serializable()]
        private class UpdateConfigurationCommand : CommandBase
        {
            private string _key;
            private string _value;

            public UpdateConfigurationCommand( string key, string value )
            {
                _key = key;
                _value = value;
            }

            public static void UpdateConfiguration( string key, string value )
            {
                UpdateConfigurationCommand command;
                command = DataPortal.Execute<UpdateConfigurationCommand>( new UpdateConfigurationCommand( key, value ) );
            }

            protected override void DataPortal_Execute()
            {
                using (SqlConnection cn = new SqlConnection( Database.AphelionTriggerConnection ))
                {
                    cn.Open();
                    using (SqlCommand cm = cn.CreateCommand())
                    {
                        cm.CommandType = CommandType.StoredProcedure;
                        cm.CommandText = "UpdateConfiguration";
                        cm.Parameters.AddWithValue( "@Key", _key );
                        cm.Parameters.AddWithValue( "@Value", _value );
                        cm.ExecuteNonQuery();
                    }
                }
            }
        }
        #endregion
    }
}
