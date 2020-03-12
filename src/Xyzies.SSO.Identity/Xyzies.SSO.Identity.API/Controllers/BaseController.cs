using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Xyzies.SSO.Identity.API.Controllers
{
    /// <summary>
    /// Base controller
    /// </summary>
    public class BaseController : ControllerBase
    {
        /// <summary>
        /// Authorize token
        /// </summary>
        public string Token
        {
            get => HttpContext.Request.Headers["Authorization"].ToString().Split(' ').LastOrDefault();
        }
    }
}
