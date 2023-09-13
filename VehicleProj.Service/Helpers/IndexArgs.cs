using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleProj.Service.Helpers
{
    public struct IndexArgs
    {
        public string sortOrder { get; set; }
        public string searchString { get; set; }
        public int pageNumber { get; set; }
        public int pageSize { get; set; }

        public IndexArgs(string _sortOrder, string _searchString, int _pageNumber, int pageSize )
        {
            this.sortOrder = _sortOrder;
            this.searchString = _searchString;
            this.pageNumber = _pageNumber;
            this.pageSize = pageSize;
        }
    }
}
