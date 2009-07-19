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
    public class AttackCommand : CommandBase
    {
        #region Business Methods
        private int _attackerHouseId;
        private int _defenderHouseId;
        public int AttackID;

        private House _attackerHouse;
        private House _defenderHouse;

        private UnitList _attackerForces;
        private UnitList _defenderForces;

        private TechnologyList _attackerTechnologies;
        private TechnologyList _defenderTechnologies;

        private List<string> _description;
        #endregion

        private AttackCommand( int attackerHouseId, int defenderHouseId )
        {
            _attackerHouseId = attackerHouseId;
            _defenderHouseId = defenderHouseId;
            _description = new List<string>();
        }

        public static void Attack( int attackerHouseId, int defenderHouseId, ref int AttackID )
        {
            AttackCommand command;
            command = DataPortal.Execute<AttackCommand>( new AttackCommand( attackerHouseId, defenderHouseId ) );
            AttackID = command.AttackID;
        }

        protected override void DataPortal_Execute()
        {
            // initialize business objects for population

            _attackerHouse = House.NewHouse();
            _defenderHouse = House.NewHouse();

            _attackerForces = UnitList.NewUnitList();
            _defenderForces = UnitList.NewUnitList();

            _attackerTechnologies = TechnologyList.NewTechnologyList();
            _defenderTechnologies = TechnologyList.NewTechnologyList();

            _attackerForces.ApplyTechnologies( _attackerTechnologies );
            _defenderForces.ApplyTechnologies( _defenderTechnologies );

            BindAttackData();
            CalculateAttack();
            SaveAttack();
        }

        private void CalculateAttack()
        {
            ATConfiguration config = ATConfiguration.Instance;

            #region 1. Calculate stat totals
            int attackerTotalAttack = 0;
            foreach (Unit unit in _attackerForces)
            {
                attackerTotalAttack += ((unit.Attack + unit.AttackTech) * unit.Count);
            }
            int attackerTotalDefense = 0;
            foreach (Unit unit in _attackerForces)
            {
                attackerTotalDefense += ((unit.Defense + unit.DefenseTech) * unit.Count);
            }
            int attackerTotalUnitDefense = 0;
            foreach (Unit unit in _attackerForces)
            {
                attackerTotalUnitDefense += (unit.Defense + unit.DefenseTech);
            }

            int defenderTotalAttack = 0;
            foreach (Unit unit in _defenderForces)
            {
                defenderTotalAttack += ((unit.Attack + unit.AttackTech) * unit.Count);
            }
            int defenderTotalDefense = 0;
            foreach (Unit unit in _defenderForces)
            {
                defenderTotalDefense += ((unit.Defense + unit.DefenseTech) * unit.Count);
            }
            int defenderTotalUnitDefense = 0;
            foreach (Unit unit in _defenderForces)
            {
                defenderTotalUnitDefense += (unit.Defense + unit.DefenseTech);
            }

            // modify attack and defense totals according to the individual power and protection of the houses
            // note: each point of a stat improves attack/defense by 0.005%
            attackerTotalAttack = (int)(attackerTotalAttack * ((_attackerHouse.Power / 200) + 0.5));
            attackerTotalDefense = (int)(attackerTotalDefense * ((_attackerHouse.Defense / 200) + 0.5));
            defenderTotalAttack = (int)(defenderTotalAttack * ((_defenderHouse.Power / 200) + 0.5));
            defenderTotalDefense = (int)(defenderTotalDefense * ((_defenderHouse.Defense / 200) + 0.5));

            // if either house is a faction leader, apply the appropriate bonus
            if ( _attackerHouse.FactionLeaderHouseID == _attackerHouse.ID )
            {
                attackerTotalAttack += Convert.ToInt32( attackerTotalAttack * config.FactionLeaderBonus );
                attackerTotalDefense += Convert.ToInt32( attackerTotalDefense * config.FactionLeaderBonus );
            }
            if ( _defenderHouse.FactionLeaderHouseID == _defenderHouse.ID )
            {
                defenderTotalAttack += Convert.ToInt32( defenderTotalAttack * config.FactionLeaderBonus );
                defenderTotalDefense += Convert.ToInt32( defenderTotalDefense * config.FactionLeaderBonus );
            }

            // modify stat totals by the respective house ambitions
            // note: each point of ambition improves scores by 0.0025%, i.e. a house can contribute up to a 25% modifier through ambition
            attackerTotalAttack = (int)( attackerTotalAttack * ( ( _attackerHouse.Ambition * 0.25 ) + 0.75 ) );
            attackerTotalDefense = (int)( attackerTotalDefense * ( ( _attackerHouse.Ambition * 0.25 ) + 0.75 ) );
            defenderTotalAttack = (int)( defenderTotalAttack * ( ( _defenderHouse.Ambition * 0.25 ) + 0.75 ) );
            defenderTotalDefense = (int)( defenderTotalDefense * ( ( _defenderHouse.Ambition * 0.25 ) + 0.75 ) );

            // modify stat totals on the basis of house contingency characteristics
            Random random = new Random();
            int attackerContingencyRange = (int)( config.ContingencyFactor * ( _attackerHouse.Contingency / 100 ) );
            int defenderContingencyRange = (int)( config.ContingencyFactor * ( _defenderHouse.Contingency / 100 ) );
            int attackerContingency = random.Next( -attackerContingencyRange, attackerContingencyRange );
            int defenderContingency = random.Next( -defenderContingencyRange, defenderContingencyRange );

            attackerTotalAttack += ( attackerTotalAttack * ( attackerContingency / 100 ) );
            attackerTotalDefense += ( attackerTotalDefense * ( attackerContingency / 100 ) );
            defenderTotalAttack += ( defenderTotalAttack * ( defenderContingency / 100 ) );
            defenderTotalDefense += ( defenderTotalDefense * ( defenderContingency / 100 ) );
            #endregion

            // only calculate casualties if defender has forces and attacker has attack
            if (_defenderHouse.ForcesCount > 0 && attackerTotalAttack > 0)
            {
                #region 2. Calculate defender casualties

                // what percent is the attacker's attack of the defender's defense?
                double percentAttackOfDefense = defenderTotalDefense > 0 ? ((double)attackerTotalAttack / (double)defenderTotalDefense) * 100 : 100;

                // convert the above value to a percentage representing casualities of the defender's forces - multiply by a constant to make losses reasonable
                double percentCasualties = ( (double)percentAttackOfDefense * config.CasualtyFactor );

                // get the actual number for the casualties of the defender's forces
                int casualtiesCount = (int)(percentCasualties * _defenderHouse.ForcesCount);

                // cannot kill more units than the defender has
                if (casualtiesCount > _defenderHouse.ForcesCount) casualtiesCount = _defenderHouse.ForcesCount;

                // calculate the odds a unit will die against the odds other units will die
                int[] percentChanceUnitDies = PercentChanceDefenderUnitDies( defenderTotalUnitDefense );

                // randomize each unit death to see which type of unit dies, then store the casualty tally for later 
                for (int i = 0; i < casualtiesCount; i++)
                {
                    random = new Random();
                    int num = random.Next( 1, 100 );

                    int lastPercentRange = 0;

                    for (int j = 0; j < _defenderForces.Count; j++)
                    {
                        if ( ( percentChanceUnitDies[j] > 0 ) && ( num > lastPercentRange) && ( num <= percentChanceUnitDies[j] ) )
                        {
                            _defenderForces[j].Casualties++;

                            // if all units of a given type are killed, make sure they are excluded from future calculations
                            if (_defenderForces[j].Casualties == _defenderForces.Count)
                            {
                                defenderTotalUnitDefense = 0;
                                foreach (Unit unit in _defenderForces)
                                {
                                    if (unit.Casualties < unit.Count) defenderTotalUnitDefense += (unit.Defense + unit.DefenseTech);
                                }
                                percentChanceUnitDies = PercentChanceDefenderUnitDies( defenderTotalUnitDefense );
                            }

                            break;
                        }
                        lastPercentRange = percentChanceUnitDies[j];
                    }
                }
                #endregion

                #region 3. Calculate attacker casualties
                // calculate attacker casualties - this is mirror logic of the above, with attacker and defender duly swapped

                if (defenderTotalAttack > 0)
                {
                    percentAttackOfDefense = attackerTotalDefense > 0 ? ((double)defenderTotalAttack / (double)attackerTotalDefense) * 100 : 100;

                    percentCasualties = ( (double)percentAttackOfDefense * config.CasualtyFactor );

                    casualtiesCount = (int)(percentCasualties * _attackerHouse.ForcesCount);

                    // cannot kill more units than the attacker has
                    if (casualtiesCount > _attackerHouse.ForcesCount) casualtiesCount = _attackerHouse.ForcesCount;

                    percentChanceUnitDies = PercentChancAttackerUnitDies( attackerTotalUnitDefense );

                    for (int i = 0; i < casualtiesCount; i++)
                    {
                        random = new Random();
                        int num = random.Next( 1, 100 );

                        int lastPercentRange = 0;

                        for (int j = 0; j < _attackerForces.Count; j++)
                        {
                            if ((percentChanceUnitDies[j] > 0) && (num > lastPercentRange) && (num <= percentChanceUnitDies[j]))
                            {
                                _attackerForces[j].Casualties++;

                                if (_attackerForces[j].Casualties == _attackerForces.Count)
                                {
                                    percentChanceUnitDies = PercentChancAttackerUnitDies( attackerTotalUnitDefense );
                                }

                                break;
                            }
                            lastPercentRange = percentChanceUnitDies[j];
                        }
                    }
                }
                #endregion
            }
        }

        private void SaveAttack()
        {
            using (SqlConnection cn = new SqlConnection( Database.AphelionTriggerConnection ))
            {
                cn.Open();

                ATConfiguration config = ATConfiguration.Instance;

                #region 4. Save Casualties
                foreach (Unit unit in _defenderForces)
                {
                    if (unit.Casualties > 0)
                    {
                        using (SqlCommand cm = cn.CreateCommand())
                        {
                            cm.CommandType = CommandType.StoredProcedure;
                            cm.CommandText = "AddCasualties";
                            cm.Parameters.AddWithValue( "@HouseID", _defenderHouse.ID );
                            cm.Parameters.AddWithValue( "@UnitID", unit.ID );
                            cm.Parameters.AddWithValue( "@Casualties", unit.Casualties );
                            cm.ExecuteNonQuery();

                            _description.Add( "You killed " + unit.Casualties.ToString() + " " + (unit.Casualties > 1 ? Pluralize( unit.Name ) : unit.Name) + "." );
                        }
                    }
                }

                foreach (Unit unit in _attackerForces)
                {
                    if (unit.Casualties > 0)
                    {
                        using (SqlCommand cm = cn.CreateCommand())
                        {
                            cm.CommandType = CommandType.StoredProcedure;
                            cm.CommandText = "AddCasualties";
                            cm.Parameters.AddWithValue( "@HouseID", _attackerHouse.ID );
                            cm.Parameters.AddWithValue( "@UnitID", unit.ID );
                            cm.Parameters.AddWithValue( "@Casualties", unit.Casualties );
                            cm.ExecuteNonQuery();

                            _description.Add( "You lost " + unit.Casualties.ToString() + " " + (unit.Casualties > 1 ? Pluralize( unit.Name ) : unit.Name) + "." );
                        }
                    }
                }
                #endregion

                #region 5. Calculate plundered credits

                int totalPlunder = 0;
                int plunder = 0;

                foreach (Unit unit in _attackerForces)
                {
                    // subtract casualties to make sure only surviving units plunder
                    totalPlunder += ((unit.Plunder + unit.PlunderTech) * (unit.Count - unit.Casualties));
                }

                // only calculate plunder if necessary
                if ( totalPlunder > 0 )
                {
                    // if attacker is a faction leader, add appropirate bonus
                    if ( _attackerHouse.FactionLeaderHouseID == _attackerHouse.ID ) { totalPlunder += Convert.ToInt32( totalPlunder * config.FactionLeaderBonus ); }

                    // modify stat totals on the basis of house contingency characteristics
                    Random plunderRandom = new Random();
                    int attackerContingencyRange = (int)( config.ContingencyFactor * ( _attackerHouse.Contingency / 100 ) );
                    int attackerContingency = plunderRandom.Next( -attackerContingencyRange, attackerContingencyRange );
                    totalPlunder += ( totalPlunder * ( attackerContingency / 100 ) );

                    double percentPlundered = ( (double)totalPlunder / 40 ) * 0.01;

                    // modify total plunder total by the attacker house's ambition
                    percentPlundered = ( percentPlundered * ( ( _attackerHouse.Ambition * 0.0025 ) + 0.75 ) );

                    // cap the ammount of credits that can be plundered at 10%
                    if ( percentPlundered > 0.1 ) percentPlundered = 0.1;

                    using ( SqlCommand cm = cn.CreateCommand() )
                    {
                        cm.CommandType = CommandType.StoredProcedure;
                        cm.CommandText = "Plunder";
                        cm.Parameters.AddWithValue( "@PlundererHouseID", _attackerHouse.ID );
                        cm.Parameters.AddWithValue( "@PlunderedHouseID", _defenderHouse.ID );
                        cm.Parameters.AddWithValue( "@PlunderPercent", percentPlundered );
                        SqlParameter param = new SqlParameter( "@Plunder", SqlDbType.Int );
                        param.Direction = ParameterDirection.Output;
                        cm.Parameters.Add( param );
                        cm.ExecuteNonQuery();

                        plunder = (int)cm.Parameters["@Plunder"].Value;
                    }

                    _description.Add( "You seized " + plunder.ToString() + " credits." );
                }

                #endregion

                #region 6. Calculate captured militia

                int totalCapture = 0;
                int totalCaptured = 0;

                foreach (Unit unit in _attackerForces)
                {
                    // subtract casualties to make sure only surviving units capture
                    totalCapture += ((unit.Capture + unit.CaptureTech) * (unit.Count - unit.Casualties));
                }

                // only calculate capture if necessary
                if ( totalCapture > 0 )
                {
                    // if attacker is a faction leader, add appropirate bonus
                    totalCapture = (int)_attackerHouse.ApplyFactionLeaderBonus( totalCapture );

                    // modify stat totals on the basis of house contingency characteristics
                    Random captureRandom = new Random();
                    int attackerContingencyRange = (int)( config.ContingencyFactor * ( _attackerHouse.Contingency / 100 ) );
                    int attackerContingency = captureRandom.Next( -attackerContingencyRange, attackerContingencyRange );
                    totalCapture += ( totalCapture * ( attackerContingency / 100 ) );

                    double percentCaptured = ( (double)totalCapture / config.CaptureDivisor ) * config.CaptureFactor;

                    // finally, modify total capture total by the attacker house's ambition
                    percentCaptured = (int)( percentCaptured * ( ( _attackerHouse.Ambition * 0.0025 ) + 0.75 ) );

                    // cap the ammount of militia that can be captured at CaptureCap
                    if ( percentCaptured > config.CaptureCap ) percentCaptured = config.CaptureCap;

                    int totalCaptureableUnits = 0;
                    foreach ( Unit unit in _defenderForces )
                    {
                        // substract casualties to make sure only surviving units are capture
                        if ( unit.UnitClassID == 1 ) totalCaptureableUnits += ( unit.Count - unit.Casualties );
                    }

                    int captureCount = (int)( percentCaptured * totalCaptureableUnits );

                    // ensure that if any percentage of units is to be captured, at least one will always be captured
                    if ( percentCaptured > 0 && captureCount == 0 ) captureCount = 1;

                    SortedBindingList<Unit> sortedList = new SortedBindingList<Unit>( _defenderForces );
                    sortedList.ApplySort( "Cost", ListSortDirection.Ascending );

                    int cheapestMilitiaId = GetCheapestMilitiaID( _attackerForces );

                    // go through defender forces until all captures are made
                    foreach ( Unit unit in sortedList )
                    {
                        // skip non-militia
                        if ( unit.UnitClassID != 1 ) continue;

                        // capture as many units as possible each iteration of the loop
                        int captured = captureCount;
                        if ( captured > unit.Count - unit.Casualties ) captured = ( unit.Count - unit.Casualties );

                        using ( SqlCommand cm = cn.CreateCommand() )
                        {
                            cm.CommandType = CommandType.StoredProcedure;
                            cm.CommandText = "Capture";
                            cm.Parameters.AddWithValue( "@CapturerHouseID", _attackerHouse.ID );
                            cm.Parameters.AddWithValue( "@CapturedHouseID", _defenderHouse.ID );
                            cm.Parameters.AddWithValue( "@CapturedUnitID", unit.ID );
                            cm.Parameters.AddWithValue( "@Captured", captured );
                            cm.ExecuteNonQuery();

                            if ( captured > 0 ) _description.Add( "You captured " + captured.ToString() + " " + unit.Name + "." );
                            captureCount -= captured;
                            totalCaptured += captured;
                        }
                        if ( captureCount <= 0 ) break;
                    }
                }

                #endregion

                #region 7. Calculate Stun

                int totalStun = 0;
                int stun = 0;
                
                foreach ( Unit unit in _attackerForces )
                {
                    // subtract casualties to make sure only surviving units stun
                    totalStun += ( ( unit.Stun + unit.StunTech ) * ( unit.Count - unit.Casualties ) );
                }

                // only calculate stun if necessary
                if ( totalStun > 0 )
                {
                    // if attacker is a faction leader, add appropirate bonus
                    if ( _attackerHouse.FactionLeaderHouseID == _attackerHouse.ID ) { totalStun += Convert.ToInt32( totalStun * config.FactionLeaderBonus ); }

                    // modify stat totals on the basis of house contingency characteristics
                    Random stunRandom = new Random();
                    int attackerContingencyRange = (int)( config.ContingencyFactor * ( _attackerHouse.Contingency / 100 ) );
                    int attackerContingency = stunRandom.Next( -attackerContingencyRange, attackerContingencyRange );
                    totalStun += ( totalStun * ( attackerContingency / 100 ) );

                    double percentStunned = ( (double)totalStun / 40 ) * 0.001;

                    // cap the ammount of turns that can be stunned at 8%
                    if ( percentStunned > 0.08 ) percentStunned = 0.08;

                    using ( SqlCommand cm = cn.CreateCommand() )
                    {
                        cm.CommandType = CommandType.StoredProcedure;
                        cm.CommandText = "Stun";
                        cm.Parameters.AddWithValue( "@HouseID", _defenderHouse.ID );
                        cm.Parameters.AddWithValue( "@StunPercent", percentStunned );
                        SqlParameter param = new SqlParameter( "@Stun", SqlDbType.Int );
                        param.Direction = ParameterDirection.Output;
                        cm.Parameters.Add( param );
                        cm.ExecuteNonQuery();

                        stun = (int)cm.Parameters["@Stun"].Value;
                    }

                    _description.Add( "You stunned your enemy for " + stun.ToString() + " turns." );
                }
                #endregion

                #region 8. Update Ambition
                // the attacker gains or looses ambition based on how much of a percentage lower
                // or higher the defender is ranked vs. the attacker.
                int lowestRank = HouseList.GetHouseList().LowestRank;

                double attackerRankPercent = 100.0 - ( ( _attackerHouse.Rank / (double)lowestRank ) * 100.0 );
                double defenderRankPercent = 100.0 - ( ( _defenderHouse.Rank / (double)lowestRank ) * 100.0 );

                //// no attacker may gain or loose more than 25% ambition in one attack
                int ambitionChangeValue = (int)( defenderRankPercent - attackerRankPercent ) / 4;

                if ( _attackerHouse.Ambition + ambitionChangeValue > 100 ) ambitionChangeValue = 0;
                if ( _attackerHouse.Ambition + ambitionChangeValue < 1 ) ambitionChangeValue = 0;

                if (ambitionChangeValue != 0)
                {
                    using (SqlCommand cm = cn.CreateCommand())
                    {
                        cm.CommandType = CommandType.Text;
                        cm.CommandText = "UPDATE bbgHouses SET Ambition = Ambition + " + ambitionChangeValue.ToString() + " WHERE ID = " + _attackerHouse.ID.ToString();
                        cm.ExecuteNonQuery();
                    }

                    if (Math.Abs( ambitionChangeValue ) == ambitionChangeValue)
                    {
                        _description.Add( "Your ambition improved by " + ambitionChangeValue.ToString() + "." );
                    }
                    else
                    {
                        _description.Add( "Your ambition worsened by " + ambitionChangeValue.ToString() + "." );
                    }
                }
                #endregion

                #region 9. Update Experience
                int experience = 0;
                foreach (Unit unit in _defenderForces)
                {
                    if ( unit.Casualties > 0 ) experience += ( ( unit.Experience + unit.ExperienceTech ) * unit.Casualties );
                }

                // only advance if experience was gained AND user isn't already at level the level cap
                if (experience > 0 && _attackerHouse.Level.Rank < config.LevelCap)
                {
                    Level.UpdateExperience( _attackerHouse, experience );
                    _description.Add( "You gained " + experience + " experience." );

                    // TODO: right now this will cause weird problems if enough exp to advance more than one level is gained at once
                    if (_attackerHouse.Experience + experience >= _attackerHouse.NextLevel.Experience)
                    {
                        _description.Add( "You advanced to level " + _attackerHouse.NextLevel.Rank.ToString() + "." );

                        // add level advancement
                        Advancement advancement = Advancement.NewAdvancement();
                        advancement.HouseID = _attackerHouse.ID;
                        advancement.LevelID = _attackerHouse.NextLevel.ID;
                        advancement.Save();

                        // add leveling report
                        AphelionTrigger.Library.Report report = Report.NewReport();
                        report.FactionID = _attackerHouse.FactionID;
                        report.GuildID = _attackerHouse.GuildID;
                        report.HouseID = _attackerHouse.ID;
                        report.Message = "House " + _attackerHouse.Name + " gained a level.";
                        report.ReportLevelID = 1 + House.GetSecrecyBonus( _attackerHouse.Intelligence );
                        report.Save();
                    }
                }

                #endregion

                #region 10. Add Attack
                int totalAttackerCasualties = 0;
                foreach (Unit unit in _attackerForces)
                    totalAttackerCasualties += unit.Casualties;

                int totalDefenderCasualties = 0;
                foreach (Unit unit in _defenderForces)
                    totalDefenderCasualties += unit.Casualties;

                Attack attack = AphelionTrigger.Library.Attack.NewAttack();
                attack.AttackerHouseID = _attackerHouse.ID;
                attack.DefenderHouseID = _defenderHouse.ID;
                attack.Captured = totalCaptured;
                attack.Plundered = plunder;
                attack.Stunned = stun;
                attack.AttackerCasualties = totalAttackerCasualties;
                attack.DefenderCasualties = totalDefenderCasualties;

                StringBuilder description = new StringBuilder();
                foreach (string s in _description)
                    description.AppendFormat( "{0}<br/>", s );

                attack.Description = description.ToString();

                attack.Save();

                AttackID = attack.ID;

                // decrement the attacker's turns that were expended in the attack
                House.UpdateTurns( _attackerHouse.ID, -1 );
                #endregion
            }
        }

        #region Data Binding
        private void BindAttackData()
        {
            using (SqlConnection cn = new SqlConnection( Database.AphelionTriggerConnection ))
            {
                cn.Open();
                using (SqlCommand cm = cn.CreateCommand())
                {
                    cm.CommandText = "GetAttackData";
                    cm.CommandType = CommandType.StoredProcedure;
                    cm.Parameters.AddWithValue( "@AttackerHouseID", _attackerHouseId );
                    cm.Parameters.AddWithValue( "@DefenderHouseID", _defenderHouseId );

                    using (SafeDataReader dr = new SafeDataReader( cm.ExecuteReader() ))
                    {
                        while (dr.Read())
                        {
                            _attackerHouse = new House(
                                dr.GetInt32( "ID" ),
                                dr.GetInt32( "UserID" ),
                                dr.GetInt32( "FactionID" ),
                                dr.GetString( "Faction" ),
                                dr.GetString( "FactionDisplay" ),
                                dr.GetString( "Name" ),
                                dr.GetInt32( "Intelligence" ),
                                dr.GetInt32( "Power" ),
                                dr.GetInt32( "Protection" ),
                                dr.GetInt32( "Affluence" ),
                                dr.GetInt32( "Speed" ),
                                dr.GetInt32( "Contingency" ),
                                dr.GetInt32( "LevelID" ),
                                (double)dr.GetDecimal( "Ambition" ),
                                dr.GetInt32( "Turns" ),
                                dr.GetInt32( "Credits" ),
                                dr.GetInt32( "MilitiaCount" ),
                                dr.GetInt32( "MilitaryCount" ),
                                dr.GetInt32( "MercenaryCount" ),
                                dr.GetInt32( "AgentCount" ),
                                dr.GetInt32( "Rank" ),
                                dr.GetInt32( "LastRank" ),
                                dr.GetInt32( "Points" ),
                                dr.GetInt32( "Experience" ),
                                dr.GetInt32( "Attack" ),
                                dr.GetInt32( "Defense" ),
                                dr.GetInt32( "Capture" ),
                                dr.GetInt32( "Plunder" ),
                                dr.GetInt32( "Stun" ),
                                dr.GetInt32( "GuildID" ),
                                dr.GetString( "Guild" ),
                                dr.GetString( "SmallFactionIconPath" ),
                                dr.GetInt32( "VotedFactionLeaderHouseID" ),
                                dr.GetInt32( "FactionLeaderHouseID" ),
                                dr.GetInt32( "FactionVotingPower" ) );
                        }

                        if (dr.NextResult())
                        {
                            while (dr.Read())
                            {
                                if (dr.GetInt32( "Count" ) > 0)
                                {
                                    _attackerForces.Add( new Unit(
                                        dr.GetInt32( "ID" ),
                                        dr.GetInt32( "FactionID" ),
                                        dr.GetInt32( "UnitClassID" ),
                                        dr.GetString( "Name" ),
                                        dr.GetString( "Description" ),
                                        dr.GetString( "Faction" ),
                                        dr.GetString( "UnitClass" ),
                                        dr.GetInt32( "Cost" ),
                                        dr.GetInt32( "Attack" ),
                                        dr.GetInt32( "Defense" ),
                                        dr.GetInt32( "Plunder" ),
                                        dr.GetInt32( "Capture" ),
                                        dr.GetInt32( "Stun" ),
                                        dr.GetInt32( "Experience" ),
                                        dr.GetDecimal( "RepopulationRate" ),
                                        dr.GetDecimal( "DepopulationRate" ),
                                        dr.GetInt32( "Count" ),
                                        dr.GetInt32( "AttackTech" ),
                                        dr.GetInt32( "DefenseTech" ),
                                        dr.GetInt32( "CaptureTech" ),
                                        dr.GetInt32( "PlunderTech" ),
                                        dr.GetInt32( "StunTech" ),
                                        dr.GetInt32( "ExperienceTech" ),
                                        dr.GetDecimal( "RepopulationRateTech" ),
                                        dr.GetDecimal( "DepopulationRateTech" ) ) );
                                }
                            }
                        }
                        else
                        {
                            throw new DataPortalException( "Attacker House has no forces to attack with.", this );
                        }

                        if (dr.NextResult())
                        {
                            while (dr.Read())
                            {
                                _attackerTechnologies.Add( new Technology(
                                    dr.GetInt32( "ID" ),
                                    dr.GetInt32( "FactionID" ),
                                    dr.GetInt32( "HouseID" ),
                                    dr.GetInt32( "GuildID" ),
                                    dr.GetInt32( "TechnologyTypeID" ),
                                    dr.GetInt32( "UnitID" ),
                                    dr.GetInt32( "UnitClassID" ),
                                    dr.GetString( "Name" ),
                                    dr.GetString( "Faction" ),
                                    dr.GetString( "House" ),
                                    dr.GetString( "Guild" ),
                                    dr.GetString( "Description" ),
                                    dr.GetString( "TechnologyType" ),
                                    dr.GetString( "Unit" ),
                                    dr.GetString( "UnitClass" ),
                                    dr.GetInt32( "Attack" ),
                                    dr.GetInt32( "Defense" ),
                                    dr.GetInt32( "Plunder" ),
                                    dr.GetInt32( "Capture" ),
                                    dr.GetInt32( "Stun" ),
                                    dr.GetInt32( "Experience" ),
                                    dr.GetDecimal( "RepopulationRate" ),
                                    dr.GetDecimal( "DepopulationRate" ),
                                    dr.GetInt32( "ResearchCost" ),
                                    dr.GetInt32( "ResearchTime" ),
                                    dr.GetInt32( "ResearchTurns" ),
                                    dr.GetInt32( "TimeSpent" ),
                                    dr.GetInt32( "TurnsSpent" ),
                                    dr.GetInt32( "CreditsSpent" ),
                                    dr.GetInt32( "ResearchStateID" ),
                                    dr.GetString( "ResearchState" ),
                                    dr.GetSmartDate( "ResearchStartedDate" ) ) );
                            }
                        }

                        if (dr.NextResult())
                        {
                            while (dr.Read())
                            {
                                _defenderHouse = new House(
                                    dr.GetInt32( "ID" ),
                                    dr.GetInt32( "UserID" ),
                                    dr.GetInt32( "FactionID" ),
                                    dr.GetString( "Faction" ),
                                    dr.GetString( "FactionDisplay" ),
                                    dr.GetString( "Name" ),
                                    dr.GetInt32( "Intelligence" ),
                                    dr.GetInt32( "Power" ),
                                    dr.GetInt32( "Protection" ),
                                    dr.GetInt32( "Affluence" ),
                                    dr.GetInt32( "Speed" ),
                                    dr.GetInt32( "Contingency" ),
                                    dr.GetInt32( "LevelID" ),
                                    (double)dr.GetDecimal( "Ambition" ),
                                    dr.GetInt32( "Turns" ),
                                    dr.GetInt32( "Credits" ),
                                    dr.GetInt32( "MilitiaCount" ),
                                    dr.GetInt32( "MilitaryCount" ),
                                    dr.GetInt32( "MercenaryCount" ),
                                    dr.GetInt32( "AgentCount" ),
                                    dr.GetInt32( "Rank" ),
                                    dr.GetInt32( "LastRank" ),
                                    dr.GetInt32( "Points" ),
                                    dr.GetInt32( "Experience" ),
                                    dr.GetInt32( "Attack" ),
                                    dr.GetInt32( "Defense" ),
                                    dr.GetInt32( "Capture" ),
                                    dr.GetInt32( "Plunder" ),
                                    dr.GetInt32( "Stun" ),
                                    dr.GetInt32( "GuildID" ),
                                    dr.GetString( "Guild" ),
                                    dr.GetString( "SmallFactionIconPath" ),
                                    dr.GetInt32( "VotedFactionLeaderHouseID" ),
                                    dr.GetInt32( "FactionLeaderHouseID" ),
                                    dr.GetInt32( "FactionVotingPower" ) );
                            }
                        }
                        else
                        {
                            throw new DataPortalException( "Defender House not found.", this );
                        }

                        if (dr.NextResult())
                        {
                            while (dr.Read())
                            {
                                if (dr.GetInt32( "Count" ) > 0)
                                {
                                    _defenderForces.Add( new Unit(
                                        dr.GetInt32( "ID" ),
                                        dr.GetInt32( "FactionID" ),
                                        dr.GetInt32( "UnitClassID" ),
                                        dr.GetString( "Name" ),
                                        dr.GetString( "Description" ),
                                        dr.GetString( "Faction" ),
                                        dr.GetString( "UnitClass" ),
                                        dr.GetInt32( "Cost" ),
                                        dr.GetInt32( "Attack" ),
                                        dr.GetInt32( "Defense" ),
                                        dr.GetInt32( "Plunder" ),
                                        dr.GetInt32( "Capture" ),
                                        dr.GetInt32( "Stun" ),
                                        dr.GetInt32( "Experience" ),
                                        dr.GetDecimal( "RepopulationRate" ),
                                        dr.GetDecimal( "DepopulationRate" ),
                                        dr.GetInt32( "Count" ),
                                        dr.GetInt32( "AttackTech" ),
                                        dr.GetInt32( "DefenseTech" ),
                                        dr.GetInt32( "CaptureTech" ),
                                        dr.GetInt32( "StunTech" ),
                                        dr.GetInt32( "PlunderTech" ),
                                        dr.GetInt32( "ExperienceTech" ),
                                        dr.GetDecimal( "RepopulationRateTech" ),
                                        dr.GetDecimal( "DepopulationRateTech" ) ) );
                                }
                            }
                        }

                        if (dr.NextResult())
                        {
                            while (dr.Read())
                            {
                                _defenderTechnologies.Add( new Technology(
                                    dr.GetInt32( "ID" ),
                                    dr.GetInt32( "FactionID" ),
                                    dr.GetInt32( "HouseID" ),
                                    dr.GetInt32( "GuildID" ),
                                    dr.GetInt32( "TechnologyTypeID" ),
                                    dr.GetInt32( "UnitID" ),
                                    dr.GetInt32( "UnitClassID" ),
                                    dr.GetString( "Name" ),
                                    dr.GetString( "Faction" ),
                                    dr.GetString( "House" ),
                                    dr.GetString( "Guild" ),
                                    dr.GetString( "Description" ),
                                    dr.GetString( "TechnologyType" ),
                                    dr.GetString( "Unit" ),
                                    dr.GetString( "UnitClass" ),
                                    dr.GetInt32( "Attack" ),
                                    dr.GetInt32( "Defense" ),
                                    dr.GetInt32( "Plunder" ),
                                    dr.GetInt32( "Capture" ),
                                    dr.GetInt32( "Stun" ),
                                    dr.GetInt32( "Experience" ),
                                    dr.GetDecimal( "RepopulationRate" ),
                                    dr.GetDecimal( "DepopulationRate" ),
                                    dr.GetInt32( "ResearchCost" ),
                                    dr.GetInt32( "ResearchTime" ),
                                    dr.GetInt32( "ResearchTurns" ),
                                    dr.GetInt32( "TimeSpent" ),
                                    dr.GetInt32( "TurnsSpent" ),
                                    dr.GetInt32( "CreditsSpent" ),
                                    dr.GetInt32( "ResearchStateID" ),
                                    dr.GetString( "ResearchState" ),
                                    dr.GetSmartDate( "ResearchStartedDate" ) ) );
                            }
                        }
                    }
                }
            }
        }
        #endregion

        #region Attack Processing

        private int GetCheapestMilitiaID( UnitList list )
        {
            int cheapest = 0;
            foreach (Unit unit in list)
            {
                if (cheapest == 0 || unit.Cost < cheapest) cheapest = unit.ID;
            }

            return cheapest;
        }

        private int[] PercentChanceDefenderUnitDies( int defenderTotalUnitDefense )
        {
            int[] percentChanceUnitDies = new int[_defenderForces.Count];
            int basePercentDiesRange = 0;
            for (int i = 0; i < _defenderForces.Count; i++)
            {
                // there is only a chance a unit will die if it hasn't suffered 100% casualties
                if (_defenderForces[i].Count != _defenderForces[i].Casualties)
                {
                    basePercentDiesRange += (int)System.Math.Round( (((_defenderForces[i].Defense + _defenderForces[i].DefenseTech) / (double)defenderTotalUnitDefense) * 100) );
                    percentChanceUnitDies[i] = basePercentDiesRange;
                }
                else
                {
                    percentChanceUnitDies[i] = 0;
                }
            }

            return percentChanceUnitDies;
        }

        private int[] PercentChancAttackerUnitDies( int attackerTotalUnitDefense )
        {
            int[] percentChanceUnitDies = new int[_attackerForces.Count];
            int basePercentDiesRange = 0;
            for (int i = 0; i < _attackerForces.Count; i++)
            {
                // there is only a chance a unit will die if it hasn't suffered 100% casualties
                if (_attackerForces[i].Count != _attackerForces[i].Casualties)
                {
                    basePercentDiesRange += (int)System.Math.Round( (((_attackerForces[i].Defense + _attackerForces[i].DefenseTech) / (double)attackerTotalUnitDefense) * 100) );
                    percentChanceUnitDies[i] = basePercentDiesRange;
                }
                else
                {
                    percentChanceUnitDies[i] = 0;
                }
            }

            return percentChanceUnitDies;
        }
        #endregion

        #region String Manipulation
        protected string Pluralize( string text )
        {
            if (text == null || text.Length == 0) return string.Empty;

            return (text.ToLower().EndsWith( "y" ) ? text.Remove( text.Length - 1, 1 ) + "ies" : text + "s");
        }
        #endregion
    }
}
