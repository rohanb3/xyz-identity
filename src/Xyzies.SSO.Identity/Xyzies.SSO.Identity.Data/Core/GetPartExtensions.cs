using System.Linq;

namespace Xyzies.SSO.Identity.Data.Core
{
    public static class GetPartExtensions
    {
        public static LazyLoadedResult<T> GetPart<T>(this IQueryable<T> query, LazyLoadParameters parameters = null)
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
            var result = query.Skip(parameters.Offset.HasValue ? parameters.Offset.Value : 0)
                              .Take(parameters.Limit.HasValue ? parameters.Limit.Value : query.Count());
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
