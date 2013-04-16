using System.Configuration;
using System.IO;
using System.Text;
using OsmSharp.Osm.Data.Core.Processor;
using OsmSharp.Osm.Data.Core.Processor.Filter;
using OsmSharp.Osm.Data.PBF.Raw.Processor;
using OsmSharp.Osm.Data.XML.Processor;
using OsmSharp.Tools.Math.Geo;

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
            if (pbfFile.Exists)
            {
                // create PBF source.
                sourceStream = pbfFile.OpenRead();
                source = new PBFDataProcessorSource(sourceStream);
            }
            else if (xmlFile.Exists)
            {
                // create XML source.
                sourceStream = xmlFile.OpenRead();
                source = new XmlDataProcessorSource(sourceStream);
            }
            //else if () // implement also data sources that are not files but name 
                // datasource that can be databases or other sources.
            //{
                
            //}
            else
            {
                // oeps! file not found!
                throw new FileNotFoundException("File not found!", xmlFile.Name);
            }

            // create the filter.
            DataProcessorFilter filter = new DataProcessorFilterBoundingBox(box);

            // create the target.
            var result = new StringBuilder();
            var writer = new StringWriter(result);
            var target = new XmlDataProcessorTarget(writer);

            // execute the processing.
            target.RegisterSource(filter);
            filter.RegisterSource(source);
            target.Pull();

            // close the source stream.
            sourceStream.Close();
            sourceStream.Dispose();

            return result.ToString();
        }
    }
}