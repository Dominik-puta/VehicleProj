using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleProj.Service.Helpers
{
    public struct IndexArgs
    {
        public string SortOrder { get; set; }
        public string SearchString { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        public IndexArgs(string _sortOrder, string _searchString, int _pageNumber, int pageSize )
        {
            this.SortOrder = _sortOrder;
            this.SearchString = _searchString;
            this.PageNumber = _pageNumber;
            this.PageSize = pageSize;
        }
    }
}
