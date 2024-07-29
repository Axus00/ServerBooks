using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Books.Models
{
    public class Role
    {
        public int Id {get; set; }
        public  enum Type
        {
            Customer,
            Admin

        }

        public UserRole? UserRoles { get; set; }
    }
}