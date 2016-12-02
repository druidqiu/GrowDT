using System.Data.Entity;

namespace GrowDT.EntityFramework.Infrastructures
{
    public class Configure : DbConfiguration
    {
        public Configure()
        {
            AddInterceptor(new CommandInterceptor());
        }
    }
}
