using System;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using GrowDT.Domain;
using GrowDT.EntityFramework.Infrastructures;

namespace GrowDT.EntityFramework.Uow
{
    public class EfUnitOfWork<TDbContext> : IUnitOfWork
        where TDbContext : DbContext
    {
        private readonly IDbContextProvider<TDbContext> _dbContextProvider;
        private TDbContext Context { get { return _dbContextProvider.DbContext; } }
        private readonly DbContextTransaction _transaction;

        protected EfUnitOfWork(IDbContextProvider<TDbContext> dbContextProvider)
        {
            _dbContextProvider = dbContextProvider;
            _transaction = Context.Database.CurrentTransaction ?? Context.Database.BeginTransaction();
        }

        public bool Commit()
        {
            try
            {
                bool flag = Context.SaveChanges() > 0;
                if (_transaction != null)
                {
                    _transaction.Commit();
                }
                return flag;
            }
            catch (DbEntityValidationException dvex)
            {
                var errors = dvex.EntityValidationErrors.SelectMany(m => m.ValidationErrors).Select(m => m.ErrorMessage);
                string message = "Throw DbEntityValidationException: ";
                message += string.Join(";", errors);
                LogDebug(message);
                //LogError(message);
                if (_transaction != null)
                {
                    _transaction.Rollback();
                }
                _dbContextProvider.ResetDbContext();

                return false;
            }
            catch (Exception ex)
            {
                string message = "Throw Exception When Committing To DB: ";
                message += ex.Message;
                LogDebug(message);
                //LogError(message);
                if (_transaction != null)
                {
                    _transaction.Rollback();
                }
                _dbContextProvider.ResetDbContext();

                return false;
            }
        }

        private void LogDebug(string message)
        {
            Logging.LoggingFactory.GetLogger().Log(Logging.LogLevel.Debug, message);
        }

        private void LogError(string message)
        {
            Logging.LoggingFactory.GetLogger().Log(Logging.LogLevel.Error, message);
        }
    }
}
