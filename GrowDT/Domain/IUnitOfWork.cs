namespace GrowDT.Domain
{
    public interface IUnitOfWork
    {
        bool Commit();
    }
}
