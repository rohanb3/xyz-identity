using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Xyzies.SSO.Identity.Data.Core
{
    public static class GetPartExtensions
    {
        public static LazyLoadedResult<T> GetPart<T>(this IQueryable<T> query, LazyLoadParameters parameters)
            where T : class
        {
            if (parameters == null)
            {
                return new LazyLoadedResult<T>
                {
                    Result = query,
                    Total = query.Count()
                };
            }
            var result = query.Skip(parameters.Offset).Take(parameters.Limit);
            return new LazyLoadedResult<T>
            {
                Result = result,
                Limit = parameters.Limit,
                Offset = parameters.Offset,
                Total = query.Count()
            };
        }
    }
}
