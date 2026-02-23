namespace API_BSS.Controllers.DTO
{
    public class ApiResponseDTO<T>
    {
        public int Status { get; set; }
        public string Message { get; set; } = null!;
        public T? Data { get; set; }
    }
}
