using System.Collections.Generic;
using GrowDT.Domain;

namespace GrowDT.Models.Entities
{
    public class User : IEntity<int>, IAggregateRoot
    {
        public User()
        {
            Roles = new List<Role>();
        }
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        public virtual List<Role> Roles { get; set; }
    }
}
