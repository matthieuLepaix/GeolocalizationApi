using System.Net;
using MaxMind.GeoIP2;
using MaxMind.GeoIP2.Responses;

namespace geolocalizationApi.Database
{
    /// <summary>
    /// This class represents the Database provider.
    /// It allows to get access to the geolocalization database GeoLite2-Country.mmdb.
    /// </summary>
    public class DbProvider
    {
        /// <summary>
        /// The database reader.
        /// </summary>
        private DatabaseReader DatabaseReader { get; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="dbPath">The relative path to the database.</param>
        public DbProvider(string dbPath)
        {
            DatabaseReader = new DatabaseReader(dbPath);
        }

        /// <summary>
        /// Get the country localization of an IP address from the database.
        /// </summary>
        /// <param name="ip">the ip address.</param>
        /// <returns>The country localization of an IP address.</returns>
        public CountryResponse GetCountryFromIpAddress(IPAddress ip)
        {
            return DatabaseReader.Country(ip);
        }

        /// <summary>
        /// Get the country localization of an IP address from the database.
        /// </summary>
        /// <param name="ip">the ip address.</param>
        /// <returns>The country localization of an IP address.</returns>
        public CountryResponse GetCountryFromIpAddress(string ip)
        {
            return DatabaseReader.Country(ip);
        }
    }
}
