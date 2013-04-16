using Funq;
using ServiceStack.WebHost.Endpoints;

namespace OsmDataService
{
    /// <summary>
    /// Defines how ServiceStack hosts the services.
    /// </summary>
    public class AppHost : AppHostHttpListenerBase
    {
        /// <summary>
        /// Creates a new app host.
        /// </summary>
        public AppHost()
            : base("Tiled Data REST Service using OsmSharp!", 
            new System.Reflection.Assembly[] { typeof(AppHost).Assembly })
        {

        }

        /// <summary>
        /// Allows for custom configuration.
        /// </summary>
        /// <param name="container"></param>
        public override void Configure(Container container)
        {
            SetConfig(new EndpointHostConfig
            {
                GlobalResponseHeaders =
	                {
	                    { "Access-Control-Allow-Origin", "*" },
	                    { "Access-Control-Allow-Methods", "GET, POST, PUT, DELETE, OPTIONS" },
	                },
            });
        }
    }
}
