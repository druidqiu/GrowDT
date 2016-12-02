namespace GrowDT.Domain
{
    public abstract class UnitOfWorkBase : IUnitOfWork
    {
        public abstract bool Commit();
    }
}
