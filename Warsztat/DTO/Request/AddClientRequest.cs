using System.ComponentModel.DataAnnotations;

namespace Warsztat.DTO.Request
{
    public class AddClientRequest
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        public string City { get; set; }
        public string Post { get; set; }
        public string Address { get; set; }
        [Required]
        public string Email { get; set; }
    }
}
