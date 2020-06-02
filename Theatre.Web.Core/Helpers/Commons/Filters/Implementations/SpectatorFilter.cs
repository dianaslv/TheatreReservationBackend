using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Theatre.Web.Core.Extensions;
using Theatre.Web.Core.Helpers.Commons.Filters.Interfaces;
using Theatre.Web.Core.Models.Entities;

namespace Theatre.Web.Core.Helpers.Commons.Filters.Implementations
{
    public class SpectatorFilter : IFilter<Spectator>
    {
        public string SearchTerm { get; set; }
        public List<Guid> Ids { get; set; }

        public IQueryable<Spectator> Filter(IQueryable<Spectator> filterQuery)
        {
            if (string.IsNullOrEmpty(SearchTerm))
                return filterQuery;

            filterQuery = Guid.TryParse(SearchTerm, out var guid)
                ? filterQuery.Where(p => p.Id.Equals(guid))
                : filterQuery.Where(p => EF.Functions.Like(p.Username, SearchTerm.ToMySqlLikeSyntax()));

            if (Ids == null || !Ids.Any())
                return filterQuery;

            Ids.ForEach(t => { filterQuery = filterQuery.Where(p => p.Id.Equals(t)); });
            return filterQuery;
        }
    }
}