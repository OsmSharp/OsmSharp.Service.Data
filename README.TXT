OsmDataService is a simple service that can serve OSM data from XML/PBF files using simple bounding box/tiled requests.

The service was developed using OsmSharp, a Mono/.NET framework for OpenStreetMap:

http://www.osmsharp.com/
http://osmsharp.codeplex.com/

Usage: 

- Configure the hostname in the .config file.
- Configure the folder containing the .osm or .osm.pbf files in the .config file.
- Start the process.
- Request data using tiles or boundingboxes:
	http://{hostname}:{port}/box/{filename}.osm?bbox=left,bottom,right,top
	http://{hostname}:{port}/data/{x}/{y}/{zoom}/{filename}.osm