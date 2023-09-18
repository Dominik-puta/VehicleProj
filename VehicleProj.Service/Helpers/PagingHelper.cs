using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleProj.Service.Models.Domain;

namespace VehicleProj.Service.Helpers
{
    public class PagingHelper<T> : IPagingHelper<T>
    {
        public async Task<PaginatedList<T>> ApplyPaging(PagingArgs<T> pagingArgs){
            return await PaginatedList<T>.CreateAsync(pagingArgs.entities, pagingArgs.pageIndex, pagingArgs.pageSize);

        }

    }
    public struct PagingArgs<T>
    {
        public IQueryable<T> entities { get; set; }
        public int pageIndex { get; set; }
        public int pageSize { get; set; }

        public PagingArgs(IQueryable<T> entities, int pageIndex, int pageSize)
        {
        this.entities = entities;
        this.pageIndex = pageIndex;
            this.pageSize = pageSize;
        }
    }
}

