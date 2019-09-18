using MaxMind.GeoIP2.Responses;

namespace geolocalizationApi.DTO
{
    /// <summary>
    /// The country localization response.
    /// </summary>
    public class CountryResponseDto
    {
        /// <summary>
        /// The IP adress that corresponds to the country.
        /// </summary>
        public string IpAddress { get; set; }
        /// <summary>
        /// The country localization, directly from Max Mind database.
        /// </summary>
        public CountryResponse Country { get; set; }
        /// <summary>
        /// Some information in case the ip address is not in a correct format or does not exist in the database.
        /// </summary>
        public string Info { get; set; } = null;
    }
}
