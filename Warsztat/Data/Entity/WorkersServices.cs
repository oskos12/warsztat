using System.ComponentModel.DataAnnotations;

namespace Warsztat.Data.Entity
{
    public class WorkersServices
    {
        [Key]
        public int Id { get; set; }
        public int Workers_Id { get; set; }
        public int Services_Id { get; set; }
    }
}
