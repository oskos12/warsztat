using System.ComponentModel.DataAnnotations;
using Warsztat.Data.Entity;

namespace Warsztat.DTO.Request
{
    public class AddServiceRequest
    {
        [Required]
        public Dictionary[] ServiceType { get; set; }
        public int? Count { get; set; }
        public int? Hours { get; set; }
        public decimal? CostSum { get; set; }
        public string Description { get; set; }
        [Required]
        public string Registration { get; set; }
        [Required]
        public Workers Supervisor { get; set; }
    }
}
