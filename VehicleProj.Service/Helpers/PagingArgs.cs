using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleProj.Service.Helpers
{
    public struct PagingArgs<T>
    {
        public IQueryable<T> Entities { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }

        public PagingArgs(IQueryable<T> entities, int pageIndex, int pageSize)
        {
            this.Entities = entities;
            this.PageIndex = pageIndex;
            this.PageSize = pageSize;
        }
    }
}
