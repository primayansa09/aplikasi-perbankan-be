namespace API_BSS.Controllers.DTO
{
    public class RequestTransactionDTO
    {
        public string SourceAccount { get; set; }
        public string DestinationAccount { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }
    }
}
