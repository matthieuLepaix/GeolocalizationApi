using System.Net;
using geolocalizationApi.Database;
using MaxMind.GeoIP2.Exceptions;
using MaxMind.GeoIP2.Responses;

namespace geolocalizationApi.Repositories
{
    public class GeolocalizationRepository
    {
        private DbProvider _dbProvider;

        public GeolocalizationRepository(DbProvider dbProvider)
        {
            _dbProvider = dbProvider;
        }

        /// <summary>
        /// Returns the localization that matched to the ip parameter.
        /// </summary>
        /// <param name="ip">the ip address.</param>
        /// <returns>null if the ip does not exist in the database, else the localization that matched to the ip parameter.</returns>
        public CountryResponse GetLocalization(IPAddress ip)
        {
            try
            {
                return _dbProvider.GetCountryFromIpAddress(ip);
            }
            catch(AddressNotFoundException)
            {
                return null;
            }
        }
    }
}
