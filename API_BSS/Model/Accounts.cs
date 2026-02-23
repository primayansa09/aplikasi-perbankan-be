namespace API_BSS.Model
{
    public class Accounts
    {
        public Guid Id { get; set; }

        public string Account { get; set; }
        public decimal Amount { get; set; }

        public Guid UserId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public bool IsDeleted { get; set; }

    }
}
