using System;
using System.Collections.Generic;
using System.Text;

namespace Xyzies.SSO.Identity.Services.Models.Branch
{
    /// <summary>
    /// Branch model
    /// </summary>
    public class BranchModel
    {
        /// <summary>
        /// Id
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// CompanyId
        /// </summary>
        public int? CompanyId { get; set; }
    }
}
