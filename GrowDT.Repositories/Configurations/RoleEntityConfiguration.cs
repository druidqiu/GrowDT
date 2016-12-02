using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using GrowDT.Models.Entities;

namespace GrowDT.Repositories.Configurations
{
    public class RoleEntityConfiguration : EntityTypeConfiguration<Role>
    {
        public RoleEntityConfiguration()
        {
            ToTable("Role").HasKey(m => m.Id);
            Property(m => m.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(m => m.RoleName).HasColumnName("Code").IsRequired();
            Property(m => m.Name).HasColumnName("Name").HasMaxLength(100).IsRequired();
        }
    }
}
