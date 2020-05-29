/* *********************************************************************
 * This Original Work is copyright of 51 Degrees Mobile Experts Limited.
 * Copyright 2020 51 Degrees Mobile Experts Limited, 5 Charlotte Close,
 * Caversham, Reading, Berkshire, United Kingdom RG4 7BY.
 *
 * This Original Work is licensed under the European Union Public Licence (EUPL) 
 * v.1.2 and is subject to its terms as set out below.
 *
 * If a copy of the EUPL was not distributed with this file, You can obtain
 * one at https://opensource.org/licenses/EUPL-1.2.
 *
 * The 'Compatible Licences' set out in the Appendix to the EUPL (as may be
 * amended by the European Commission) shall be deemed incompatible for
 * the purposes of the Work and the provisions of the compatibility
 * clause in Article 5 of the EUPL shall not apply.
 * 
 * If using the Work as, or as part of, a network application, by 
 * including the attribution notice(s) required under Article 5 of the EUPL
 * in the end user terms of the application under an appropriate heading, 
 * such notice(s) shall fulfill the requirements of that article.
 * ********************************************************************* */

using FiftyOne.DeviceDetection;
using FiftyOne.DeviceDetection.Cloud.FlowElements;
using FiftyOne.GeoLocation;
using FiftyOne.GeoLocation.Cloud.FlowElements;
using FiftyOne.GeoLocation.Core;
using FiftyOne.GeoLocation.Core.Data;
using FiftyOne.Pipeline.CloudRequestEngine.FlowElements;
using FiftyOne.Pipeline.Core.FlowElements;
using Microsoft.Extensions.Logging;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

/// <summary>
/// @example CombiningServices/Program.cs
/// 
/// using 51Degrees reverse geocoding alongside 
/// 51Degrees device detection.
/// 
/// This example is available in full on [GitHub](https://github.com/51Degrees/location-dotnet/blob/master/FiftyOne.GeoLocation/Examples/CombiningServices/Program.cs). 
/// 
/// To run this example, you will need to create a **resource key**. 
/// The resource key is used as short-hand to store the particular set of 
/// properties you are interested in as well as any associated license keys 
/// that entitle you to increased request limits and/or paid-for properties.
/// 
/// You can create a resource key using the 51Degrees [Configurator](https://configure.51degrees.com).
/// 
/// Required NuGet Dependencies:
/// - FiftyOne.GeoLocation
/// - FiftyOne.DeviceDetection
/// 
/// The example shows how to:
/// 
/// 1. Build new cloud-based geolocation, and device detection engines, along
/// with the cloud request engine on which they rely.
/// ```
/// var cloudRequestEngine =
///     new CloudRequestEngineBuilder(_loggerFactory, _httpClient)
///     .SetResourceKey(resourceKey)
///     .Build();
/// var deviceDetectionEngine =
///     new DeviceDetectionCloudEngineBuilder(
///         _loggerFactory,
///         _httpClient,
///         cloudRequestEngine)
///     .Build();
/// var locationEngine =
///     new GeoLocationCloudEngineBuilder(
///         _loggerFactory,
///         cloudRequestEngine)
///     .Build(GeoLocationProvider.FiftyOneDegrees);
/// ```
///
/// 2. Build a new Pipeline containing the engines which were just created.
/// ```
/// var pipeline = new PipelineBuilder(_loggerFactory)
///     .AddFlowElement(cloudRequestEngine)
///     .AddFlowElement(deviceDetectionEngine)
///     .AddFlowElement(locationEngine)
///     .Build();
/// ```
/// 
/// Note that while the cloud request engine must be run before the others, the
/// device detection and geolocation engines can be run in parallel.
/// ```
/// var pipeline = new PipelineBuilder(_loggerFactory)
///     .AddFlowElement(cloudRequestEngine)
///     .AddFlowElementsParallel(deviceDetectionEngine, locationEngine)
///     .Build();
/// ```
/// 
/// 3. Create a new FlowData instance ready to be populated with evidence for
/// the Pipeline.
/// ```
/// var data = pipeline.CreateFlowData();
/// ```
///
/// 4. Process a longitude and latitude and User-Agent to retrieve the values
/// associated with the location and device for the selected properties.
/// ```
/// data
///     .AddEvidence(Constants.EVIDENCE_HEADER_USERAGENT_KEY, userAgent)
///     .AddEvidence(Constants.EVIDENCE_GEO_LAT_PARAM_KEY, "51.458048")
///     .AddEvidence(Constants.EVIDENCE_GEO_LON_PARAM_KEY, "-0.9822207999999999")
///     .Process();
/// ```
///
/// 5. Extract the value of a property as a string from the results.
/// ```
/// var country = data.Get<IGeoData>().Country;
/// var isMobile = data.Get<IDeviceData>().IsMobile;
/// ```
/// </summary>
namespace CombiningServices
{
    class Program
    {
        private static ILoggerFactory _loggerFactory = new LoggerFactory();

        private static HttpClient _httpClient = new HttpClient();

        private static string mobileUserAgent =
            "Mozilla/5.0 (Linux; Android 9; SAMSUNG SM-G960U) " +
            "AppleWebKit/537.36 (KHTML, like Gecko) SamsungBrowser/10.1 " +
            "Chrome/71.0.3578.99 Mobile Safari/537.36";

        static void Main(string[] args)
        {
            // Obtain a resource key for free at https://configure.51degrees.com
            // Make sure to include the 'Country' and 'IsMobile' properties as
            // they are used by this example.
            string resourceKey = "!!YOUR_RESOURCE_KEY!!";

            if (resourceKey.StartsWith("!!"))
            {
                Console.WriteLine("You need to create a resource key at " +
                    "https://configure.51degrees.com and paste it into this example.");
                Console.WriteLine("Make sure to include the 'Country' and " +
                    "'IsMobile' properties as they are used by this example.");
            }
            else
            {
                var cloudRequestEngine =
                    new CloudRequestEngineBuilder(_loggerFactory, _httpClient)
                    .SetResourceKey(resourceKey)
                    .Build();
                var deviceDetectionEngine =
                    new DeviceDetectionCloudEngineBuilder(_loggerFactory)
                    .Build();
                var locationEngine =
                    new GeoLocationCloudEngineBuilder(
                        _loggerFactory)
                    .Build(GeoLocationProvider.FiftyOneDegrees);

                using (var pipeline =
                    new PipelineBuilder(_loggerFactory)
                    .AddFlowElement(cloudRequestEngine)
                    .AddFlowElement(deviceDetectionEngine)
                    .AddFlowElement(locationEngine)
                    .Build())
                {
                    var data = pipeline.CreateFlowData();
                    data.AddEvidence("header.user-agent", mobileUserAgent);
                    data.AddEvidence("query.51D_Pos_latitude", "51.458048");
                    data.AddEvidence("query.51D_Pos_longitude", "-0.9822207999999999");

                    data.Process();

                    Console.Write($"Awaiting response");
                    CancellationTokenSource cancellationSource = new CancellationTokenSource();
                    Task.Run(() => { OutputUntilCancelled(".", 1000, cancellationSource.Token); });

                    var country = data.Get<IGeoData>().Country;
                    var isMobile = data.Get<IDeviceData>().IsMobile;

                    cancellationSource.Cancel();
                    Console.WriteLine();

                    Console.WriteLine($"Country: {country.ToString()}");
                    Console.WriteLine($"IsMobile: {isMobile.ToString()}");
                }
            }
            Console.ReadKey();
        }
        private static void OutputUntilCancelled(string text, int intervalMs, CancellationToken token)
        {
            while (token.IsCancellationRequested == false)
            {
                Console.Write(text);
                Task.Delay(intervalMs).Wait();
            }
        }
    }
}
