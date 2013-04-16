using System;
using System.Net;
using ServiceStack.ServiceInterface;
using OsmSharp.Osm;

namespace OsmDataService.Operations
{
    /// <summary>
    /// Data operation service implementation.
    /// </summary>
    public class DataOperationService : Service
    {
        /// <summary>
        /// Executes the request.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [AddHeader(ContentType = "text/plain")]
        public string Get(DataOperation request)
        {
            try
            {
                // parse the zoom, x and y.
                int x, y, zoom;
                if (!int.TryParse(request.X, out x))
                {
                    // invalid x.
                    throw new InvalidCastException("Cannot parse x-coordinate!");
                }
                if (!int.TryParse(request.Y, out y))
                {
                    // invalid y.
                    throw new InvalidCastException("Cannot parse y-coordinate!");
                }
                if (!int.TryParse(request.Zoom, out zoom))
                {
                    // invalid zoom.
                    throw new InvalidCastException("Cannot parse zoom!");
                }

                // create the filter.
                var tile = new Tile(x, y, zoom);

                // invert the y-coordinate, system of HOT-tasking manager is inverted.
                tile = tile.InvertY();

                // execute the request.
                return (new DataService()).RequestData(request.File, tile.Box);
            }
            catch (Exception ex)
            {
                base.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return string.Empty;
            }
        }
    }
}