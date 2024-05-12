using AutoWrapper.Extensions;
using MediatR.Registration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Warsztat.Contracts;
using Warsztat.Data.Entity;
using Warsztat.DTO.Response;

namespace Warsztat.Data.DataManager
{
    public class WorkshopManager: IWorkshopManager
    {
        private readonly IWorkshopDbContext _workshopDbContext;
        private readonly List<Dictionary> _dictionary;
        public WorkshopManager(IWorkshopDbContext workshopDbContext)
        {
            _workshopDbContext = workshopDbContext;
            _dictionary = Task.FromResult(_workshopDbContext.DictionarySet.ToList()).Result;
        }

        public async Task<IEnumerable<CarsResponse>> GetCars()
        {
            var x = await Task.FromResult(_workshopDbContext.CarsSet
                .Join(_workshopDbContext.ClientsSet,
                ci => ci.Clients_Id,
                cli => cli.Id,
                (ci, cli) => new CarsResponse(ci, cli, ci.DictionaryType_Id, ci.DictionaryEngine_Id, ci.DictionaryStatus_Id)
                ).ToList());

            foreach(var c in x)
            {
                c.DictionaryType_Id = _dictionary.Where(x => x.Id == c.dictIds[0]).First().Value;
                c.DictionaryEngine_Id = _dictionary.Where(x => x.Id == c.dictIds[1]).First().Value;
                c.DictionaryStatus_Id = _dictionary.Where(x => x.Id == c.dictIds[2]).First().Value;
            }

            return x;
            //var x = (from car in _workshopDbContext.CarsSet
            //         join client in _workshopDbContext.ClientsSet on car.Clients_Id equals client.Id
            //         join dictType in _dictionary on car.DictionaryType_Id equals dictType.Id
            //         join dictEngine in _dictionary on car.DictionaryEngine_Id equals dictEngine.Id
            //         join dictStatus in _dictionary on car.DictionaryStatus_Id equals dictStatus.Id
            //         select new CarsResponse(car, client, dictType, dictEngine, dictStatus)).ToList();
            //return x;
        }
        public async Task<int> AddCar(string brand, string model, string registration, string? productionYear, double? capacity, int clientID, int vehicleType, int engineType)
        {
            DateTime date = DateTime.MinValue;
            if (productionYear != null)
                date = new DateTime(productionYear.Substring(0, 4).ToInt32(), productionYear.Substring(5, 2).ToInt32(), productionYear.Substring(8, 2).ToInt32());
            var car = new Cars { Brand = brand, Model = model, Registration = registration, ProductionYear = date, Capacity = capacity, Active = true, Clients_Id = clientID, DictionaryType_Id = vehicleType, DictionaryEngine_Id = engineType, DictionaryStatus_Id = _dictionary.First(x=>x.Key == "Status pojazdu" && x.Value == "Nowy").Id };
            try
            {
                await _workshopDbContext.CarsSet.AddAsync(car);
                await _workshopDbContext.Context.SaveChangesAsync();
            }
            catch
            {
                return await Task.FromResult(-1);
            }
            return await Task.FromResult(car.Id);
        } 

        public async Task<IEnumerable<Users>> GetUsers()
        {
            return await Task.FromResult(_workshopDbContext.UsersSet.ToList());
        }

        public async Task<string> LoginUser(string login, string password)
        {
            var user = await _workshopDbContext.UsersSet.Where(x => (x.Login == login || x.Email == login)).FirstOrDefaultAsync();
            if (user == null) 
                return "Niepoprawny login";
            if (user.Password != password)
                return "Niepoprawne haslo";
            var client = await GetClientByUserID(user.Id);
            if (client == null)
                return "";

            return await Task.FromResult(CreateToken(user, client));
        }

        public async Task<Clients> GetClientByUserID(int userId)
        {
            return await Task.FromResult(_workshopDbContext.ClientsSet.First(x => x.Users_Id == userId));
        }

        public async Task<int> RegisterUser(string login, string password, string email)
        {
            var user = new Users { Login = login, Password = password, Email = email, Active = true, DictionaryType_Id = _dictionary.Where(x=>x.Key == "Typ konta" && x.Value == "User").First().Id};
            try
            {
                await _workshopDbContext.UsersSet.AddAsync(user);
                await _workshopDbContext.Context.SaveChangesAsync();
            }
            catch
            {
                return await Task.FromResult(-1);
            }
            return await Task.FromResult(user.Id);
        }

        public async Task<int> AddClient(string name, string surname, string phoneNumber, string city, string post, string address, int userId)
        {
            var client = new Clients { Name = name, Surname = surname, PhoneNumber = phoneNumber, City = city, Post = post, Address = address, Active = true, Users_Id = userId};
            try
            {
                await _workshopDbContext.ClientsSet.AddAsync(client);
                await _workshopDbContext.Context.SaveChangesAsync();
            }
            catch
            {
                return await Task.FromResult(-1);
            }

            return await Task.FromResult(client.Id);
        }

        public async Task<int> AddNewClient(string name, string surname, string phoneNumber, string city, string post, string address, string email)
        {
            var user = await RegisterUser(email, surname + phoneNumber.Substring(0, 3), email);
            if (user < 0)
                return -1;
            var client = new Clients { Name = name, Surname = surname, PhoneNumber = phoneNumber, City = city, Post = post, Address = address, Active = true, Users_Id = user };
            try
            {
                await _workshopDbContext.ClientsSet.AddAsync(client);
                await _workshopDbContext.Context.SaveChangesAsync();
            }
            catch
            {
                return await Task.FromResult(-1);
            }

            return await Task.FromResult(client.Id);
        }

        public async Task<bool> CheckEmail(string email)
        {
            return await Task.FromResult(_workshopDbContext.UsersSet.Any(x => x.Email == email));
        }

        public async Task<bool> CheckLogin(string login) {
            return await Task.FromResult(_workshopDbContext.UsersSet.Any(x => x.Login == login));
        }

        public string CreateToken(Users user, Clients client)
        {
            var jwtHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("kluczdowarsztatu");
            var identity = new ClaimsIdentity(new Claim[]
            {
                new Claim("sid", user.Id.ToString()),
                new Claim(ClaimTypes.Role, _dictionary.Where(x=>x.Id == user.DictionaryType_Id).First().Value),
                new Claim(ClaimTypes.Name, client.Name + " " + client.Surname)
            });

            var credentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = identity,
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = credentials
            };
            var token = jwtHandler.CreateToken(tokenDescriptor);
            return jwtHandler.WriteToken(token);
        }

        public IEnumerable<Dictionary> GetDictionaryValue(string name)
        {
            return _dictionary.Where(x => x.Key == name);
        }

        public Users GetUserById(int id)
        {
            return _workshopDbContext.UsersSet.Where(x => x.Id == id).First();
        }

        public async Task<int> AddOfferQuery(string contact, string content)
        {
            var offer = new Applications { Contact = contact, Content = content, DictionaryStatus_Id = _dictionary.Where(x=>x.Key == "Status zapytania" && x.Value=="Nowy").First().Id };
            try
            {
                await _workshopDbContext.ApplicationsSet.AddAsync(offer);
                await _workshopDbContext.Context.SaveChangesAsync();
            }
            catch
            {
                return await Task.FromResult(-1);
            }

            return await Task.FromResult(offer.Id);
        }

        public async Task<int> AddOfferQuery(int clientId, string content)
        {
            var offer = new Applications { Contact = GetUserById(clientId).Email, Content = content, DictionaryStatus_Id = _dictionary.Where(x=>x.Key == "Status zapytania" && x.Value == "Nowy").First().Id, Clients_Id = clientId };
            try
            {
                await _workshopDbContext.ApplicationsSet.AddAsync(offer);
                await _workshopDbContext.Context.SaveChangesAsync();
            }
            catch
            {
                return await Task.FromResult(-1);
            }

            return await Task.FromResult(offer.Id);
        }

        public async Task<IEnumerable<CarsResponse>> GetCarsByUserId(int id)
        {
            var client = await GetClientByUserID(id);
            var x = await Task.FromResult(_workshopDbContext.CarsSet
                .Where(x=>x.Clients_Id == client.Id)
                .Join(_workshopDbContext.ClientsSet,
                ci => ci.Clients_Id,
                cli => cli.Id,
                (ci, cli) => new CarsResponse(ci, cli, ci.DictionaryType_Id, ci.DictionaryEngine_Id, ci.DictionaryStatus_Id)
                ).ToList());

            foreach (var c in x)
            {
                c.DictionaryType_Id = _dictionary.Where(x => x.Id == c.dictIds[0]).First().Value;
                c.DictionaryEngine_Id = _dictionary.Where(x => x.Id == c.dictIds[1]).First().Value;
                c.DictionaryStatus_Id = _dictionary.Where(x => x.Id == c.dictIds[2]).First().Value;
            }
            return x;
        }

        public IEnumerable<Clients> GetAllClients()
        {
            return _workshopDbContext.ClientsSet;
        }
        
        public async Task<IEnumerable<Workers>> GetAllWorkers()
        {
            return await Task.FromResult(_workshopDbContext.WorkersSet.ToList());
        }

        public async Task<IEnumerable<ServicesResponse>> GetServices()
        {
            List<ServicesResponse> list = new();
            var x = await _workshopDbContext.ServicesViewSet.ToListAsync();
            foreach (var s in x)
            {
                list.Add(new ServicesResponse { Service = s, Workers = GetWorkersInService(s.Id).Result });
            }
            return await Task.FromResult(list);
        }

        public async Task<int> AddService(Dictionary[] services, int? count, int? hours, decimal? costSum, string description, string registration, int supervisorId)
        {
            var car = await GetCarByRegistration(registration);
            foreach (var s in services)
            {
                var service = new Services { Count = count, Hours = hours, DateAdd = DateTime.Now, CostSum = costSum, Description = description, Cars_Id = car, Dictionary_Id = s.Id, Workers_Id = supervisorId };
                await _workshopDbContext.ServicesSet.AddAsync(service);
                try
                {
                    await _workshopDbContext.Context.SaveChangesAsync();
                }
                catch
                {
                    return await Task.FromResult(-1);
                }
                var ws = new WorkersServices { Services_Id = service.Id, Workers_Id = supervisorId };
                await _workshopDbContext.WorkersServicesSet.AddAsync(ws);
            }
            try
            {
                await _workshopDbContext.Context.SaveChangesAsync();
            }
            catch
            {   
                return await Task.FromResult(-1);
            }
            return await Task.FromResult(1);
        }

        public async Task<int> GetCarByRegistration(string registration)
        {
            return await Task.FromResult(_workshopDbContext.CarsSet.Where(x => x.Registration == registration).FirstOrDefault().Id);
        }

        public async Task<IEnumerable<ServicesResponse>> GetServicesByUserId(int id)
        {
            List<ServicesResponse> list = new();
            var x = _workshopDbContext.ServicesViewSet.Where(x => x.Clients_Id == GetClientByUserID(id).Result.Id).ToList();
            foreach(var s in x)
            {
                list.Add(new ServicesResponse { Service = s, Workers = GetWorkersInService(s.Id).Result});
            } 
            return await Task.FromResult(list);
        }

        public async Task<int> UpdateClient(int id, string address, string city, string post, string phoneNumber)
        {
            var user = _workshopDbContext.UsersSet.Where(x => x.Id == id).First();

            if(_dictionary.Where(x=>x.Id == user.DictionaryType_Id).First().Value != "User")
            {
                var worker = _workshopDbContext.WorkersSet.Where(x=>x.Users_Id == id).First();
                if (worker.Id == 0)
                    return -1;
                worker.Address = address;
                worker.City = city;
                worker.Post = post;
                worker.PhoneNumber = phoneNumber;
            }

            var client = await GetClientByUserID(id);
            if (client.Id == 0)
                return -1;
            client.Address = address;
            client.City = city;
            client.Post = post;
            client.PhoneNumber = phoneNumber;
            try
            {
                await _workshopDbContext.Context.SaveChangesAsync();
            }
            catch
            {
                return await Task.FromResult(-1);
            }
            return await Task.FromResult(client.Id);
        }

        public async Task<int> ActiveClient(int id)
        {
            var client = _workshopDbContext.ClientsSet.Where(x => x.Id == id).FirstOrDefaultAsync().Result;
            if (client == null)
                return -1;
            client.Active = !client.Active;

            var worker = _workshopDbContext.WorkersSet.Where(x => x.Users_Id == client.Users_Id).First();
            if(worker != null)
            {
                worker.Active = !worker.Active;

                try
                {
                    await _workshopDbContext.Context.SaveChangesAsync();
                }
                catch
                {
                    return await Task.FromResult(-1);
                }
                return await Task.FromResult(worker.Id);
            }

            try
            {
                await _workshopDbContext.Context.SaveChangesAsync();
            }
            catch
            {
                return await Task.FromResult(-1);
            }
            return await Task.FromResult(client.Id);
        }

        public IEnumerable<ClientsWithUser> GetAllClientsWithUser()
        {
            var userId = _dictionary.Where(x => x.Key == "Typ konta" && x.Value == "User").First().Id;
            var clients = _workshopDbContext.ClientsSet.
                Join(_workshopDbContext.UsersSet,
                ci => ci.Users_Id, 
                ui => ui.Id,
                (ci,ui) => new ClientsWithUser(ci,ui)
                ).ToListAsync().Result;
            List<ClientsWithUser> result = new List<ClientsWithUser>(); 
            foreach (var client in clients)
            {
                if (client.User.DictionaryType_Id == userId)
                    result.Add(client);
            }
            return result;
        }

        public async Task<int> UserType(int id, int typeId)
        {
            var client = _workshopDbContext.ClientsSet.Where(x => x.Id == id).First();
            var user = _workshopDbContext.UsersSet.Where(x => x.Id == client.Users_Id).FirstOrDefaultAsync().Result;
            if (user == null)
                return -1;
            if (_dictionary.Where(x => x.Id == user.DictionaryType_Id).First().Value == "User")
            {
                await _workshopDbContext.WorkersSet.AddAsync(new Workers { Name = client.Name, Surname = client.Surname, PhoneNumber = client.PhoneNumber, City = client.City, Post = client.Post, Address = client.Address, Active = true, Users_Id = client.Users_Id });
                //_workshopDbContext.ClientsSet.Remove(client);
                //user.Active = !user.Active;
            }
            user.DictionaryType_Id = typeId;
            try
            {
                await _workshopDbContext.Context.SaveChangesAsync();
            }
            catch
            {
                return await Task.FromResult(-1);
            }
            return await Task.FromResult(user.Id);
        }

        public async Task<int> AddWorkersToService(int id, int[] workers)
        {
            var service = _workshopDbContext.ServicesSet.Where(x => x.Id == id).FirstOrDefaultAsync().Result;
            if (service == null)
                return -1;
            var wservice = _workshopDbContext.WorkersServicesSet.Where(x=>x.Services_Id == service.Id).ToListAsync().Result;
            foreach (var w in workers)
            {
                var ws = new WorkersServices { Services_Id = service.Id, Workers_Id = w };
                if (!wservice.Contains(ws))
                    await _workshopDbContext.WorkersServicesSet.AddAsync(ws);
            }
            try
            {
                await _workshopDbContext.Context.SaveChangesAsync();
            }
            catch
            {
                return await Task.FromResult(-1);
            }
            return await Task.FromResult(1);
        }

        public async Task<int> FinishService(int id)
        {
            var service = _workshopDbContext.ServicesSet.Where(x => x.Id == id).FirstOrDefaultAsync().Result;
            if (service == null)
                return -1;
            service.DateFinish = DateTime.Now;
            try
            {
                await _workshopDbContext.Context.SaveChangesAsync();
            }
            catch
            {
                return await Task.FromResult(-1);
            }
            return await Task.FromResult(service.Id);
        }

        public async Task<IEnumerable<Workers>> GetWorkersInService(int id)
        {
            var services = await Task.FromResult(_workshopDbContext.WorkersServicesSet.Where(x => x.Services_Id == id).ToList());
            var workers = await Task.FromResult(_workshopDbContext.WorkersSet);
            List<Workers> workersList = new List<Workers>();
            foreach(var s in services)
            {
                workersList.Add(workers.Where(x => x.Id == s.Workers_Id).First());
            }
            return workersList;
        }

        public async Task<IEnumerable<ApplicationsView>> GetAllApplications()
        {
            return await Task.FromResult(_workshopDbContext.ApplicationsViewSet);
        }

        public async Task<int> ChangeApplicationStatus(int id, string status)
        {
            var application = _workshopDbContext.ApplicationsSet.Where(x => x.Id == id).FirstOrDefaultAsync().Result;
            if (application == null)
                return -1;
            application.DictionaryStatus_Id = _dictionary.Where(x=>x.Key=="Status zapytania" && x.Value == status).First().Id;
            try
            {
                await _workshopDbContext.Context.SaveChangesAsync();
            }
            catch
            {
                return await Task.FromResult(-1);
            }
            return await Task.FromResult(application.Id);
        }

        public async Task<int> ActiveCar(int id)
        {
            var car = _workshopDbContext.CarsSet.Where(x => x.Id == id).FirstOrDefaultAsync().Result;
            if (car == null)
                return -1;
            car.Active = !car.Active;

            try
            {
                await _workshopDbContext.Context.SaveChangesAsync();
            }
            catch
            {
                return await Task.FromResult(-1);
            }
            return await Task.FromResult(car.Id);
        }

        public async Task<int> CarType(int id, int typeId)
        {
            var car = _workshopDbContext.CarsSet.Where(x => x.Id == id).First();
            if (car == null)
                return -1;
            car.DictionaryStatus_Id = typeId;
            try
            {
                await _workshopDbContext.Context.SaveChangesAsync();
            }
            catch
            {
                return await Task.FromResult(-1);
            }
            return await Task.FromResult(car.Id);
        }
        public async Task<IEnumerable<ServicesResponse>> GetServicesByCarId(int id)
        {
            List<ServicesResponse> list = new();
            var x = _workshopDbContext.ServicesViewSet.Where(x => x.Cars_Id == id).ToList();
            foreach (var s in x)
            {
                list.Add(new ServicesResponse { Service = s, Workers = GetWorkersInService(s.Id).Result });
            }
            return await Task.FromResult(list);
        }

        public IEnumerable<ClientsWithUser> GetAllWorkersWithUser()
        {
            var userId = _dictionary.Where(x => x.Key == "Typ konta" && x.Value == "User").First().Id;
            var workers = _workshopDbContext.WorkersSet.
                Join(_workshopDbContext.UsersSet,
                wi => wi.Users_Id,
                ui => ui.Id,
                (wi, ui) => new ClientsWithUser(wi, ui)
                ).ToListAsync().Result;
            List<ClientsWithUser> result = new List<ClientsWithUser>();
            foreach (var worker in workers)
            {
                if (worker.User.DictionaryType_Id != userId)
                    result.Add(worker);
            }
            return result;
        }

        public async Task<int> ActiveWorker(int id)
        {
            var worker = _workshopDbContext.WorkersSet.Where(x => x.Id == id).FirstOrDefaultAsync().Result;
            if (worker == null)
                return -1;
            worker.Active = !worker.Active;

            try
            {
                await _workshopDbContext.Context.SaveChangesAsync();
            }
            catch
            {
                return await Task.FromResult(-1);
            }
            return await Task.FromResult(worker.Id);
        }
    }
}
