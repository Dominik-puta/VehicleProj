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
            return await PaginatedList<T>.CreateAsync(pagingArgs.Entities, pagingArgs.PageIndex, pagingArgs.PageSize);

        }

    }

}

