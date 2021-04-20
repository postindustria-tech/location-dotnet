using FiftyOne.Common.TestHelpers;
using FiftyOne.GeoLocation.Cloud;
using FiftyOne.GeoLocation.Cloud.FlowElements;
using FiftyOne.Pipeline.CloudRequestEngine.FlowElements;
using FiftyOne.Pipeline.Core.FlowElements;
using FiftyOne.Pipeline.Engines.Data;
using FiftyOne.Pipeline.Engines.FlowElements;
using System.Collections.Generic;
using System.Net.Http;

namespace FiftyOne.GeoLocation.Tests
{
    /// <summary>
    /// Implementation of the <see cref="IWrapper"/> interface using a 
    /// 51Degrees Geo-Location engine.
    /// </summary>
    public class Wrapper51Degrees: IWrapper
    {
        protected static readonly TestLoggerFactory _logger = new TestLoggerFactory();

        public CloudRequestEngine RequestEngine { get; private set; }
        public GeoLocationCloudEngine Engine { get; private set; }
        public IPipeline Pipeline { get; private set; }

        /// <summary>
        /// Constructor. Create a Cloud Request engine and a Geo-Location engine 
        /// and add these to a pipeline. Gets the resource key for the Cloud 
        /// Request engine from environment variables.
        /// </summary>
        public Wrapper51Degrees()
        {
            var resourceKey = System.Environment.GetEnvironmentVariable(
                Constants.RESOURCE_KEY_ENV_VAR) ?? "!!YOUR_RESOURCE_KEY!!";

            RequestEngine = new CloudRequestEngineBuilder(_logger, new HttpClient())
                .SetResourceKey(resourceKey)
                .Build();
            Engine = new GeoLocationCloudEngineBuilder(_logger)
                .SetGeoLocationProvider("FiftyOneDegrees")
                .Build();

            Pipeline = new PipelineBuilder(_logger)
                .AddFlowElement(RequestEngine)
                .AddFlowElement(Engine)
                .Build();
        }

        public IEnumerable<IAspectPropertyMetaData> Properties => Engine.Properties;

        public IAspectEngine GetEngine()
        {
            return Engine;
        }
    }
}
