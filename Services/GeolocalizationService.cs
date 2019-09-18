using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using geolocalizationApi.DTO;
using geolocalizationApi.Repositories;

namespace geolocalizationApi.Services
{
    public class GeolocalizationService
    {
        private GeolocalizationRepository _repository;

        public GeolocalizationService(GeolocalizationRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Calls the geolocalization repository to get the localization of an IP address.
        /// </summary>
        /// <param name="ip">The IP address</param>
        /// <returns>The localization of the IP address.</returns>
        public CountryResponseDto GetLocalization(IPAddress ip)
        {
            var country = _repository.GetLocalization(ip);

            return new CountryResponseDto
            {
                IpAddress = ip.ToString(),
                Country = country,
                Info = country == null ? $"The IP Address {ip.ToString()} was not found in database." : null
            };
        }

        /// <summary>
        /// Calls the geolocalization repository to get the localization of all the IP addresses.
        /// </summary>
        /// <param name="ipAddressesList">The IP list addresses</param>
        /// <returns>The localization of the IP list addresses.</returns>
        public IEnumerable<CountryResponseDto> GetLocalizationFromIpAdresses(IEnumerable<string> ipAddressesList)
        {
            return ipAddressesList
                            .Select(GetLocalizationFromIpAddressString);
        }

        /// <summary>
        /// Returns the country localozation from a string ip address
        /// </summary>
        /// <param name="ipAddressString">The IP address in a string format.</param>
        /// <returns>The country localozation from a string ip address</returns>
        private CountryResponseDto GetLocalizationFromIpAddressString(string ipAddressString)
        {
            CountryResponseDto countryResponseDto = new CountryResponseDto();
            try
            {
                var ipAddress = IPAddress.Parse(ipAddressString);
                return GetLocalization(ipAddress);
            }
            catch (FormatException)
            {
                countryResponseDto.Info = $"The IP Address {ipAddressString} is not in a correct format.";
            }
            catch (ArgumentException)
            {
                countryResponseDto.Info = "The IP Address is missing.";
            }

            return countryResponseDto;
        }
    }
}
