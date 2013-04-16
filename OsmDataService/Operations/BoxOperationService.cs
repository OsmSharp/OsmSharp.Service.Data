using System;
using System.Globalization;
using System.Net;
using OsmSharp.Tools.Math.Geo;
using ServiceStack.ServiceInterface;

namespace OsmDataService.Operations
{
    /// <summary>
    /// Data operation service implementation.
    /// </summary>
    public class BoxOperationService : Service
    {
        /// <summary>
        /// Executes the request.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [AddHeader(ContentType = "text/plain")]
        public string Get(BoxOperation request)
        {
            try
            {
                // parse the bounding box.
                if (string.IsNullOrWhiteSpace(request.Box))
                {
                    throw new ArgumentException("Invalid request: bbox not defined!");
                }

                // split arguments.
                string[] bounds = request.Box.Split(',');
                if (bounds == null || bounds.Length != 4)
                {
                    throw new ArgumentException(string.Format(
                        "Invalid request: bbox invalid: {0}!", request.Box));
                }

                // parse bounds.
                float left, bottom, right, top;
                if (!float.TryParse(bounds[0], NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture,
                                    out left) ||
                    !float.TryParse(bounds[1], NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture,
                                    out bottom) ||
                    !float.TryParse(bounds[2], NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture,
                                    out right) ||
                    !float.TryParse(bounds[3], NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture,
                                    out top))
                {
                    throw new ArgumentException(string.Format(
                        "Invalid request: bbox invalid: {0}!", request.Box));
                }

                // execute the request.
                return (new DataService()).RequestData(request.File, new GeoCoordinateBox(
                                                                         new GeoCoordinate(left, bottom),
                                                                         new GeoCoordinate(right, top)));
            }
            catch (Exception ex)
            {
                base.Response.StatusCode = (int) HttpStatusCode.BadRequest;
                return string.Empty;
            }
        }
    }
}