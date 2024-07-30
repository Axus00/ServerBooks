using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Books.Models
{
    public class Role
    {
        public int Id { get; set; }

        public string Type { get; set; }

        public List<UserRole> UserRole { get; set; }
    }
}
