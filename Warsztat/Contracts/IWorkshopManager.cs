using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Warsztat.Data.Entity;
using Warsztat.DTO.Response;

namespace Warsztat.Contracts
{
    public interface IWorkshopManager
    {
        Task<IEnumerable<CarsResponse>> GetCars();
        Task<IEnumerable<Users>> GetUsers();
        Task<string> LoginUser(string login, string password);
        Task<int> RegisterUser(string login, string password, string email);
        Task<int> AddClient(string name, string surname, string phoneNumber, string city, string post, string address, int userId);
        Task<bool> CheckEmail(string email);
        Task<bool> CheckLogin(string login);
        string CreateToken(Users user, Clients client);
        Task<Clients> GetClientByUserID(int userId);
        IEnumerable<Dictionary> GetDictionaryValue(string name);
        Task<int> AddCar(string brand, string model, string registration, string productionYear, double? capacity, int clientId, int vehicleType, int engineType);
        Users GetUserById(int id);
        Task<int> AddOfferQuery(string contact, string content);
        Task<int> AddOfferQuery(int clientId, string content);
        Task<IEnumerable<CarsResponse>> GetCarsByUserId(int id);
        IEnumerable<Clients> GetAllClients();
        Task<IEnumerable<Workers>> GetAllWorkers();
        Task<IEnumerable<ServicesResponse>> GetServices();
        Task<int> AddService(Dictionary[] services, int? count, int? hours, decimal? costSum, string description, string registration, int supervisorId);
        Task<IEnumerable<ServicesResponse>> GetServicesByUserId(int id);
        Task<int> GetCarByRegistration(string registration);
        Task<int> UpdateClient(int id, string address, string city, string post, string phoneNumber);
        Task<int> ActiveClient(int id);
        IEnumerable<ClientsWithUser> GetAllClientsWithUser();
        Task<int> UserType(int id, int typeId);
        Task<int> AddNewClient(string name, string surname, string phoneNumber, string city, string post, string address, string email);
        Task<int> AddWorkersToService(int id, int[] workers);
        Task<int> FinishService(int id);
        Task<IEnumerable<Workers>> GetWorkersInService(int id);
        Task<IEnumerable<ApplicationsView>> GetAllApplications();
        Task<int> ChangeApplicationStatus(int id, string status);
        Task<int> ActiveCar(int id);
        Task<int> CarType(int id, int typeId);
        Task<IEnumerable<ServicesResponse>> GetServicesByCarId(int id);
        IEnumerable<ClientsWithUser> GetAllWorkersWithUser();
        Task<int> ActiveWorker(int id);
    }
}
