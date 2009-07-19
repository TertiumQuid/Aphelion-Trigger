using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.ComponentModel;
using AphelionTrigger.Library.Security;
using Csla;
using Csla.Data;
using Csla.Validation;

namespace AphelionTrigger.Library
{
    [Serializable()]
    public class EspionageCommandBase : CommandBase
    {
        #region Business Methods
        public int EspionageID;
        public int Compromised = 0;

        public House OperatingHouse;
        public House TargetHouse;
        public SpyList OperatingSpies;
        public SpyList DefendingSpies;
        public EspionageOperation Operation;

        public List<string> Description;

        public double GetAppliedIntelligenceDifference( double value, double influence )
        {
            if ( OperatingHouse == null || TargetHouse == null ) return 0;

            double percentOfIntelligence = ( (double)OperatingHouse.Intelligence / (double)TargetHouse.Intelligence );
            return value * ( ( percentOfIntelligence * influence ) + ( 1 - influence ) );
        }

        public double GetAppliedSpyDifference( int operatingSpyScore, int targetSpyScore, int spiesPerPoint )
        {
            if ( OperatingSpies == null || DefendingSpies == null ) return 0;

            return ( OperatingHouse.ApplyFactionLeaderBonus( operatingSpyScore ) - TargetHouse.ApplyFactionLeaderBonus( targetSpyScore ) ) / spiesPerPoint;
        }
        #endregion

        #region Validation
        public virtual void CheckValidationRules()
        {
            if ( OperatingHouse.Credits < Operation.Cost )
                throw new Csla.Validation.ValidationException( "Insufficient credits to commit operation." );

            if ( OperatingHouse.Turns < Operation.Turns )
                throw new Csla.Validation.ValidationException( "Insufficient turns to commit operation." );

            if ( TargetHouse.Credits < 1 )
                throw new Csla.Validation.ValidationException( "Target House is bankrupt." );

            if ( OperatingSpies.Larceny < 1 )
                throw new Csla.Validation.ValidationException( "No skilled spies available for Larceny." );
        }
        #endregion

        private bool IsSpyEligibleForDetection( Spy spy )
        {
            switch ( Operation.ID )
            {
                case 1:
                    return spy.Larceny > 0;
                    break;
                case 2:
                    return spy.Surveillance > 0;
                    break;
                case 3:
                    return spy.Reconnaissance > 0;
                    break;
                case 4:
                    return spy.MICE > 0;
                    break;
                case 5:
                    return spy.Ambush > 0;
                    break;
                case 6:
                    return spy.Sabotage > 0;
                    break;
                case 7:
                    return spy.Expropriation > 0;
                    break;
                case 8:
                    return spy.Inspection > 0;
                    break;
                case 9:
                    return spy.Subversion > 0;
                    break;
                default:
                    return true;
            }

        }

        internal SortedBindingList<Spy> ApplySortForOperation( SpyList spies )
        {
            SortedBindingList<Spy> list = new Csla.SortedBindingList<Spy>( spies );

            switch ( Operation.ID )
            {
                case 1:
                    for ( int spy = 0; spy < list.Count; spy++ ) if ( list[spy].Larceny == 0 ) list.RemoveAt( spy );
                    list.ApplySort( "Larceny", ListSortDirection.Descending );
                    break;
                case 2:
                    for ( int spy = 0; spy < list.Count; spy++ ) if ( list[spy].Surveillance == 0 ) list.RemoveAt( spy );
                    list.ApplySort( "Surveillance", ListSortDirection.Descending );
                    break;
                case 3:
                    for ( int spy = 0; spy < list.Count; spy++ ) if ( list[spy].Reconnaissance == 0 ) list.RemoveAt( spy );
                    list.ApplySort( "Reconnaissance", ListSortDirection.Descending );
                    break;
                case 4:
                    for ( int spy = 0; spy < list.Count; spy++ ) if ( list[spy].MICE == 0 ) list.RemoveAt( spy );
                    list.ApplySort( "MICE", ListSortDirection.Descending );
                    break;
                case 5:
                    for ( int spy = 0; spy < list.Count; spy++ ) if ( list[spy].Ambush == 0 ) list.RemoveAt( spy );
                    list.ApplySort( "Ambush", ListSortDirection.Descending );
                    break;
                case 6:
                    for ( int spy = 0; spy < list.Count; spy++ ) if ( list[spy].Sabotage == 0 ) list.RemoveAt( spy );
                    list.ApplySort( "Sabotage", ListSortDirection.Descending );
                    break;
                case 7:
                    for ( int spy = 0; spy < list.Count; spy++ ) if ( list[spy].Expropriation == 0 ) list.RemoveAt( spy );
                    list.ApplySort( "Expropriation", ListSortDirection.Descending );
                    break;
                case 8:
                    for ( int spy = 0; spy < list.Count; spy++ ) if ( list[spy].Inspection == 0 ) list.RemoveAt( spy );
                    list.ApplySort( "Inspection", ListSortDirection.Descending );
                    break;
                case 9:
                    for ( int spy = 0; spy < list.Count; spy++ ) if ( list[spy].Subversion == 0 ) list.RemoveAt( spy );
                    list.ApplySort( "Subversion", ListSortDirection.Descending );
                    break;
            }

            return list;
        }

        internal void LogOperation( bool success, bool detection )
        {
            EspionageLog log = EspionageLog.NewEspionageLog();

            log.OperatingHouseID = OperatingHouse.ID;
            log.TargetHouseID = TargetHouse.ID;
            log.EspionageOperationID = Operation.ID;

            StringBuilder description = new StringBuilder();
            foreach ( string s in Description )
                description.AppendFormat( "{0}<br/>", s );

            log.Description = description.ToString();
            log.Success = success;
            log.Detection = detection;
            log.Compromised = Compromised;

            log.Save();

            EspionageID = log.ID;
        }

        internal void DetectEspionage()
        {
            ATConfiguration config = ATConfiguration.Instance;

            double chanceOfDetection = Operation.Detection;

            chanceOfDetection += ( DefendingSpies.CounterIntelligence / 200 );

            // houses with higher intelligence have a better chance to detect espionage
            double percentIntelligence = ( (double)OperatingHouse.Intelligence / (double)TargetHouse.Intelligence );
            chanceOfDetection = chanceOfDetection * ( ( percentIntelligence * 0.5 ) + 0.5 );

            Random random = new Random();
            int contingencyRange = (int)( config.ContingencyFactor * ( OperatingHouse.Contingency / 100 ) );
            int contingency = random.Next( -contingencyRange, contingencyRange ) / 2;

            EspionageLog log = EspionageLog.NewEspionageLog();

            chanceOfDetection += contingency;
            random = new Random();
            int num = random.Next( 1, 100 );

            if ( num <= chanceOfDetection ) // success
            {
                // a higher intelligence means better effects when detecting espionage
                int extentOfSuccess = ( TargetHouse.Intelligence - num );

                if ( extentOfSuccess >= 75 )
                {
                    // 1% more spies are discovered for each 300 pts of counter intelligence defending spies posses
                    double counterIntelligenceBonus = OperatingSpies.CounterIntelligence / 30000;

                    // base 15% casualties for a failure of 25-50
                    double casualities = 0.15 + counterIntelligenceBonus;

                    int spyCountForDetection = 0;

                    foreach ( Spy spy in OperatingSpies )
                        if ( IsSpyEligibleForDetection( spy ) ) spyCountForDetection += spy.Count;

                    int compromised = (int)( spyCountForDetection * casualities );
                    if ( compromised > spyCountForDetection ) compromised = spyCountForDetection;
                    if ( compromised == 0 ) compromised = 1;

                    Description.Add( "Your operation failed, your spies were detected by the enemy, and " + compromised.ToString() + " agents were compromised!" );

                    SortedBindingList<Spy> list = ApplySortForOperation( OperatingSpies );

                    if ( compromised > 0 )
                    {
                        using ( SqlConnection cn = new SqlConnection( Database.AphelionTriggerConnection ) )
                        {
                            cn.Open();

                            foreach ( Spy spy in list )
                            {
                                int comp = compromised > spy.Count ? spy.Count : compromised;
                                using ( SqlCommand cm = cn.CreateCommand() )
                                {
                                    cm.CommandType = CommandType.StoredProcedure;
                                    cm.CommandText = "AddCompromises";
                                    cm.Parameters.AddWithValue( "@HouseID", OperatingHouse.ID );
                                    cm.Parameters.AddWithValue( "@SpyID", spy.ID );
                                    cm.Parameters.AddWithValue( "@Compromised", comp );

                                    cm.ExecuteNonQuery();
                                }
                                compromised -= comp;
                                if ( compromised == 0 ) break;
                            }

                        }
                    }

                    Compromised = compromised;
                    LogOperation( false, true );
                }
                else if ( extentOfSuccess >= 50 )
                {
                    // 1% more spies are discovered for each 300 pts of counter intelligence defending spies posses
                    double counterIntelligenceBonus = OperatingSpies.CounterIntelligence / 30000;

                    // base 8% casualties for a failure of 25-50
                    double casualities = 0.08 + counterIntelligenceBonus;

                    int spyCountForDetection = 0;

                    foreach ( Spy spy in OperatingSpies )
                        if ( IsSpyEligibleForDetection( spy ) ) spyCountForDetection += spy.Count;

                    int compromised = (int)(spyCountForDetection * casualities);
                    if ( compromised > spyCountForDetection ) compromised = spyCountForDetection;
                    if ( compromised == 0 ) compromised = 1;

                    Description.Add( "Your operation failed, your spies were detected by the enemy, and " + compromised.ToString() + " agents were compromised!" );

                    SortedBindingList<Spy> list = ApplySortForOperation( OperatingSpies );

                    if ( compromised > 0 )
                    {
                        using ( SqlConnection cn = new SqlConnection( Database.AphelionTriggerConnection ) )
                        {
                            cn.Open();

                            foreach ( Spy spy in list )
                            {
                                int comp = compromised > spy.Count ? spy.Count : compromised;
                                using ( SqlCommand cm = cn.CreateCommand() )
                                {
                                    cm.CommandType = CommandType.StoredProcedure;
                                    cm.CommandText = "AddCompromises";
                                    cm.Parameters.AddWithValue( "@HouseID", OperatingHouse.ID );
                                    cm.Parameters.AddWithValue( "@SpyID", spy.ID );
                                    cm.Parameters.AddWithValue( "@Compromised", comp );

                                    cm.ExecuteNonQuery();
                                }
                                compromised -= comp;
                                if ( compromised == 0 ) break;
                            }

                        }
                    }

                    Compromised = compromised;
                    LogOperation( false, true );

                }
                else if ( extentOfSuccess >= 25 )
                {
                    Description.Add( "Your operation failed, but your spies managed to evade detection." );
                    LogOperation( false, true );
                }
                else
                {
                    Description.Add( "Your operation failed and your spies were detected by the enemy!" );
                    LogOperation( false, true );
                }
            }
            else
            {
                Description.Add( "Your operation failed, but your spies managed to evade detection." );
                LogOperation( false, false );
            }
        }

        internal void ApplyExperience()
        {
            ATConfiguration config = ATConfiguration.Instance;

            // only advance if user isn't already at level the level cap
            if ( OperatingHouse.Level.Rank < config.LevelCap )
            {
                Level.UpdateExperience( OperatingHouse, Operation.Experience );
                Description.Add( "You gained " + Operation.Experience.ToString() + " experience." );

                // TODO: right now this will cause weird problems if enough exp to advance more than one level is gained at once
                if ( OperatingHouse.Experience + Operation.Experience >= OperatingHouse.NextLevel.Experience )
                {
                    Description.Add( "You advanced to level " + OperatingHouse.NextLevel.Rank.ToString() + "." );

                    // add level advancement
                    Advancement advancement = Advancement.NewAdvancement();
                    advancement.HouseID = OperatingHouse.ID;
                    advancement.LevelID = OperatingHouse.NextLevel.ID;
                    advancement.Save();

                    // add leveling report
                    AphelionTrigger.Library.Report report = Report.NewReport();
                    report.FactionID = OperatingHouse.FactionID;
                    report.GuildID = OperatingHouse.GuildID;
                    report.HouseID = OperatingHouse.ID;
                    report.Message = "House " + OperatingHouse.Name + " gained a level.";
                    report.ReportLevelID = 1 + House.GetSecrecyBonus( OperatingHouse.Intelligence );
                    report.Save();
                }
            }
        }

        internal void PayForOperation()
        {
            if ( Operation == null ) return;

            // pay for performing the operation
            using ( SqlConnection cn = new SqlConnection( Database.AphelionTriggerConnection ) )
            {
                cn.Open();
                using ( SqlCommand cm = cn.CreateCommand() )
                {
                    cm.CommandType = CommandType.Text;
                    cm.CommandText = "UPDATE bbgHouses SET Credits = Credits - " + Operation.Cost.ToString() + ", Turns = Turns - " + Operation.Turns.ToString() + " WHERE ID = " + OperatingHouse.ID.ToString();
                    cm.ExecuteNonQuery();
                }
            }
        }
    }

    [Serializable()]
    public class LarcenyCommand : EspionageCommandBase
    {
        #region Factory Methods
        private LarcenyCommand( int operatingHouseId, int targetHouseId )
        {
            OperatingHouse = House.GetHouse( operatingHouseId );
            TargetHouse = House.GetHouse( targetHouseId );

            OperatingSpies = SpyList.GetAgentSchema( operatingHouseId, RecordScope.ShowNumbers );
            DefendingSpies = SpyList.GetAgentSchema( targetHouseId, RecordScope.ShowNumbers );

            Operation = EspionageOperation.GetEspionageOperation( 1 );
            Description = new List<string>();
        }

        public static void Commit( int operatingHouseId, int targetHouseId, ref int espionageID )
        {
            LarcenyCommand command;
            command = DataPortal.Execute<LarcenyCommand>( new LarcenyCommand( operatingHouseId, targetHouseId ) );
            espionageID = command.EspionageID;
        }
        #endregion

        protected override void DataPortal_Execute()
        {
            ATConfiguration config = ATConfiguration.Instance;

            double chanceOfSuccess = Operation.Success;

            // modify chance of success based on compared intelligence scores - max influence of 50%
            chanceOfSuccess = GetAppliedIntelligenceDifference( chanceOfSuccess, 0.5 );

            // modify chance of success based on reconnaissance scores
            chanceOfSuccess += GetAppliedSpyDifference( OperatingSpies.Larceny, DefendingSpies.Larceny, 300 );

            // modify chance of success based on operating house's ambition - max influence of 25%
            chanceOfSuccess = (int)OperatingHouse.ApplyAmbition( chanceOfSuccess, 0.25 );

            Random random = new Random();
            int num = random.Next( 1, 100 );

            PayForOperation();

            if ( num <= chanceOfSuccess ) // success
            {
                Description.Add( "Larceny Operation: SUCCESS" );
                Description.Add( "" );

                // if either house is a faction leader, apply the appropriate bonus
                int operatingHouseLarceny = ( OperatingSpies.Larceny * 3 );
                if ( OperatingHouse.FactionLeaderHouseID == OperatingHouse.ID ) operatingHouseLarceny += (int)( operatingHouseLarceny * config.FactionLeaderBonus );

                int targetHouseLarceny = ( DefendingSpies.Larceny * 5 );
                if ( TargetHouse.FactionLeaderHouseID == TargetHouse.ID ) targetHouseLarceny += (int)( targetHouseLarceny * config.FactionLeaderBonus );

                // calculate base credits stolen 
                int credits = ( 100 * OperatingHouse.Intelligence ) + operatingHouseLarceny;
                credits = credits - targetHouseLarceny;

                // modify credits on the basis of house contingency characteristics
                random = new Random();
                int contingencyRange = (int)( config.ContingencyFactor * ( OperatingHouse.Contingency / 100 ) );
                int contingency = random.Next( -contingencyRange, contingencyRange );

                credits += ( credits * ( contingency / 100 ) );

                // cannot steal more credits than target has
                if ( credits < 0 ) credits = 0;
                if ( credits > TargetHouse.Credits ) credits = TargetHouse.Credits;

                if ( credits > 0 )
                {
                    using ( SqlConnection cn = new SqlConnection( Database.AphelionTriggerConnection ) )
                    {
                        cn.Open();
                        using ( SqlCommand cm = cn.CreateCommand() )
                        {
                            cm.CommandType = CommandType.Text;
                            cm.CommandText = "UPDATE bbgHouses SET Credits = Credits - " + credits.ToString() + " WHERE ID = " + TargetHouse.ID.ToString();
                            cm.ExecuteNonQuery();
                        }

                        using ( SqlCommand cm = cn.CreateCommand() )
                        {
                            cm.CommandType = CommandType.Text;
                            cm.CommandText = "UPDATE bbgHouses SET Credits = Credits + " + credits.ToString() + " WHERE ID = " + OperatingHouse.ID.ToString();
                            cm.ExecuteNonQuery();
                        }
                    }
                }
                
                Description.Add( "Your larcenary garnered " + credits.ToString() + " credits." );

                ApplyExperience();

                LogOperation( true, false );

            }
            else // failure
            {
                Description.Add( "Larceny Operation: FAILURE" );
                Description.Add( "" );

                DetectEspionage();
            }
        }
    }

    [Serializable()]
    public class SurveillanceCommand : EspionageCommandBase
    {
        #region Factory Methods
        private SurveillanceCommand( int operatingHouseId, int targetHouseId )
        {
            OperatingHouse = House.GetHouse( operatingHouseId );
            TargetHouse = House.GetHouse( targetHouseId );

            OperatingSpies = SpyList.GetAgentSchema( operatingHouseId, RecordScope.ShowNumbers );
            DefendingSpies = SpyList.GetAgentSchema( targetHouseId, RecordScope.ShowNumbers );

            Operation = EspionageOperation.GetEspionageOperation( 2 );
            Description = new List<string>();
        }

        public static void Commit( int operatingHouseId, int targetHouseId, ref int espionageID )
        {
            SurveillanceCommand command;
            command = DataPortal.Execute<SurveillanceCommand>( new SurveillanceCommand( operatingHouseId, targetHouseId ) );
            espionageID = command.EspionageID;
        }
        #endregion

        protected override void DataPortal_Execute()
        {
            ATConfiguration config = ATConfiguration.Instance;

            double chanceOfSuccess = Operation.Success;

            // modify chance of success based on compared intelligence scores - max influence of 50%
            chanceOfSuccess = GetAppliedIntelligenceDifference( chanceOfSuccess, 0.5 );

            // modify chance of success based on surveillance scores
            chanceOfSuccess += GetAppliedSpyDifference( OperatingSpies.Surveillance, DefendingSpies.Surveillance, 100 );

            // modify chance of success based on operating house's ambition - max influence of 25%
            chanceOfSuccess = OperatingHouse.ApplyAmbition( chanceOfSuccess, 0.25 );

            Random random = new Random();
            int num = random.Next( 1, 100 );

            PayForOperation();

            if ( num <= chanceOfSuccess ) // success
            {
                Description.Add( "Surveillance Operation: SUCCESS");
                Description.Add( "" );

                // add a small amount of inaccuracy to the results
                int randomRange = ( 100 - OperatingHouse.Intelligence ) / 10;

                Description.Add( "Profile: House " + TargetHouse.Name );
                Description.Add( "" );

                Random statRandom = new Random();

                num = random.Next( -randomRange, randomRange );
                Description.Add( "Intelligence: " + ( TargetHouse.Intelligence + num ) );
                num = random.Next( -randomRange, randomRange );
                Description.Add( "Affluence: " + ( TargetHouse.Affluence + num ) );
                num = random.Next( -randomRange, randomRange );
                Description.Add( "Power: " + ( TargetHouse.Power + num ) );
                num = random.Next( -randomRange, randomRange );
                Description.Add( "Protection: " + ( TargetHouse.Protection + num ) );
                num = random.Next( -randomRange, randomRange );
                Description.Add( "Speed: " + ( TargetHouse.Speed + num ) );
                num = random.Next( -randomRange, randomRange );
                Description.Add( "Contingency: " + ( TargetHouse.Contingency + num ) );
                Description.Add( "" );
                num = random.Next( -randomRange, randomRange );
                Description.Add( "Ambition: " + ( TargetHouse.Ambition + num ) );
                Description.Add( "" );

                ApplyExperience();

                LogOperation( true, false );
            }
            else // failure
            {
                Description.Add( "Surveillance Operation: FAILURE" );
                Description.Add( "" );

                DetectEspionage();
            }
        }
    }

    [Serializable()]
    public class ReconnaissanceCommand : EspionageCommandBase
    {
        #region Factory Methods
        private ReconnaissanceCommand( int operatingHouseId, int targetHouseId )
        {
            OperatingHouse = House.GetHouse( operatingHouseId );
            TargetHouse = House.GetHouse( targetHouseId );

            OperatingSpies = SpyList.GetAgentSchema( operatingHouseId, RecordScope.ShowNumbers );
            DefendingSpies = SpyList.GetAgentSchema( targetHouseId, RecordScope.ShowNumbers );

            Operation = EspionageOperation.GetEspionageOperation( 3 );
            Description = new List<string>();
        }

        public static void Commit( int operatingHouseId, int targetHouseId, ref int espionageID )
        {
            ReconnaissanceCommand command;
            command = DataPortal.Execute<ReconnaissanceCommand>( new ReconnaissanceCommand( operatingHouseId, targetHouseId ) );
            espionageID = command.EspionageID;
        }
        #endregion

        protected override void DataPortal_Execute()
        {
            ATConfiguration config = ATConfiguration.Instance;

            double chanceOfSuccess = Operation.Success;

            // modify chance of success based on compared intelligence scores - max influence of 50%
            chanceOfSuccess = GetAppliedIntelligenceDifference( chanceOfSuccess, 0.5 );

            // modify chance of success based on reconnaissance scores
            chanceOfSuccess += GetAppliedSpyDifference( OperatingSpies.Reconnaissance, DefendingSpies.Reconnaissance, 100 );

            // modify chance of success based on operating house's ambition - max influence of 25%
            chanceOfSuccess = OperatingHouse.ApplyAmbition( chanceOfSuccess, 0.25 );

            Random random = new Random();
            int num = random.Next( 1, 100 );

            PayForOperation();

            if ( num <= chanceOfSuccess ) // success
            {
                Description.Add( "Reconnaissance Operation: SUCCESS" );
                Description.Add( "" );

                Description.Add( "House " + TargetHouse.Name + "'s Standing Forces" );
                Description.Add( "" );

                UnitList forces = UnitList.GetForces( TargetHouse.ID );

                foreach ( Unit unit in forces )
                {
                    Description.Add( "<strong>" + unit.Name + "</strong>" );
                    Description.Add( "" );
                    Description.Add( "Count: " + unit.Count.ToString() + " (worth " + ( unit.Count * unit.Cost ).ToString() );
                    Description.Add( "-- Attack:  " + unit.Attack.ToString() );
                    Description.Add( "-- Defense:  " + unit.Defense.ToString() );
                    Description.Add( "-- Plunder:  " + unit.Plunder.ToString() );
                    Description.Add( "-- Capture:  " + unit.Capture.ToString() );
                    Description.Add( "-- Stun:  " + unit.Stun.ToString() );
                    Description.Add( "-- Experience:  " + unit.Experience.ToString() );
                    Description.Add( "" );
                }

                ApplyExperience();

                LogOperation( true, false );
            }
            else // failure
            {
                Description.Add( "Reconnaissance Operation: FAILURE" );
                Description.Add( "" );

                DetectEspionage();
            }
        }
    }

    [Serializable()]
    public class MICECommand : EspionageCommandBase
    {
        #region Business Methods
        Spy GetRandomSpy( SpyList spies)
        {
            Random random = new Random();
            int index = random.Next( 0, spies.Count - 1 );

            return spies[index];
        }
        #endregion

        #region Factory Methods
        private MICECommand( int operatingHouseId, int targetHouseId )
        {
            OperatingHouse = House.GetHouse( operatingHouseId );
            TargetHouse = House.GetHouse( targetHouseId );

            OperatingSpies = SpyList.GetAgentSchema( operatingHouseId, RecordScope.ShowNumbers );
            DefendingSpies = SpyList.GetAgentSchema( targetHouseId, RecordScope.ShowNumbers );

            Operation = EspionageOperation.GetEspionageOperation( 4 );
            Description = new List<string>();
        }

        public static void Commit( int operatingHouseId, int targetHouseId, ref int espionageID )
        {
            MICECommand command;
            command = DataPortal.Execute<MICECommand>( new MICECommand( operatingHouseId, targetHouseId ) );
            espionageID = command.EspionageID;
        }
        #endregion

        protected override void DataPortal_Execute()
        {
            ATConfiguration config = ATConfiguration.Instance;

            double chanceOfSuccess = Operation.Success;

            // modify chance of success based on compared intelligence scores - max influence of 50%
            chanceOfSuccess = GetAppliedIntelligenceDifference( chanceOfSuccess, 0.5 );

            // modify chance of success based on MICE scores
            chanceOfSuccess += GetAppliedSpyDifference( OperatingSpies.MICE, DefendingSpies.MICE, 200 );

            // modify chance of success based on operating house's ambition - max influence of 25%
            chanceOfSuccess = (int)OperatingHouse.ApplyAmbition( chanceOfSuccess, 0.25 );

            Random random = new Random();
            int num = random.Next( 1, 100 );

            PayForOperation();

            if ( num <= chanceOfSuccess ) // success
            {
                Description.Add( "M.I.C.E. Operation: SUCCESS" );
                Description.Add( "" );

                // if either house is a faction leader, apply the appropriate bonus
                int operatingHouseMICE = (int)OperatingHouse.ApplyFactionLeaderBonus( ( (double)OperatingSpies.MICE * 3 ) );

                int targetHouseMICE = (int)TargetHouse.ApplyFactionLeaderBonus( ( (double)DefendingSpies.MICE * 5 ) );

                // calculate base spies converted 
                double percentConverted = ( operatingHouseMICE / 25 ) * 0.008;

                // modify conversions on the basis of ambition
                percentConverted = OperatingHouse.ApplyAmbition( percentConverted, 0.25 );

                int totalSpies = 0;
                foreach ( Spy spy in DefendingSpies )
                    totalSpies += spy.Count;

                // calculate how many spies are converted from the defending house
                int convertedSpies = (int)Math.Floor( totalSpies * percentConverted );

                // always convert at least one spy
                if ( convertedSpies < 0 ) convertedSpies = 1;

                using ( SqlConnection cn = new SqlConnection( Database.AphelionTriggerConnection ) )
                {
                    cn.Open();

                    // randomly select a type of spy to convert until all conversions have been performed
                    int convertedCount = 0;
                    while ( convertedCount < convertedSpies )
                    {
                        using ( SqlCommand cm = cn.CreateCommand() )
                        {
                            Spy targetSpy = GetRandomSpy( DefendingSpies );

                            int convert = ( convertedSpies - convertedCount );
                            if ( convert > targetSpy.Count ) convert = targetSpy.Count;
                            
                            cm.CommandType = CommandType.StoredProcedure;
                            cm.CommandText = "Convert";
                            cm.Parameters.AddWithValue( "@OperatingHouseID", OperatingHouse.ID );
                            cm.Parameters.AddWithValue( "@TargetHouseID", TargetHouse.ID );
                            cm.Parameters.AddWithValue( "@CapturedUnitID", targetSpy.ID );
                            cm.Parameters.AddWithValue( "@Converted", convert );
                            cm.ExecuteNonQuery();

                            Description.Add( "You converted " + convertedSpies.ToString() + " " + targetSpy.Name + "." );
                            Description.Add( "" );

                            convertedCount += convert;
                        }
                    }

                }

                ApplyExperience();

                LogOperation( true, false );

            }
            else // failure
            {
                Description.Add( "M.I.C.E. Operation: FAILURE" );
                Description.Add( "" );

                DetectEspionage();
            }
        }
    }

    [Serializable()]
    public class AmbushCommand : EspionageCommandBase
    {
        #region Business Methods
        internal SortedBindingList<Unit> GetTargetUnits()
        {
            UnitList units = UnitList.GetForces( TargetHouse.ID );
            SortedBindingList<Unit> list = new Csla.SortedBindingList<Unit>( units );
            list.ApplySort( "Cost", ListSortDirection.Descending );

            return list;
        }
        #endregion

        #region Factory Methods
        private AmbushCommand( int operatingHouseId, int targetHouseId )
        {
            OperatingHouse = House.GetHouse( operatingHouseId );
            TargetHouse = House.GetHouse( targetHouseId );

            OperatingSpies = SpyList.GetAgentSchema( operatingHouseId, RecordScope.ShowNumbers );
            DefendingSpies = SpyList.GetAgentSchema( targetHouseId, RecordScope.ShowNumbers );

            Operation = EspionageOperation.GetEspionageOperation( 5 );
            Description = new List<string>();
        }

        public static void Commit( int operatingHouseId, int targetHouseId, ref int espionageID )
        {
            AmbushCommand command;
            command = DataPortal.Execute<AmbushCommand>( new AmbushCommand( operatingHouseId, targetHouseId ) );
            espionageID = command.EspionageID;
        }
        #endregion

        protected override void DataPortal_Execute()
        {
            ATConfiguration config = ATConfiguration.Instance;

            double chanceOfSuccess = Operation.Success;

            // modify chance of success based on compared intelligence scores - max influence of 50%
            chanceOfSuccess = GetAppliedIntelligenceDifference( chanceOfSuccess, 0.5 );

            // modify chance of success based on Ambush scores
            chanceOfSuccess += GetAppliedSpyDifference( OperatingSpies.Ambush, DefendingSpies.Ambush, 50 );

            // modify chance of success based on operating house's ambition - max influence of 25%
            chanceOfSuccess = (int)OperatingHouse.ApplyAmbition( chanceOfSuccess, 0.25 );

            Random random = new Random();
            int num = random.Next( 1, 100 );

            PayForOperation();

            if ( num <= chanceOfSuccess ) // success
            {
                Description.Add( "Ambush Operation: SUCCESS" );
                Description.Add( "" );

                //always apply faction bonus to ambush score
                int operatingHouseAmbush = (int)OperatingHouse.ApplyFactionLeaderBonus( OperatingSpies.Ambush );

                // calculate base units killed
                double percentKilled = ( operatingHouseAmbush / 25 ) * 0.005;

                // modify kills on the basis of ambition
                percentKilled = OperatingHouse.ApplyAmbition( percentKilled, 0.25 );

                UnitList units = UnitList.GetForces( TargetHouse.ID );
                SortedBindingList<Unit> list = new Csla.SortedBindingList<Unit>( units );
                list.ApplySort( "Cost", ListSortDirection.Descending );

                int totalUnits = 0;
                foreach ( Unit unit in units )
                    totalUnits += unit.Count;

                // calculate how many units are killed
                int murderedUnits = (int)Math.Ceiling( totalUnits * percentKilled );

                // always convert at least one spy
                if ( murderedUnits < 0 ) murderedUnits = 1;

                using ( SqlConnection cn = new SqlConnection( Database.AphelionTriggerConnection ) )
                {
                    cn.Open();

                    // randomly select a type of spy to convert until all conversions have been performed
                    int killCount = 0;
                    foreach ( Unit unit in units )
                    {
                        int killed = ( murderedUnits - killCount );
                        if ( killed > unit.Count ) killed = unit.Count;

                        if ( killed > 0 )
                        {
                            using ( SqlCommand cm = cn.CreateCommand() )
                            {
                                cm.CommandType = CommandType.StoredProcedure;
                                cm.CommandText = "AddCasualties";
                                cm.Parameters.AddWithValue( "@HouseID", TargetHouse.ID );
                                cm.Parameters.AddWithValue( "@UnitID", unit.ID );
                                cm.Parameters.AddWithValue( "@Casualties", killed );
                                cm.ExecuteNonQuery();
                            }

                            Description.Add( "You ambushed and murdered " + killed.ToString() + " " + unit.Name + "." );
                            Description.Add( "" );

                            killCount += killed;
                        }
                    }
                }

                ApplyExperience();

                LogOperation( true, false );

            }
            else // failure
            {
                Description.Add( "Ambush Operation: FAILURE" );
                Description.Add( "" );

                DetectEspionage();
            }

        }
    }

    [Serializable()]
    public class SabotageCommand : EspionageCommandBase
    {
        #region Factory Methods
        private SabotageCommand( int operatingHouseId, int targetHouseId )
        {
            OperatingHouse = House.GetHouse( operatingHouseId );
            TargetHouse = House.GetHouse( targetHouseId );

            OperatingSpies = SpyList.GetAgentSchema( operatingHouseId, RecordScope.ShowNumbers );
            DefendingSpies = SpyList.GetAgentSchema( targetHouseId, RecordScope.ShowNumbers );

            Operation = EspionageOperation.GetEspionageOperation( 6 );
            Description = new List<string>();
        }

        public static void Commit( int operatingHouseId, int targetHouseId, ref int espionageID )
        {
            SabotageCommand command;
            command = DataPortal.Execute<SabotageCommand>( new SabotageCommand( operatingHouseId, targetHouseId ) );
            espionageID = command.EspionageID;
        }
        #endregion

        protected override void DataPortal_Execute()
        {
            ATConfiguration config = ATConfiguration.Instance;

            double chanceOfSuccess = Operation.Success;

            // modify chance of success based on compared intelligence scores - max influence of 50%
            chanceOfSuccess = GetAppliedIntelligenceDifference( chanceOfSuccess, 0.5 );

            // modify chance of success based on surveillance scores
            chanceOfSuccess += GetAppliedSpyDifference( OperatingSpies.Surveillance, DefendingSpies.Surveillance, 100 );

            // modify chance of success based on operating house's ambition - max influence of 25%
            chanceOfSuccess = OperatingHouse.ApplyAmbition( chanceOfSuccess, 0.25 );

            Random random = new Random();
            int num = random.Next( 1, 100 );

            PayForOperation();

            int techCount = 0;
            if ( num <= chanceOfSuccess ) // success
            {
                Description.Add( "Sabotage Operation: SUCCESS" );
                Description.Add( "" );
                
                using (SqlConnection cn = new SqlConnection(Database.AphelionTriggerConnection))
                {
                    cn.Open();
                    using (SqlCommand cm = cn.CreateCommand())
                    {
                        cm.CommandType = CommandType.Text;
                        cm.CommandText = "SELECT COUNT(*) FROM bbgResearch WHERE ResearchStateID <> 4 AND HouseID = " + TargetHouse.ID.ToString() + "; UPDATE bbgResearch SET CreditsSpent = 0, TurnsSpent = 0, CreditCount = 0, TurnCount = 0 WHERE ResearchStateID <> 4 AND HouseID = " + TargetHouse.ID.ToString();
                        techCount = Convert.ToInt32( cm.ExecuteScalar() );
                    }
                }

                if ( techCount == 0 )
                {
                    Description.Add( "Your efforts were squandered. House " + TargetHouse.Name + " was not currently engaged in any research." );
                    Description.Add( "" );
                }
                else
                {
                    Description.Add( "You ruined House " + TargetHouse.Name + "'s research on a total of " + techCount.ToString() + " technolog" + ( techCount > 1 ? "ies." : "y." ) );
                    Description.Add( "" );

                    ApplyExperience();
                }
                

                LogOperation( true, false );
            }
            else // failure
            {
                Description.Add( "Surveillance Operation: FAILURE" );
                Description.Add( "" );

                DetectEspionage();
            }
        }
    }

    [Serializable()]
    public class ExpropriationCommand : EspionageCommandBase
    {
        #region Factory Methods
        private ExpropriationCommand( int operatingHouseId, int targetHouseId )
        {
            OperatingHouse = House.GetHouse( operatingHouseId );
            TargetHouse = House.GetHouse( targetHouseId );

            OperatingSpies = SpyList.GetAgentSchema( operatingHouseId, RecordScope.ShowNumbers );
            DefendingSpies = SpyList.GetAgentSchema( targetHouseId, RecordScope.ShowNumbers );

            Operation = EspionageOperation.GetEspionageOperation( 7 );
            Description = new List<string>();

            // validate command
            CheckValidationRules();
        }

        public static void Commit( int operatingHouseId, int targetHouseId, ref int espionageID )
        {
            ExpropriationCommand command;
            command = DataPortal.Execute<ExpropriationCommand>( new ExpropriationCommand( operatingHouseId, targetHouseId ) );
            espionageID = command.EspionageID;
        }
        #endregion

        #region Validation
        public override void CheckValidationRules()
        {
            base.CheckValidationRules();

            if ( TargetHouse.FactionID != OperatingHouse.FactionID )
                throw new Csla.Validation.ValidationException( "Target house must serve the same faction in order to expropriate technologicy." );

        }
        #endregion

        protected override void DataPortal_Execute()
        {
            ATConfiguration config = ATConfiguration.Instance;

            double chanceOfSuccess = Operation.Success;

            // modify chance of success based on compared intelligence scores - max influence of 50%
            chanceOfSuccess = GetAppliedIntelligenceDifference( chanceOfSuccess, 0.5 );

            // modify chance of success based on surveillance scores
            chanceOfSuccess += GetAppliedSpyDifference( OperatingSpies.Expropriation, DefendingSpies.Expropriation, 200 );

            // modify chance of success based on operating house's ambition - max influence of 25%
            chanceOfSuccess = OperatingHouse.ApplyAmbition( chanceOfSuccess, 0.25 );

            Random random = new Random();
            int num = random.Next( 1, 100 );

            PayForOperation();

            int techCount = 0;
            if ( num <= chanceOfSuccess ) // success
            {
                Description.Add( "Expropriation Operation: SUCCESS" );
                Description.Add( "" );

                TechnologyList operatingTech = TechnologyList.GetResearchedTechnologies( OperatingHouse.ID ); 
                TechnologyList targetTech = TechnologyList.GetResearchedTechnologies( TargetHouse.ID );

                SortedBindingList<Technology> list = new Csla.SortedBindingList<Technology>( targetTech );
                list.ApplySort( "Cost", ListSortDirection.Ascending );

                // find the first technology the target house has that the operating house doesn't,
                // starting with the cheapest technology first
                int technologyId = 0;
                string technologyName = string.Empty;
                foreach ( Technology target in list )
                {
                    bool alreadyHasTech = false;
                    foreach ( Technology tech in operatingTech )
                    {
                        if ( tech.ID == target.ID )
                        {
                            alreadyHasTech = true;
                            break;
                        }

                        if ( !alreadyHasTech )
                        {
                            technologyId = target.ID;
                            technologyName = target.Name;
                            break;
                        }
                    }
                }

                if ( technologyId == 0 )
                {
                    Description.Add( "Your efforts were squandered. House " + TargetHouse.Name + " does not possess any technology unknown to you." );
                    Description.Add( "" );
                }
                else
                {

                    using ( SqlConnection cn = new SqlConnection( Database.AphelionTriggerConnection ) )
                    {
                        cn.Open();
                        using ( SqlCommand cm = cn.CreateCommand() )
                        {
                            cm.CommandType = CommandType.Text;
                            cm.CommandText = "INSERT INTO bbgResearch (HouseId,TechnologyID,ResearchStateID,AgeID) VALUES (" + OperatingHouse.ID.ToString() + "," + technologyId.ToString() + ",4,dbo.GetAge())";
                            techCount = Convert.ToInt32( cm.ExecuteScalar() );
                        }
                    }

                    Description.Add( "You expropriated the research for " + technologyName + ", making the technology available to your house." );
                    Description.Add( "" );

                    ApplyExperience();
                }

                LogOperation( true, false );
            }
            else // failure
            {
                Description.Add( "Expropriation Operation: FAILURE" );
                Description.Add( "" );

                DetectEspionage();
            }
        }
    }

    [Serializable()]
    public class InspectionCommand : EspionageCommandBase
    {
        #region Factory Methods
        private InspectionCommand( int operatingHouseId, int targetHouseId )
        {
            OperatingHouse = House.GetHouse( operatingHouseId );
            TargetHouse = House.GetHouse( targetHouseId );

            OperatingSpies = SpyList.GetAgentSchema( operatingHouseId, RecordScope.ShowNumbers );
            DefendingSpies = SpyList.GetAgentSchema( targetHouseId, RecordScope.ShowNumbers );

            Operation = EspionageOperation.GetEspionageOperation( 8 );
            Description = new List<string>();
        }

        public static void Commit( int operatingHouseId, int targetHouseId, ref int espionageID )
        {
            InspectionCommand command;
            command = DataPortal.Execute<InspectionCommand>( new InspectionCommand( operatingHouseId, targetHouseId ) );
            espionageID = command.EspionageID;
        }
        #endregion

        protected override void DataPortal_Execute()
        {
            ATConfiguration config = ATConfiguration.Instance;

            double chanceOfSuccess = Operation.Success;

            // modify chance of success based on compared intelligence scores - max influence of 50%
            chanceOfSuccess = GetAppliedIntelligenceDifference( chanceOfSuccess, 0.5 );

            // modify chance of success based on reconnaissance scores
            chanceOfSuccess += GetAppliedSpyDifference( OperatingSpies.Reconnaissance, DefendingSpies.Reconnaissance, 100 );

            // modify chance of success based on operating house's ambition - max influence of 25%
            chanceOfSuccess = OperatingHouse.ApplyAmbition( chanceOfSuccess, 0.25 );

            Random random = new Random();
            int num = random.Next( 1, 100 );

            PayForOperation();

            if ( num <= chanceOfSuccess ) // success
            {
                Description.Add( "Inspection Operation: SUCCESS" );
                Description.Add( "" );

                Description.Add( "House " + TargetHouse.Name + "'s Research Profile" );
                Description.Add( "" );

                TechnologyList technologies = TechnologyList.GetTechnologies( TargetHouse.ID );

                foreach ( Technology tech in technologies )
                {
                    if ( tech.ResearchStateID > 0 )
                    {
                        Description.Add( "<strong>" + tech.Name + "</strong>" );
                        Description.Add( "" );
                        Description.Add( "Status: " + tech.ResearchState );
                        if ( tech.Attack > 0 ) Description.Add( "-- Attack:  " + tech.Attack.ToString() );
                        if ( tech.Defense > 0 ) Description.Add( "-- Defense:  " + tech.Defense.ToString() );
                        if ( tech.Plunder > 0 ) Description.Add( "-- Plunder:  " + tech.Plunder.ToString() );
                        if ( tech.Capture > 0 ) Description.Add( "-- Capture:  " + tech.Capture.ToString() );
                        if ( tech.Stun > 0 ) Description.Add( "-- Stun:  " + tech.Stun.ToString() );
                        if ( tech.Experience > 0 ) Description.Add( "-- Experience:  " + tech.Experience.ToString() );
                        Description.Add( "" );
                    }
                }

                ApplyExperience();

                LogOperation( true, false );
            }
            else // failure
            {
                Description.Add( "Inspection Operation: FAILURE" );
                Description.Add( "" );

                DetectEspionage();
            }
        }
    }

    [Serializable()]
    public class SubversionCommand : EspionageCommandBase
    {
        #region Factory Methods
        private SubversionCommand( int operatingHouseId, int targetHouseId )
        {
            OperatingHouse = House.GetHouse( operatingHouseId );
            TargetHouse = House.GetHouse( targetHouseId );

            OperatingSpies = SpyList.GetAgentSchema( operatingHouseId, RecordScope.ShowNumbers );
            DefendingSpies = SpyList.GetAgentSchema( targetHouseId, RecordScope.ShowNumbers );

            Operation = EspionageOperation.GetEspionageOperation( 9 );
            Description = new List<string>();
        }

        public static void Commit( int operatingHouseId, int targetHouseId, ref int espionageID )
        {
            SubversionCommand command;
            command = DataPortal.Execute<SubversionCommand>( new SubversionCommand( operatingHouseId, targetHouseId ) );
            espionageID = command.EspionageID;
        }
        #endregion

        protected override void DataPortal_Execute()
        {
            ATConfiguration config = ATConfiguration.Instance;

            double chanceOfSuccess = Operation.Success;

            // modify chance of success based on compared intelligence scores - max influence of 50%
            chanceOfSuccess = GetAppliedIntelligenceDifference( chanceOfSuccess, 0.5 );

            // modify chance of success based on subversion scores
            chanceOfSuccess += GetAppliedSpyDifference( OperatingSpies.Subversion, DefendingSpies.Subversion, 100 );

            // modify chance of success based on operating house's ambition - max influence of 25%
            chanceOfSuccess = OperatingHouse.ApplyAmbition( chanceOfSuccess, 0.25 );

            Random random = new Random();
            int num = random.Next( 1, 100 );

            PayForOperation();

            if ( num <= chanceOfSuccess ) // success
            {
                Description.Add( "Subversion Operation: SUCCESS" );
                Description.Add( "" );
                
                // if either house is a faction leader, apply the appropriate bonus
                int operatingHouseSubversion = (int)OperatingHouse.ApplyFactionLeaderBonus( ( (double)OperatingSpies.Subversion * 3 ) );

                int targetHouseSubversion = (int)TargetHouse.ApplyFactionLeaderBonus( ( (double)DefendingSpies.Subversion * 5 ) );

                // calculate base turns lost
                int turns = ( OperatingHouse.Intelligence / 10 ) - ( TargetHouse.Intelligence / 10 );
                turns = turns + ( operatingHouseSubversion - targetHouseSubversion );

                // vastly limit turn loss
                turns = turns / 500;

                // cannot loose more than 10% of turns, and always looses at least 1 turn
                if ( turns > ( TargetHouse.Turns / 10 ) ) turns = ( TargetHouse.Turns / 10 );
                if ( turns < 1 ) turns = 1;

                using ( SqlConnection cn = new SqlConnection( Database.AphelionTriggerConnection ) )
                {
                    cn.Open();
                    using ( SqlCommand cm = cn.CreateCommand() )
                    {
                        cm.CommandType = CommandType.Text;
                        cm.CommandText = "UPDATE bbgHouses SET Turns = " + turns.ToString() + ", TurnCount = 0";
                        cm.ExecuteNonQuery();
                    }
                }

                Description.Add( "Your agents subverted " + turns.ToString() + " turns of House " + TargetHouse.Name );
                Description.Add( "" );
            
                ApplyExperience();

                LogOperation( true, false );
            }
            else // failure
            {
                Description.Add( "Subversion Operation: FAILURE" );
                Description.Add( "" );

                DetectEspionage();
            }
        }
    }
}
