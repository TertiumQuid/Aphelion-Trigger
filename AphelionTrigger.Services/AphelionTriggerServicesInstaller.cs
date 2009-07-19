using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.ServiceProcess;

namespace AphelionTrigger.Services
{
    [RunInstaller( true )]
    public partial class AphelionTriggerInstaller : Installer
    {
        private ServiceProcessInstaller MainProcessInstaller;
        private ServiceInstaller AphelionTriggerProcessInstaller;

        public AphelionTriggerInstaller()
        {
            MainProcessInstaller = new ServiceProcessInstaller();
            AphelionTriggerProcessInstaller = new ServiceInstaller();

            MainProcessInstaller.Account = System.ServiceProcess.ServiceAccount.LocalService;
            MainProcessInstaller.Password = null;
            MainProcessInstaller.Username = null;

            AphelionTriggerProcessInstaller.ServiceName = "AphelionTriggerDaemon";
            AphelionTriggerProcessInstaller.DisplayName = "Aphelion Trigger Daemon";
            AphelionTriggerProcessInstaller.StartType = System.ServiceProcess.ServiceStartMode.Manual;
            AphelionTriggerProcessInstaller.Description = "Updates recurring game events including credits, population, ranking, research, and turns."; 

            this.Installers.AddRange( new System.Configuration.Install.Installer[] { MainProcessInstaller, AphelionTriggerProcessInstaller } );

            InitializeComponent();
        }
    }
}