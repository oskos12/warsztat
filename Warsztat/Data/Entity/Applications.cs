using System.ComponentModel.DataAnnotations;

namespace Warsztat.Data.Entity
{
    public class Applications
    {
        [Key]
        public int Id { get; set; }
        public string Content { get; set; }
        public string Contact { get; set; }
        public int? Clients_Id { get; set; }
        public int? DictionaryStatus_Id { get; set; }
    }
}
