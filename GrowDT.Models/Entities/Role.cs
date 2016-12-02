using System;
using System.Collections.Generic;
using GrowDT.Domain;

namespace GrowDT.Models.Entities
{
    [Flags]
    public enum RoleNames
    {
        None = 0,
        Admin = 1,
    }

    public class Role : IEntity<int>, IAggregateRoot
    {
        public Role()
        {
            Users = new List<User>();
        }

        public int Id { get; set; }
        public RoleNames RoleName { get; set; }
        public string Name { get; set; }

        public virtual List<User> Users { get; set; }
    }
}
