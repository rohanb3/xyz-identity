using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Xyzies.TWC.DisputeService.API.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [ApiController, Route("api/version")]
    public class VersionController : ControllerBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet, HttpHead]
        public async Task<IActionResult> Get()
        {
            bool isHttp20 = Request.Protocol == "HTTP/2";

            return await Task.FromResult(Ok(new
            {
                ServiceName = "XYZies.SSO.IdentityService",
                ServiceVersion = "1.0",
                ApiVersion = "1.0",
                BuildNumber = "none",
                ReleaseDate = DateTime.Now.ToShortDateString(),
                UUseHttp2 = isHttp20
            }));
        }
    }
}