namespace Books.Models
{
    public class User
    {
        public int Id { get; set; }
        public string? Names { get; set; }
        public UserData? UserData { get; set; }
<<<<<<< HEAD
        public List<UserRole>? UserRoles { get; set; } 
        // Lista para múltiples roles
=======
        public UserRole? UserRole { get; set; }
        /* public List<UserRole>? UserRoles { get; set; } */ // Lista para múltiples roles
>>>>>>> eada80ab14525ffadbc8541a769fc5fc96377643
        public string? Status { get; set; }
        public ICollection<BookBorrow>? BookBorrows { get; set; }
    }
}

