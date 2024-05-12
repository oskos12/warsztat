using System.ComponentModel.DataAnnotations;

namespace Warsztat.DTO.Request
{
    public class Worker
    {
        public int[] NewWorkers { get; set; }
    }
    public class AddWorkersToServiceRequest
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public Worker Workers { get; set; }
    }
}
