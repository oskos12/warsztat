using System.ComponentModel.DataAnnotations;

namespace Warsztat.DTO.Request
{
    public class UpdateApplicationRequest
    {
        [Required]
        public int IdApplication { get; set; }
        [Required]
        public string Status { get; set; }
    }
}
