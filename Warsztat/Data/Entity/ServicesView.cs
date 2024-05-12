using System.ComponentModel.DataAnnotations;

namespace Warsztat.Data.Entity
{
    public class ServicesView
    {
        public int Id { get; set; }
        public string Registration { get; set; }
        public DateTime DateAdd { get; set; }
        public DateTime? DateFinish { get; set; }
        public int? Count { get; set; }
        public int? Hours { get; set; }
        public decimal? CostSum { get; set; }
        public string DictValue { get; set; }
        public string DictDescription { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int Cars_Id { get; set; }
        public int Dictionary_Id { get; set; }
        public int Workers_Id { get; set; }
        public int Clients_Id { get; set; }
    }
}
