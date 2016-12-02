using System;
using System.Collections.Generic;

namespace GrowDT.MemoryDb
{
    public class MemoryDatabase
    {
        private readonly Dictionary<Type, object> _sets;

        private readonly object _syncObj = new object();

        public MemoryDatabase()
        {
            _sets = new Dictionary<Type, object>();
        }

        public List<TEntity> Set<TEntity>()
        {
            var entityType = typeof(TEntity);

            lock (_syncObj)
            {
                if (!_sets.ContainsKey(entityType))
                {
                    _sets[entityType] = new List<TEntity>();
                }

                return _sets[entityType] as List<TEntity>;
            }
        }
    }

    public class MemoryDatabaseFactory
    {
        private static readonly MemoryDatabase Database = new MemoryDatabase();

        public static MemoryDatabase GetInstance()
        {
            return Database;
        }
    }
}