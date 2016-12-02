using System.Web;

namespace GrowDT.EntityFramework.DbContextStorage
{
    public class HttpDbContextStorage : IDbContextStorage
    {
        public T Retrieve<T>(string storageKey)
        {
            if (HttpContext.Current.Items.Contains(storageKey))
                return (T) HttpContext.Current.Items[storageKey];
            return default(T);
        }

        public void Store<T>(string storageKey, T entity)
        {
            if (HttpContext.Current.Items.Contains(storageKey))
                HttpContext.Current.Items[storageKey] = entity;
            else
                HttpContext.Current.Items.Add(storageKey, entity);
        }
    }
}
