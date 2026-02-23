namespace API_BSS.Model
{
    public class Users
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; } 
        public string Password { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public bool IsDeleted { get; set; } = false;
        public ICollection<Accounts> Accounts { get; set; } = new List<Accounts>();
    }
}
