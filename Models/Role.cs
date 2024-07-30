using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Books.Models
{
    public class Role
    {
        public int Id { get; set; }
        
        public enum Type
        {
            Customer,
            Admin
        }
        
        public Type RoleType { get; set; }
        
        public ICollection<UserRole>? UserRoles { get; set; } // Relación uno a muchos con UserRole
    }
}
