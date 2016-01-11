using System.Collections;
using System.ComponentModel;
using System.ServiceProcess;

namespace SiLAHost
{
    /// <summary>
    /// Installs SiLA Demo service
    /// </summary>
    /// <seealso cref="System.Configuration.Install.Installer" />
    [RunInstaller(true)]
    public partial class ProjectInstaller : System.Configuration.Install.Installer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProjectInstaller"/> class.
        /// </summary>
        public ProjectInstaller()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Raises the <see cref="E:System.Configuration.Install.Installer.AfterInstall" /> event.
        /// </summary>
        /// <param name="savedState">An <see cref="T:System.Collections.IDictionary" /> that contains the state of the computer after all the installers contained in the <see cref="P:System.Configuration.Install.Installer.Installers" /> property have completed their installations.</param>
        protected override void OnAfterInstall(IDictionary savedState)
        {
            base.OnAfterInstall(savedState);
            var controller = new System.ServiceProcess.ServiceController(ServiceName);
            controller.Start();
        }
    }
}