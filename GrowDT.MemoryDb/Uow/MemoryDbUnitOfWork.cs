using GrowDT.Domain;

namespace GrowDT.MemoryDb.Uow
{
    public class MemoryDbUnitOfWork : UnitOfWorkBase
    {
        public override bool Commit()
        {
            return true;
        }
    }
}