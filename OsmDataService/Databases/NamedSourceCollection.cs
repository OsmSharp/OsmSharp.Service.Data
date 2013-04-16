using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OsmDataService.Databases
{
    /// <summary>
    /// Contains a collection of named data source collections.
    /// </summary>
    public sealed class NamedSourceCollection
    {
        /// <summary>
        /// Holds all named sources.
        /// </summary>
        private readonly Dictionary<string, NamedSource> _namedSources;

        /// <summary>
        /// The synchonisation object.
        /// </summary>
        private static readonly object Sync = new object();

        #region Singleton

        /// <summary>
        /// Creates new collection.
        /// </summary>
        private NamedSourceCollection()
        {
            _namedSources = new Dictionary<string, NamedSource>();
        }

        /// <summary>
        /// Holds the one and only instance of this class.
        /// </summary>
        private static NamedSourceCollection _instance;

        /// <summary>
        /// Returns the one and only instance of this class.
        /// </summary>
        public static NamedSourceCollection Instance
        {
            get
            {
                lock (Sync)
                {
                    if (_instance == null)
                    {
                        _instance = new NamedSourceCollection();
                    }
                    return _instance;
                }
            }
        }

        #endregion

        /// <summary>
        /// Adds a new name source.
        /// </summary>
        /// <param name="namedSource"></param>
        public void AddSource(NamedSource namedSource)
        {
            lock (Sync)
            {
                _namedSources[namedSource.Name] = namedSource;
            }
        }

        /// <summary>
        /// Gets the named source by name, returns false if the source is not found.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="namedSource"></param>
        /// <returns></returns>
        public bool TryGetSource(string name, out NamedSource namedSource)
        {
            lock (Sync)
            {
                return _namedSources.TryGetValue(name, out namedSource);
            }
        }
    }
}
