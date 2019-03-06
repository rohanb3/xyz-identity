using System;
using System.Collections.Generic;
using System.Linq;
using Xyzies.SSO.Identity.Data.Entity;
using Xyzies.SSO.Identity.Data.Helpers;

namespace Xyzies.SSO.Identity.Services.Service
{
    public class FilterConditions
    {
        /// <summary>
        /// Creates a "and" filter condition with passed params
        /// </summary>
        /// <param name="first">Condition's left operand</param>
        /// <param name="second">Condition's right operand</param>
        /// <returns></returns>
        public static string And(string left, string right)
        {
            if(string.IsNullOrEmpty(left))
            {
                throw new ArgumentNullException(nameof(left));
            }

            if (string.IsNullOrEmpty(right))
            {
                throw new ArgumentNullException(nameof(right));
            }

            return $"{left} and {right}";
        }

        /// <summary>
        /// Creates a "or" filter condition with passed params
        /// </summary>
        /// <param name="first">Condition's left operand</param>
        /// <param name="second">Condition's right operand</param>
        /// <returns></returns>
        public static string Or(string left, string right)
        {
            if (string.IsNullOrEmpty(left))
            {
                throw new ArgumentNullException(nameof(left));
            }

            if (string.IsNullOrEmpty(right))
            {
                throw new ArgumentNullException(nameof(right));
            }

            return $"{left} or {right}";
        }

        /// <summary>
        /// Creates a "equal" (eq) filter condition with passed params
        /// </summary>
        /// <param name="first">Condition's left operand</param>
        /// <param name="second">Condition's right operand</param>
        /// <returns></returns>
        public static string Equal(string propertyName, string propertyValue)
        {
            if (string.IsNullOrEmpty(propertyName))
            {
                throw new ArgumentNullException(nameof(propertyName));
            }

            if (string.IsNullOrEmpty(propertyValue))
            {
                throw new ArgumentNullException(nameof(propertyValue));
            }

            return $"{propertyName} eq '{propertyValue}'";
        }

        /// <summary>
        /// Creates a filter string with "or" conditions between each successive pair of parameters from the incoming sequence
        /// </summary>
        /// <param name="conditions">Not empty sequence of conditions</param>
        /// <returns></returns>
        public static string GenerateOrSequence(IEnumerable<string> conditions)
        {
            return conditions.Aggregate(Or);
        }

        /// <summary>
        /// Creates a filter string with "and" conditions between each successive pair of parameters from the incoming sequence
        /// </summary>
        /// <param name="conditions">Not empty sequence of conditions</param>
        /// <returns></returns>
        public static string GenerateAndSequence(IEnumerable<string> conditions)
        {
            return conditions.Aggregate(And);
        }

        /// <summary>
        /// Creates a filter query string with all passed filters for user
        /// </summary>
        /// <param name="filters">Model of filters for user</param>
        /// <returns></returns>
        public static string GetUserFilterString(UserFilteringParams filters)
        {
            List<string> filtersByFields = new List<string>();

            if (!string.IsNullOrEmpty(filters.Role))
            {
                filtersByFields.Add(Equal(Consts.RolePropertyName, filters.Role));
            }

            if (filters.CompanyIds != null && filters.CompanyIds.Length != 0)
            {
                List<string> companyIdsFilter = new List<string>();
                foreach (var companyId in filters.CompanyIds)
                {
                    companyIdsFilter.Add(Equal(Consts.CompanyIdPropertyName, companyId.ToString()));
                }

                filtersByFields.Add(GenerateOrSequence(companyIdsFilter));
            }

            if (filters.ManagerIds != null && filters.ManagerIds.Length != 0)
            {
                List<string> managerIdsFilter = new List<string>();
                foreach (var managerId in filters.ManagerIds)
                {
                    managerIdsFilter.Add(Equal(Consts.ManagerIdPropertyName, managerId.ToString()));
                }

                filtersByFields.Add(GenerateOrSequence(managerIdsFilter));
            }

            return filtersByFields.Count != 0 ? $"$filter={GenerateAndSequence(filtersByFields)}" : "";
        }
    }
}
