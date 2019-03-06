using System.Collections.Generic;

namespace Xyzies.SSO.Identity.Services.Service
{
    public class SelectConditions
    {
        /// <summary>
        /// Creates a query param string to select passed properties from Azure user entity
        /// </summary>
        /// <param name="paramNames">List of user's property names</param>
        /// <returns></returns>
        public static string GetSelectQuery(List<string> paramNames)
        {
            return paramNames != null && paramNames.Count != 0 
                ? $"$select={string.Join(',', paramNames)}" 
                : "";
        }
    }
}
