using FiftyOne.Pipeline.Core.FlowElements;
using FiftyOne.Pipeline.Engines.Data;
using FiftyOne.Pipeline.Engines.FlowElements;
using System.Collections.Generic;

namespace FiftyOne.GeoLocation.Tests
{
    /// <summary>
    /// Cloud Geo-Location engine test wrapper Interface.
    /// </summary>
    public interface IWrapper
    {
        /// <summary>
        /// The Pipeline in this wrapper instance.
        /// </summary>
        IPipeline Pipeline { get; }

        /// <summary>
        /// Get the property meta data from the Geo-Location engine in this 
        /// wrapper instance.
        /// </summary>
        IEnumerable<IAspectPropertyMetaData> Properties { get; }

        /// <summary>
        /// Get the Geo-Location engine in this wrapper instance.
        /// </summary>
        /// <returns></returns>
        IAspectEngine GetEngine();

    }
}
