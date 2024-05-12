using System.ComponentModel.DataAnnotations;

namespace Warsztat.DTO.Request
{
    public class UsersRequest
    {
        [Required]
        public string Login { get; set; }
        [Required]
        public string Password { get; set; }
    }
    public class RegisterRequest : UsersRequest
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        public string? PhoneNumber { get; set; }
        public string? City { get; set; }
        public string? Post { get; set; }
        public string? Address { get; set; }
        public string? Email { get; set; }
    }
}
