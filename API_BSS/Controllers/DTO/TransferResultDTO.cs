namespace API_BSS.Controllers.DTO
{
    public class TransferResultDTO
    {
        public int Status { get; set; }
        public string Message { get; set; }
        public string? SourceAccount { get; set; }
        public string? DestinationAccount { get; set; }
        public decimal? Amount { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string? Description { get; set; }
    }
}
