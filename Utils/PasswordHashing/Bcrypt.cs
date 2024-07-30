using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Books.Infrastructure.Data;

namespace Books.Utils.PasswordHashing
{
    public class Bcrypt
    {
        private readonly BaseContext _context;
        public Bcrypt(BaseContext context)
        {
            _context = context;
        }
        public string HashPassword(string password)
        {   
            // Generate the password hash
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        public bool VerifyPassword(string password, string hashedPassword)
        {
            // Verify the password against the hash
            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        }
    }
}