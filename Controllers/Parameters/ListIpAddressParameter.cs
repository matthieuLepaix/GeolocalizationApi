using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace geolocalizationApi.Controllers.Parameters
{
    public class ListIpAddressParameter
    {
        [Required]
        public IEnumerable<string> IpAddressList { get; set; }
    }
}
