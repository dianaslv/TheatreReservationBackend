using System;
using System.Collections.Generic;
using System.Linq;
using Theatre.Web.Core.Helpers.Interfaces.Commons;

namespace Theatre.Web.Core.Helpers.Commons.Filters.Interfaces
{
    public interface IFilter<TEntity> where TEntity : IIdentifier
    {
        public string SearchTerm { get; set; }
        public List<Guid> Ids { get; set; }
        IQueryable<TEntity> Filter(IQueryable<TEntity> filterQuery);
    }
}