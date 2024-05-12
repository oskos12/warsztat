using System.ComponentModel.DataAnnotations;

namespace Warsztat.Data.Entity
{
    public class Clients
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string PhoneNumber { get; set; }
        public string? City { get; set; }
        public string? Post { get; set; }
        public string? Address { get; set; }
        public bool Active { get; set; }
        public int Users_Id { get; set; }
    }
}
