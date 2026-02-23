namespace API_BSS.Model
{
    public class Transactions
    {
        public Guid Id { get; set; }
        public Guid AccountId { get; set; }
        public string SourceAccount { get; set; }
        public string DestinationAccount { get; set; }
        public decimal Amount { get; set; }
        public decimal BalanceBefore { get; set; }
        public decimal BalanceAfter { get; set; }
        public string Description { get; set; }
        public TransactionStatus TransactionStatus { get; set; }
        public TransactionType TransactionType { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public bool IsDeleted { get; set; }
    }

    public enum TransactionStatus
    {
        Pending = 0,
        Success = 1,
        Failed = 2
    }

    public enum TransactionType
    {
        Debit = 0,
        Credit = 1
    }

}
