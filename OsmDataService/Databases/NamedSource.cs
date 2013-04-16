using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OsmSharp.Osm;
using OsmSharp.Osm.Data;
using OsmSharp.Osm.Data.Core.Processor;
using OsmSharp.Osm.Data.Core.Processor.ListSource;
using OsmSharp.Osm.Simple;
using OsmSharp.Tools.Math.Geo;

namespace OsmDataService.Databases
{
    /// <summary>
    /// A named data source.
    /// </summary>
    public class NamedSource
    {
        /// <summary>
        /// Holds the data source for this named source.
        /// </summary>
        private readonly IDataSourceReadOnly _dataSourceReadOnly;

        /// <summary>
        /// The name of this data source.
        /// </summary>
        private readonly string _name;

        /// <summary>
        /// Creates a new named source.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="dataSourceReadOnly"></param>
        public NamedSource(string name, IDataSourceReadOnly dataSourceReadOnly)
        {
            _name = name;
            _dataSourceReadOnly = dataSourceReadOnly;
        }

        /// <summary>
        /// Returns the name of this source.
        /// </summary>
        public string Name
        {
            get { return _name; }
        }

        /// <summary>
        /// Returns all data in this source for the given bounding box.
        /// </summary>
        /// <param name="box"></param>
        /// <returns></returns>
        public DataProcessorSource Get(GeoCoordinateBox box)
        {
            return new OsmGeoListDataProcessorSource(
                _dataSourceReadOnly.Get(box, null));
        }
    }
}
