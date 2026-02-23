namespace API_BSS.Controllers.DTO
{
    public class AccountBalanceDTO
    {
        public Guid Id { get; set; }
        public string Account { get; set; }
        public decimal Amount { get; set; }
        public Guid AccountId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public bool IsDeleted { get; set; }
    }
}
