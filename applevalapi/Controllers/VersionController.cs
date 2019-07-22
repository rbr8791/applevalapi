using Microsoft.AspNetCore.Mvc;
using applevalApi.Helpers;

namespace applevalApi.Controllers
{
    [Route("/[controller]")]
    [Produces("text/plain")]
    public class VersionController : BaseController
    {
        /// <summary>
        /// Gets the semver formatted version number for the application
        /// </summary>
        /// <returns>
        /// A string representing the semver formatted version number for the application
        /// </returns>
        [HttpGet]
        public string Get()
        {
            return CommonHelpers.GetVersionNumber();
        }
    }
}