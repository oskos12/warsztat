using System.ComponentModel.DataAnnotations;
using Warsztat.Data.Entity;

namespace Warsztat.DTO.Request
{
    public class AddCarRequest
    {
        [Required]
        public string Brand { get; set; }
        [Required]
        public string Model { get; set; }
        [Required]
        public string Registration { get; set; }
        public string? ProductionYear { get; set; }
        public double? Capacity { get; set; }
        [Required]
        public Clients Owner { get; set; }
        [Required]
        public Dictionary VehicleType { get; set; }
        [Required]
        public Dictionary EngineType { get; set; }
    }
}
