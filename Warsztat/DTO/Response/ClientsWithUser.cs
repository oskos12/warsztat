using Warsztat.Data.Entity;

namespace Warsztat.DTO.Response
{
    public class ClientsWithUser
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string PhoneNumber { get; set; }
        public string? City { get; set; }
        public string? Post { get; set; }
        public string? Address { get; set; }
        public bool Active { get; set; }
        public Users User { get; set; }
        public ClientsWithUser(Clients client, Users user)
        {
            Id = client.Id;
            Name = client.Name;
            Surname = client.Surname;
            PhoneNumber = client.PhoneNumber;
            City = client.City;
            Post = client.Post;
            Address = client.Address;
            Active = client.Active;
            User = user;
        }
        public ClientsWithUser(Workers worker, Users user)
        {
            Id = worker.Id;
            Name = worker.Name;
            Surname = worker.Surname;
            PhoneNumber = worker.PhoneNumber;
            City = worker.City;
            Post = worker.Post;
            Address = worker.Address;
            Active = worker.Active;
            User = user;
        }
    }
}
