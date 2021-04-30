﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using log4net;
using log4net.Config;
using NamedPipeSerialProxy.Core;

namespace NamedPipeSerialProxy.Service
{
    public partial class NamedPipeSerialProxyService : ServiceBase
    {
        static readonly List<Connection> ConnectionList = new List<Connection>();
        static readonly ILog Logger = LogManager.GetLogger(typeof(NamedPipeSerialProxyService));
        readonly string _servicePath = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory).FullName;

        public NamedPipeSerialProxyService()
        {
            XmlConfigurator.ConfigureAndWatch(new FileInfo(_servicePath + "log4net.config"));
            InitializeComponent();
        }

        public void Start() => OnStart(null);
        // public void Stop() => OnStop();

        protected override void OnStart(string[] args)
        {
            ConnectionList.AddRange(
                Directory.GetFiles(_servicePath, "*.n2c")
                    .Select(Settings.Load)
                    .Select(c => new Connection(c)));

            Logger.Debug("Loaded (" + ConnectionList.Count + ") connection files");
            ConnectionList.ForEach(c => c.Start());
        }

        protected override void OnStop() => ConnectionList.ForEach(c => c.Stop());
    }

    [RunInstaller(true)]
    public class WindowsServiceInstaller : Installer
    {
        public WindowsServiceInstaller()
        {
            var serviceProcessInstaller = new ServiceProcessInstaller();
            var serviceInstaller = new ServiceInstaller();

            //# Service Account Information
            serviceProcessInstaller.Account = ServiceAccount.LocalSystem;
            serviceProcessInstaller.Username = null;
            serviceProcessInstaller.Password = null;

            //# Service Information
            serviceInstaller.DisplayName = "NP2COMService";
            serviceInstaller.StartType = ServiceStartMode.Automatic;

            //# This must be identical to the WindowsService.ServiceBase name
            //# set in the constructor of WindowsService.cs
            serviceInstaller.ServiceName = "NP2COMService";

            Installers.Add(serviceProcessInstaller);
            Installers.Add(serviceInstaller);
        }
    }
}
