namespace Warsztat.Data.Entity
{
    public class ApplicationsView
    {
        public int Id { get; set; }
        public string? Content { get; set; }
        public string? Contact { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string Value { get; set; }
        public int? Clients_Id { get; set; }
        public int DictionaryStatus_Id { get; set; }

    }
}
