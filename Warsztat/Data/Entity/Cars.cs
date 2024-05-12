using System.ComponentModel.DataAnnotations;

namespace Warsztat.Data.Entity
{
    public class Cars
    {
        [Key]
        public int Id { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string Registration { get; set; }
        public DateTime? ProductionYear { get; set; }
        public double? Capacity { get; set; }
        public bool Active { get; set; }
        public int Clients_Id { get; set; }
        public int DictionaryType_Id { get; set; }
        public int DictionaryEngine_Id { get; set; }
        public int DictionaryStatus_Id { get; set; }
    }
}
