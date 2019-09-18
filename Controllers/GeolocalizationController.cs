using System.Collections.Generic;
using System.Linq;
using geolocalizationApi.Controllers.Parameters;
using geolocalizationApi.DTO;
using geolocalizationApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace geolocalizationApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GeolocalizationController : ControllerBase
    {
        private GeolocalizationService _service;
        private IHttpContextAccessor _httpContextAccessor;

        public GeolocalizationController(GeolocalizationService service, IHttpContextAccessor httpContextAccessor)
        {
            _service = service;
            _httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// Calls the geolocalization service to get the localization country of the caller.
        /// </summary>
        /// <returns>The localization country of the caller.</returns>
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<CountryResponseDto> GetLocalization()
        {
            var ipAddress = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress;

            var countryResponseDto = _service.GetLocalization(ipAddress);
            if (countryResponseDto.Country == null)
                return NotFound(countryResponseDto.Info);

            return Ok(countryResponseDto);
        }

        /// <summary>
        /// Calls the geolocalization service to get the localization country of the ip address list.
        /// </summary>
        /// <param name="listIpsParameter">The list of ip address.</param>
        /// <returns>The localization country of the ip address list.</returns>
        [HttpPost]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<IEnumerable<CountryResponseDto>> GetLocalizationFromIpAdresses([FromBody]ListIpAddressParameter listIpsParameter)
        {
            if (!listIpsParameter.IpAddressList.Any())
            {
                var message = "The list of IPs addresses is required.";
                return BadRequest(message);
            }

            return Ok(_service.GetLocalizationFromIpAdresses(listIpsParameter.IpAddressList));
        }
    }
}
