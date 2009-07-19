using System.Collections.Generic;
using System.ServiceProcess;
using System.Text;
using System.Threading;

namespace AphelionTrigger.Services
{
    public class AphelionTriggerDaemon : ServiceBase
    {
        private Timer CreditsTimer;
        private Timer PopulationTimer;
        private Timer RankingTimer;
        private Timer ResearchTimer;
        private Timer TurnsTimer;

        private ATTurns turns;
        private ATResearch research;
        private ATRanking ranking;
        private ATPopulation population;
        private ATCredits credits;

        public static void Main()
        {
            ServiceBase[] ServicesToRun = new ServiceBase[] { new AphelionTriggerDaemon() };

            ServiceBase.Run( ServicesToRun );
        }

        public AphelionTriggerDaemon()
        {
            this.ServiceName = "Aphelion Trigger Daemon";
            this.CanStop = true;
            this.CanPauseAndContinue = true;
            this.AutoLog = true;
        }

        protected override void OnStart( string[] args )
        {
            // every 10 minutes
            credits = new ATCredits();
            TimerCallback creditsCallback = new TimerCallback( credits.UpdateCredits );
            CreditsTimer = new Timer( creditsCallback, null, 0, (10 * 60 * 1000) );

            ////// every hour
            population = new ATPopulation();
            TimerCallback populationCallback = new TimerCallback( population.UpdatePopulation );
            PopulationTimer = new Timer( populationCallback, null, 0, (60 * 60 * 1000) );

            ////// every 20 minutes
            ranking = new ATRanking();
            TimerCallback rankingCallback = new TimerCallback( ranking.UpdateRankings );
            RankingTimer = new Timer( rankingCallback, null, 0, (20 * 60 * 1000) );

            ////// every minute
            research = new ATResearch();
            TimerCallback researchCallback = new TimerCallback( research.UpdateResearch );
            ResearchTimer = new Timer( researchCallback, null, 0, (60 * 1000) );

            // every 5 minutes
            turns = new ATTurns();
            TimerCallback turnsCallback = new TimerCallback( turns.UpdateTurns );
            TurnsTimer = new Timer( turnsCallback, null, 0, (5 * 60 * 1000) );
        }

        protected override void OnStop()
        {
            TurnsTimer.Dispose();
        }
    }
}