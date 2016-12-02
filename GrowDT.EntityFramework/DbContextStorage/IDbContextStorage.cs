namespace GrowDT.EntityFramework.DbContextStorage
{
    public interface IDbContextStorage
    {
        T Retrieve<T>(string storageKey);
        void Store<T>(string storageKey, T entity);
    }
}
