using System.ComponentModel.DataAnnotations;

namespace Warsztat.DTO.Request
{
    public class UpdateClientDataRequest
    {
        [Required]
        public int id { get; set; }
        [Required]
        public string address { get; set; }
        [Required]
        public string city { get; set; }
        [Required]
        public string phoneNumber { get; set; }
        [Required]
        public string post { get; set; }
    }
}
