namespace VehicleProj.Helpers
{
    public interface IFilterHelper<T>
    {
        IQueryable<T> ApplyFitler(IQueryable<T> entities, string searchString, string searchBy);
    }
}
