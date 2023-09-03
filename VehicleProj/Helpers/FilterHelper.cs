using System.Linq.Dynamic.Core;

namespace VehicleProj.Helpers
{
    public class FilterHelper<T> : IFilterHelper<T>
    {
        public IQueryable<T> ApplyFitler(IQueryable<T> entities, string searchString, string searchBy)
        {
            if (!entities.Any())
                return entities;
            if (string.IsNullOrWhiteSpace(searchString) || string.IsNullOrEmpty(searchBy))
            {
                return entities;
            }
            searchString= searchString.Trim();
            searchBy = searchBy.Trim();
            String Search = searchBy + " ==@0 ";


            return entities.Where(Search, searchString);

        }
    }
}
