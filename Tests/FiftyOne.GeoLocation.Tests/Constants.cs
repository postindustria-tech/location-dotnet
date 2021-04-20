namespace FiftyOne.GeoLocation.Tests
{
    /// <summary>
    /// GeoLocation test constants
    /// </summary>
    internal class Constants
    {
        /// <summary>
        /// Sample response returned by a geo location api server. 
        /// In this case, nominatim.
        /// </summary>
        internal const string JSON_RESPONSE = @"
{
    'place_id': 85642204,
    'licence': 'Data © OpenStreetMap contributors, ODbL 1.0. https://osm.org/copyright',
    'osm_type': 'way',
    'osm_id': 96177745,
    'lat': '51.4578261',
    'lon': '-0.975922996290084',
    'display_name': 'Spaces, 9, Greyfriars Road, Coley, Reading, South East, England, RG1 1NU, United Kingdom',
    'address': {
        'place': 'Spaces',
        'house_number': '9',
        'road': 'Greyfriars Road',
        'suburb': 'Coley',
        'town': 'Reading',
        'county': 'Reading',
        'state_district': 'South East',
        'state': 'England',
        'postcode': 'RG1 1NU',
        'country': 'United Kingdom',
        'country_code': 'gb'
    },
    'boundingbox': ['51.4575761', '51.4580634', '-0.9764213', '-0.9757286']
}";

        internal const string TEST_LAT = "51.4578261";
        internal const string TEST_LON = "-0.975922996290084";

        internal const string RESOURCE_KEY_ENV_VAR = "SuperResourceKey";
    }
}
