using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xyzies.SSO.Identity.Data.Entity;
using Xyzies.SSO.Identity.Data.Entity.Azure;
using Xyzies.SSO.Identity.Data.Helpers;

namespace Xyzies.SSO.Identity.Services.Service
{
    public static class UsersExtension
    {
        private delegate bool UserFilters(AzureUser user);

        public static List<AzureUser> GetByParameters(this List<AzureUser> users, UserFilteringParams filters = null, UserSortingParameters sorting = null)
        {
            if (filters != null)
            {
                users = users.GetFiltered(filters);
            }

            if (sorting != null)
            {
                users = users.GetSorted(sorting);
            }

            return users.Skip(filters.Offset.HasValue ? filters.Offset.Value : 0)
                              .Take(filters.Limit.HasValue ? filters.Limit.Value : users.Count()).ToList();
        }


        private static List<AzureUser> GetSorted(this List<AzureUser> users, UserSortingParameters sorting)
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

        private static List<AzureUser> GetFiltered(this List<AzureUser> users, UserFilteringParams filters)
        {
            UserFilters condition = null;
            if (filters.Role != null)
            {
                condition += (AzureUser user) => user.Role == filters.Role;
            }

            if (filters.State != null)
            {
                condition += (AzureUser user) => user.State == filters.State;
            }

            if (filters.City != null)
            {
                condition += (AzureUser user) => user.City == filters.City;
            }

            if (filters.CompanyId != null)
            {
                condition += (AzureUser user) => user.CompanyId == filters.CompanyId;
            }

            if (filters.UserName != null)
            {
                condition += (AzureUser user) => user.DisplayName.ToLower().Contains(filters.UserName.ToLower());
            }

            if (filters.UsersId != null && filters.UsersId.Any())
            {
                condition += (AzureUser user) => filters.UsersId.Contains(user.ObjectId);
            }

            if (filters.CompaniesId != null && filters.CompaniesId.Any())
            {
                condition += (AzureUser user) => filters.CompaniesId.Contains(user.CompanyId);
            }

            if (filters.BranchesId != null && filters.BranchesId.Any())
            {
                condition += (AzureUser user) => user.BranchId.HasValue ? filters.BranchesId.Contains(user.BranchId.Value) : true;
            }

            if (condition != null)
            {
                return users.Where(user => AllTrue(condition, user)).ToList();
            }
            return users;
        }

        private static bool AllTrue(UserFilters condition, AzureUser user)
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
    }
}
