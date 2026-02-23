using System.ComponentModel.DataAnnotations;

namespace API_BSS.Controllers.DTO
{
    public class AddAccountDTO
    {
        [Required(ErrorMessage = "FullName wajib diisi")]
        [MaxLength(100)]
        public string FullName { get; set; } = null!;
        [Required(ErrorMessage = "Email wajib diisi")]
        [EmailAddress(ErrorMessage = "Format email tidak valid")]
        [MaxLength(100)]
        [RegularExpression(
        @"^[^@\s]+@[^@\s]+\.[^@\s]{2,}$",
        ErrorMessage = "Format email tidak valid (contoh: user@gmail.com)")]
        public string Email { get; set; } = null!;
        [Required(ErrorMessage = "Password wajib diisi")]
        [MinLength(6, ErrorMessage = "Password minimal 6 karakter")]
        [RegularExpression(
        @"^(?=.*[A-Z])(?=.*\d)(?=.*[\W_]).+$",
        ErrorMessage = "Password harus mengandung minimal 1 huruf kapital, 1 angka, dan 1 simbol.")]
        public string Password { get; set; } = null!;
    }
}
