namespace Books.Models
{
    public class User
    {
        public int Id { get; set; }
        public string? Names { get; set; }
        public UserData? UserData { get; set; }
        public UserRole? UserRole { get; set; }
        /* public List<UserRole>? UserRoles { get; set; } */ // Lista para múltiples roles
        public string? Status { get; set; }
        public ICollection<BookBorrow>? BookBorrows { get; set; }
    }
}
