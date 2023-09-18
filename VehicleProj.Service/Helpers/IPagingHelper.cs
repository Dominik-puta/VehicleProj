namespace VehicleProj.Service.Helpers
{
    public interface IPagingHelper<T>
    {
        Task<PaginatedList<T>> ApplyPaging(PagingArgs<T> pagingArgs);
    }
}