using System.Configuration;
using System.IO;
using System.Text;
using OsmSharp.Osm.Data.Core.Processor;
using OsmSharp.Osm.Data.PBF.Raw.Processor;
using OsmSharp.Osm.Data.XML.Processor;
using OsmSharp.Tools.Math.Geo;
using OsmDataService.Databases;
using OsmSharp.Osm.Data.Processor.Filter.BoundingBox;

namespace OsmDataService
{
    /// <summary>
    /// Handles all requests.
    /// </summary>
    public class DataService
    {
        /// <summary>
        /// Returns the data from the given data source that is inside the given box.
        /// </summary>
        /// <param name="dataSourceName">The name of the datasource.</param>
        /// <param name="box">The bounding box.</param>
        /// <returns></returns>
        public string RequestData(string dataSourceName, GeoCoordinateBox box)
        {
            string dataPath = ConfigurationManager.AppSettings["datapath"];

            // check of the file exists.
            var pbfFile = new FileInfo(dataPath + dataSourceName + ".pbf");
            var xmlFile = new FileInfo(dataPath + dataSourceName);

            DataProcessorSource source = null;
            FileStream sourceStream = null;
            NamedSource namedSource;
            if (pbfFile.Exists)
            { // first try PBF: more efficient.
                // create PBF source.
                sourceStream = pbfFile.OpenRead();
                source = new PBFDataProcessorSource(sourceStream);

                // create filter.
                DataProcessorFilter filter = new DataProcessorFilterBoundingBox(box);
                filter.RegisterSource(source);
                source = filter;
            }
            else if (xmlFile.Exists)
            { // then try XML.
                // create XML source.
                sourceStream = xmlFile.OpenRead();
                source = new XmlDataProcessorSource(sourceStream);

                // create filter.
                DataProcessorFilter filter = new DataProcessorFilterBoundingBox(box);
                filter.RegisterSource(source);
                source = filter;
            }
            else if (NamedSourceCollection.Instance.TryGetSource(dataSourceName, out namedSource))
            { // then try a named source.
                source = namedSource.Get(box);
            }
            else
            { // oeps! file or named source not found!
                throw new FileNotFoundException("File or name source {0} not found!", dataSourceName);
            }

            // create the target.
            var result = new StringBuilder();
            var writer = new StringWriter(result);
            var target = new XmlDataProcessorTarget(writer);

            // execute the processing.
            target.RegisterSource(source);
            target.Pull();

            // close the source stream if needed.
            if (sourceStream != null)
            {
                sourceStream.Close();
                sourceStream.Dispose();
            }

            return result.ToString();
        }
    }
}