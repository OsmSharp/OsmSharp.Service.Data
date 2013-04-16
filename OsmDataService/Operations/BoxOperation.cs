// License: MIT. Copyright 2013 by Ben Abelshausen. See LICENCE
using System.Runtime.Serialization;
using ServiceStack.ServiceHost;

namespace OsmDataService.Operations
{
    /// <summary>
    /// The default data operation.
    /// </summary>
    [Route("/box/{File}", "GET")]
    public class BoxOperation : IReturn<string>
    {
        /// <summary>
        /// The filename to get a piece out of.
        /// </summary>
        public string File { get; set; }

        /// <summary>
        /// The description the box.
        /// </summary>
        [DataMember(Name = "bbox")]
        public string Box { get; set; }
    }
}