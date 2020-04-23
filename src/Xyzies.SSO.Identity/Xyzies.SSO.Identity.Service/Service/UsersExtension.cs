using System;
using System.Collections.Generic;
using System.Linq;
using Xyzies.SSO.Identity.Data.Entity;
using Xyzies.SSO.Identity.Data.Entity.Azure;
using Xyzies.SSO.Identity.Data.Helpers;

namespace Xyzies.SSO.Identity.Services.Service
{
    public static class UsersExtension
    {
        private delegate bool UserFilters(AzureUserWithTenant user);

        public static List<AzureUserWithTenant> GetByParameters(this List<AzureUserWithTenant> users, UserFilteringParamsWithTenant filters = null, UserSortingParameters sorting = null)
        {
            if (filters != null)
            {
                users = users.GetFiltered(filters);
            }

            if (sorting != null)
            {
                users = users.GetSorted(sorting);
            }

            return users.Skip(filters?.Offset ?? 0)
                              .Take(filters?.Limit ?? users.Count()).ToList();
        }


        private static List<AzureUserWithTenant> GetSorted(this List<AzureUserWithTenant> users, UserSortingParameters sorting)
        {
            if (sorting.Sort == Consts.UsersSorting.City)
            {
                users = sorting.By == Consts.UsersSorting.Descending ? users.OrderByDescending(x => x.City).ToList() : users.OrderBy(x => x.City).ToList();
            }

            if (sorting.Sort == Consts.UsersSorting.Id)
            {
                users = sorting.By == Consts.UsersSorting.Descending ? users.OrderByDescending(x => x.ObjectId).ToList() : users.OrderBy(x => x.ObjectId).ToList();
            }

            if (sorting.Sort == Consts.UsersSorting.Name)
            {
                users = sorting.By == Consts.UsersSorting.Descending ? users.OrderByDescending(x => x.DisplayName).ToList() : users.OrderBy(x => x.DisplayName).ToList();
            }

            if (sorting.Sort == Consts.UsersSorting.Role)
            {
                users = sorting.By == Consts.UsersSorting.Descending ? users.OrderByDescending(x => x.Role).ToList() : users.OrderBy(x => x.Role).ToList();
            }

            if (sorting.Sort == Consts.UsersSorting.State)
            {
                users = sorting.By == Consts.UsersSorting.Descending ? users.OrderByDescending(x => x.State).ToList() : users.OrderBy(x => x.State).ToList();
            }
            return users;
        }

        private static List<AzureUserWithTenant> GetFiltered(this List<AzureUserWithTenant> users, UserFilteringParamsWithTenant filters)
        {
            UserFilters condition = null;

            if (!string.IsNullOrEmpty(filters.Status))
            {
                condition += (AzureUserWithTenant user) => FilterUserByStatus(user, filters.Status);
            }

            if (filters.Role != null && filters.Role.Any())
            {
                condition += (AzureUserWithTenant user) => filters.Role.Select(role => role.ToLower()).Contains(user.Role?.ToLower());
            }

            if (filters.State != null && filters.State.Any())
            {
                condition += (AzureUserWithTenant user) => filters.State.Select(state => state.ToLower()).Contains(user.State?.ToLower());
            }

            if (filters.City != null && filters.City.Any())
            {
                condition += (AzureUserWithTenant user) => filters.City.Select(city => city.ToLower()).Contains(user.City?.ToLower());
            }

            if (filters.UserName != null)
            {
                condition += (AzureUserWithTenant user) => user.DisplayName.ToLower().Contains(filters.UserName.ToLower());
            }

            if (filters.UsersId != null && filters.UsersId.Any())
            {
                condition += (AzureUserWithTenant user) => filters.UsersId.Contains(user.ObjectId);
            }

            if (filters.CompanyId != null && filters.CompanyId.Any())
            {
                condition += (AzureUserWithTenant user) => filters.CompanyId.Contains(user.CompanyId?.ToLower());
            }

            if (filters.BranchesId != null && filters.BranchesId.Any())
            {
                condition += (AzureUserWithTenant user) => user.BranchId.HasValue ? filters.BranchesId.Contains(user.BranchId.Value) : false;
            }

            if (filters.TenantId.HasValue)
            {
                condition += (AzureUserWithTenant user) => user.TenantId == filters.TenantId;
            }

            if (condition != null)
            {
                return users.Where(user => AllTrue(condition, user)).ToList();
            }
            return users;
        }

        private static bool AllTrue(UserFilters condition, AzureUserWithTenant user)
        {
            foreach (UserFilters t in condition.GetInvocationList())
            {
                if (!t(user))
                {
                    return false;
                }
            }
            return true;
        }

        private static bool FilterUserByStatus(AzureUser user, string status)
        {
            switch (status)
            {
                case Consts.UserStatuses.Active:
                    return user.RequestStatus?.Name?.ToLower() == Consts.UserStatuses.CPActiveUserStatus && user.AccountEnabled == true;
                case Consts.UserStatuses.Disabled:
                    return user.RequestStatus?.Name?.ToLower() != Consts.UserStatuses.CPActiveUserStatus || user.AccountEnabled == false;
                default:
                    throw new ArgumentException("Unknown status", nameof(status));
            }
        }
    }
}
