using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Clean.Api.Helpers.Queries
{
    public static class IQueryableExtensions
    {


        public static IQueryable<T> ApplyQuery<T>(this IQueryable<T> parentQuery, QueryParameters query)
        {
            return ApplyQueryInternal(parentQuery, query);
        }

        public static T[] GetQueryResults<T>(IQueryable<T> parentQuery, QueryParameters query)
        {
            return ApplyQueryInternal(parentQuery, query).ToArray();
        }

        private static IQueryable<T> ApplyQueryInternal<T>(IQueryable<T> parentQuery, QueryParameters query)
        {
            if (query == null)
            {
                return parentQuery;
            }

            var filteredQuery = parentQuery.ApplyFilter(query.Filter);
            query.FilteredCount = filteredQuery.Count();

            return filteredQuery
                .ApplyOrder(query.Sort)
                .ApplySkip(query.PageFirstItemNumber, query.PageSize);
        }

        public static IQueryable<T> ApplyOrder<T>(this IQueryable<T> query, string order)
        {
            if (string.IsNullOrEmpty(order))
            {
                return query;
            }

            try
            {
                var compiledOrdering = OrderByParser.Parse(order);
                return compiledOrdering.Apply<T>(query);
            }
            catch (Exception e)
            {
                throw new FormatException($"Provided sort expression '{order}' has incorrect format", e);
            }
        }

        public static IQueryable<T> ApplyFilter<T>(this IQueryable<T> query, string filter)
        {
            if (string.IsNullOrEmpty(filter))
            {
                return query;
            }

            try
            {
                var compiledFilter = new ExtendedODataFilterLanguage().Parse<T>(filter);

                return query.Where(compiledFilter);
            }
            catch (Exception e)
            {
                throw new FormatException($"Provided filter expression '{filter}' has incorrect format [{e.ToString()}]", e);
            }
        }

        public static IQueryable<T> ApplySkip<T>(this IQueryable<T> query, uint? skip, uint? take)
            => query
                .SkipIf(skip.HasValue, (int)skip.GetValueOrDefault())
                .TakeIf(take.HasValue, (int)take.GetValueOrDefault());

        private static IQueryable<T> SkipIf<T>(this IQueryable<T> query, bool predicate, int skip)
            => predicate ? query.Skip(skip) : query;

        private static IQueryable<T> TakeIf<T>(this IQueryable<T> query, bool predicate, int skip)
            => predicate ? query.Take(skip) : query;

    }
}
