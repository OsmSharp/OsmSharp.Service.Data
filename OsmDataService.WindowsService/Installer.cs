using System.ComponentModel;

namespace OsmDataService.WindowsService
{
    /// <summary>
    /// Installer for this service.
    /// </summary>
    [RunInstaller(true)]
    public partial class Installer : System.Configuration.Install.Installer
    {
        /// <summary>
        /// Creates installer.
        /// </summary>
        public Installer()
        {
            InitializeComponent();
        }
    }
}
