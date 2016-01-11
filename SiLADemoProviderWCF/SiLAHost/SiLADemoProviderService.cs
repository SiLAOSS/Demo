using System;
using System.ServiceModel;
using System.ServiceProcess;
using SiLA.Provider;

namespace SiLAHost
{
    /// <summary>
    /// Windows service to host SiLADemoProvider
    /// </summary>
    /// <seealso cref="System.ServiceProcess.ServiceBase" />
    public partial class SiLADemoProviderService : ServiceBase
    {
        /// <summary>
        /// Gets or sets the host
        /// </summary>
        private ServiceHost Host { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="SiLADemoProviderService"/> class
        /// </summary>
        public SiLADemoProviderService()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Starts this instance
        /// </summary>
        public void Start()
        {
            this.OnStart(new string[] { });
        }

        /// <summary>
        /// When implemented in a derived class, executes when a Start command is sent to the service by the Service Control Manager (SCM) or when the operating system starts (for a service that starts automatically). Specifies actions to take when the service starts.
        /// </summary>
        /// <param name="args">Data passed by the start command.</param>
        protected override void OnStart(string[] args)
        {
            try
            {
                string http = Properties.Settings.Default.Uri;
                Uri[] adrbase = { new Uri(http) };
                this.Host = new ServiceHost(typeof(SiLAWebService), adrbase);            
                this.Host.Open();
            }
            catch
            {
                this.OnStop();
            }
        }

        /// <summary>
        /// When implemented in a derived class, executes when a Stop command is sent to the service by the Service Control Manager (SCM). Specifies actions to take when a service stops running.
        /// </summary>
        protected override void OnStop()
        {
            this.Host.Close();
        }
    }
}
