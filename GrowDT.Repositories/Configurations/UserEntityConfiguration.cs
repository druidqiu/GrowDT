using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using GrowDT.Models.Entities;

namespace GrowDT.Repositories.Configurations
{
    public class UserEntityConfiguration : EntityTypeConfiguration<User>
    {
        public UserEntityConfiguration()
        {
            ToTable("User").HasKey(m => m.Id);
            Property(m => m.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(m => m.Code).HasColumnName("Code").HasMaxLength(100).IsRequired();
            Property(m => m.Name).HasColumnName("Name").HasMaxLength(100).IsRequired();
            Property(m => m.Email).HasColumnName("Email").HasMaxLength(100);

            HasMany(m => m.Roles).WithMany(m => m.Users).Map(mp =>
            {
                mp.MapLeftKey("UserId");
                mp.MapRightKey("RoleId");
                mp.ToTable("UserRoleRef");
            });
        }
    }
}
