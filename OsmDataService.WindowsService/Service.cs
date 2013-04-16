using System.Configuration;
using System.ServiceProcess;

namespace OsmDataService.WindowsService
{
    /// <summary>
    /// Windows service serving tiled data.
    /// </summary>
    public partial class TileDataServiceService : ServiceBase
    {
        /// <summary>
        /// Holds the application host.
        /// </summary>
        private AppHost _host;

        /// <summary>
        /// Creates the service.
        /// </summary>
        public TileDataServiceService()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Called when the service starts.
        /// </summary>
        /// <param name="args"></param>
        protected override void OnStart(string[] args)
        {
            // get the hostname.
            string hostname = ConfigurationManager.AppSettings["hostname"];

            // start the self-hosting.
            _host = new AppHost();
            _host.Init();
            _host.Start(hostname);

        }

        /// <summary>
        /// Called when the service stops.
        /// </summary>
        protected override void OnStop()
        {
            _host.Stop();
        }
    }
}
