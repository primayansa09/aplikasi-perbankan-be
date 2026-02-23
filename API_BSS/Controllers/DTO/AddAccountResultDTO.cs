namespace API_BSS.Controllers.DTO
{
    public class AddAccountResultDTO
    {
        public int Status { get; set; }
        public string Message { get; set; } = null!;
        public Guid? UserId { get; set; }
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string? Account { get; set; }
        public decimal? Amount { get; set; }
    }
}
