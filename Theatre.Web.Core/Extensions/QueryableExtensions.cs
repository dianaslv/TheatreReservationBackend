using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Theatre.Web.Core.Helpers.Commons;

namespace Theatre.Web.Core.Extensions
{
    public static class QueryableExtensions
    {
        public static async Task<Tuple<int, List<TSource>>> WithPaginationAsync<TSource>(this IQueryable<TSource> source, Pagination paging)
        {
            return new Tuple<int, List<TSource>>(
                await source.CountAsync(),
                await source.Skip(paging.Offset).Take(paging.Take)
                    .AsQueryable()
                    .ToListAsync());
        }
    }
}