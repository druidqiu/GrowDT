using System.Collections;

namespace GrowDT.EntityFramework.DbContextStorage
{
    public class ThreadDbContextStorage : IDbContextStorage
    {
        private static readonly Hashtable HsTable = new Hashtable();

        public T Retrieve<T>(string storageKey)
        {
            if (HsTable.Contains(storageKey))
                return (T) HsTable[storageKey];
            return default(T);
        }

        public void Store<T>(string storageKey, T entity)
        {
            if (HsTable.Contains(storageKey))
                HsTable[storageKey] = entity;
            else
                HsTable.Add(storageKey, entity);
        }
    }
}
