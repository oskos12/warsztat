using System.ComponentModel.DataAnnotations;

namespace Warsztat.DTO.Request
{
    public class OfferRequest
    {
        public string phoneNumber { get; set; }
        public string email { get; set; }
        public string clientId { get; set; }
        [Required]
        public string query { get; set; }
    }
}
