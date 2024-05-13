using Warsztat.Data.Entity;

namespace Warsztat.Contracts
{
    public interface ISqlServerManager
    {
        Task<IEnumerable<Users>> GetUsers();
        IEnumerable<Clients> GetAllClients();
        Task<IEnumerable<Clients>> GetAllClientsEF();
        IEnumerable<Clients> GetAllClientsDapper();
        Task<int> RegisterUser(string login, string password, string email);
        Task<int> AddNewClient(string name, string surname, string phoneNumber, string city, string post, string address, string email);
        Task<int> AddCountOfClientsSimple(int count);
        Task<int> AddCountOfClientsConnection(int count);
        Task<int> AddCountOfClientsStringBuilder(int count);
        Task<int> AddCountOfClientsEF(int id);
        Task<int> AddCountOfClientsEFRange(int count);
        Task<int> AddCountOfClientsDapperSimple(int count);
        Task<int> AddCountOfClientsDapperList(int count);
        Task<int> AddCountOfClientsDapperBulk(int count);
    }
}
